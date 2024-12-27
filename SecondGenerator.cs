using System.Collections.Generic;
using UnityEngine;


public class SecondGenerator: MonoBehaviour
{
    [Header("Configuration")]
    public CharacterConfig config;

    [Header("Skeleton Parts")]
    private Dictionary<string, Transform> skeletonParts = new Dictionary<string, Transform>();

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
    private void OnValidate()
    {
        if (Application.isPlaying) return;
        UpdateSkeleton();
    }
    #endif
}
