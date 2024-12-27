using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "CharacterConfig", menuName = "Configs/Character")]
public class CharacterConfig : ScriptableObject  //Configuration Paatern Desing :In this pattern, settings and configuration data are managed from external sources.

{
    public Vector3 armSize;
    public Vector3 handSize;
    public Vector3 handPosition;
    public Vector3 spineSize;
    public float yPosition;
}

public class SkeletonUpdater : MonoBehaviour
{
    [Header("Configuration")]
    public CharacterConfig config;

    [Header("Skeleton Parts")] 
    private Dictionary<string, Transform> skeletonParts = new Dictionary<string, Transform>();  //Dictionary Pattern

    private void Awake()
    {
        InitializeSkeletonParts();
    }

    private void InitializeSkeletonParts()
    {
        // Populate skeleton parts dynamically based on children
        foreach (Transform child in GetComponentsInChildren<Transform>())
        {
            if (!skeletonParts.ContainsKey(child.name))
            {
                skeletonParts.Add(child.name, child);
            }
        }
    }

    public void UpdateSkeleton()
    {
        if (config == null)
        {
            Debug.LogError("Configuration is missing. Please assign a CharacterConfig.");
            return;
        }
    // Single Responsibility Principle : Each transaction has a single responsibility: Single Responsibility Principle.
        UpdateLimbSize("LeftArm", config.armSize);
        UpdateLimbSize("RightArm", config.armSize);
        UpdateLimbSize("Spine", config.spineSize);
        UpdatePosition("LeftHand", config.handPosition);
        UpdatePosition("RightHand", config.handPosition);
        UpdateYPosition(config.yPosition);
    }

    private void UpdateLimbSize(string partName, Vector3 size)
    {
        if (skeletonParts.TryGetValue(partName, out Transform part))
        {
            part.localScale = size;
        }
        else
        {
            Debug.LogWarning($"Skeleton part '{partName}' not found.");
        }
    }

    private void UpdatePosition(string partName, Vector3 position)
    {
        if (skeletonParts.TryGetValue(partName, out Transform part))
        {
            part.localPosition = position;
        }
        else
        {
            Debug.LogWarning($"Skeleton part '{partName}' not found.");
        }
    }

    private void UpdateYPosition(float yPosition)
    {
        Transform root = transform;
        Vector3 currentPosition = root.position;
        root.position = new Vector3(currentPosition.x, yPosition, currentPosition.z);
    }

    #if UNITY_EDITOR
    private void OnValidate() //Observer Pattern : 
    {
        if (Application.isPlaying) return;
        UpdateSkeleton();
    }
    #endif
}
