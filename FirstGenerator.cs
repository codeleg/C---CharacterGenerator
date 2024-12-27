using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FirstGenerator : MonoBehaviour

{ //nasıl birbirne entegre edeceğimi bulamadım . şimdilik sadece değişkenlerin atanarak değil find ile bulunarak transform ettim sciprit buraya bırakıyorum  
 // Kol boyutları
    [Header("Kol Boyutları")]
    [Range(0.5f, 3.0f)] public float armSizeX = 1.0f;
    [Range(0.5f, 3.0f)] public float armSizeY = 1.0f;
    [Range(0.5f, 3.0f)] public float armSizeZ = 1.0f;

    [Header("El Boyutu Ayarı")]
    [Range(0.1f, 1.0f)] public float handY = 0.5f;
    [Range(0.1f, 1.0f)] public float handX = 0.5f;
    [Range(0.1f, 1.0f)] public float handZ = 0.5f;

    // Bacak boyutları
    [Header("Bacak Boyutları")]
    [Range(0.5f, 3.0f)] public float legSizeX = 1.0f;
    [Range(0.5f, 3.0f)] public float legSizeY = 1.0f;
    [Range(0.5f, 3.0f)] public float legSizeZ = 1.0f;

    [Header("Ayak Boyutu Ayarı")]
    [Range(0.1f, 1.0f)] public float footY = 0.5f;
    [Range(0.1f, 1.0f)] public float footX = 0.5f;
    [Range(0.1f, 1.0f)] public float footZ = 0.5f;

    [Header("Omurga Boyutları ve Pozisyonları")]
    [Range(0.5f, 3.0f)] public float spineBellySize = 1.0f;
    [Range(0.5f, 3.0f)] public float spineMiddleSize = 1.0f;
    [Range(0.5f, 3.0f)] public float spineUpperSize = 1.0f;

    public float spineBellyY = 0.0f;
    public float spineMiddleY = 0.0f;
    public float spineUpperY = 0.0f;

    // Kafa ve boyun boyutları
    [Header("Kafa ve Boyun Boyutları")]
    [Range(0.5f, 3.0f)] public float headSize = 1.0f;
    [Range(0.5f, 3.0f)] public float neckSize = 1.0f;

    public float headY = 0.0f;
    public float neckY = 0.0f;

    // İskelet referansı
    [Header("İskelet Referansı")]
    public Transform skeletonRoot; // İskelet kökü (örn. "GoblinRigtest4")

    // Dinamik olarak referansları bulma
    private Transform FindSkeletonPart(string path)
    {
        if (skeletonRoot == null)
        {
            Debug.LogError("Skeleton root atanmadı! Lütfen iskelet kökünü belirtin.");
            return null;
        }
        var part = skeletonRoot.Find(path);
        if (part == null)
        {
            Debug.LogWarning($"'{path}' isimli iskelet parçası bulunamadı!");
        }
        return part;
    }

    private void OnValidate()
    {
        // İskeleti bul ve referansları güncelle
        var leftArm = FindSkeletonPart("Armature/spine/spine.001Belly/spine.002Middle/spine.003Upper/arm_L");
        var leftHand = FindSkeletonPart("Armature/spine/spine.001Belly/spine.002Middle/spine.003Upper/arm_L/ForeArm_L/arm_L.002Hand");
        var rightArm = FindSkeletonPart("Armature/spine/spine.001Belly/spine.002Middle/spine.003Upper/arm_R");
        var rightHand = FindSkeletonPart("Armature/spine/spine.001Belly/spine.002Middle/spine.003Upper/arm_R/ForeArm_R/arm_R.002Hand");

        var leftLeg = FindSkeletonPart("Armature/leg_L");
        var leftFoot = FindSkeletonPart("Armature/leg_L/Foot_L");
        var rightLeg = FindSkeletonPart("Armature/leg_R");
        var rightFoot = FindSkeletonPart("Armature/leg_R/Foot_R");

        var spineBelly = FindSkeletonPart("Armature/spine/spine.001Belly");
        var spineMiddle = FindSkeletonPart("Armature/spine/spine.002Middle");
        var spineUpper = FindSkeletonPart("Armature/spine/spine.003Upper");

        var head = FindSkeletonPart("Armature/spine/spine.005Head");
        var neck = FindSkeletonPart("Armature/spine/spine.004Neck");

        // Boyut ve pozisyonları güncelle
        UpdateLimbSize(leftArm, leftHand, armSizeX, armSizeY, armSizeZ, handX, handY, handZ);
        UpdateLimbSize(rightArm, rightHand, armSizeX, armSizeY, armSizeZ, handX, handY, handZ);

        UpdateLimbSize(leftLeg, leftFoot, legSizeX, legSizeY, legSizeZ, footX, footY, footZ);
        UpdateLimbSize(rightLeg, rightFoot, legSizeX, legSizeY, legSizeZ, footX, footY, footZ);

        UpdateSpineSize(spineBelly, spineBellySize);
        UpdateYPosition(spineBelly, spineBellyY);

        UpdateSpineSize(spineMiddle, spineMiddleSize);
        UpdateYPosition(spineMiddle, spineMiddleY);

        UpdateSpineSize(spineUpper, spineUpperSize);
        UpdateYPosition(spineUpper, spineUpperY);

        UpdateSpineSize(head, headSize);
        UpdateYPosition(head, headY);

        UpdateSpineSize(neck, neckSize);
        UpdateYPosition(neck, neckY);
    }

    private void UpdateLimbSize(Transform limb, Transform endPart, float sizeX, float sizeY, float sizeZ, float endPartX, float endPartY, float endPartZ)
    {
        if (limb != null)
        {
            limb.localScale = new Vector3(sizeX, sizeY, sizeZ);

            if (endPart != null)
            {
                endPart.localScale = new Vector3(sizeX * endPartX, sizeY * endPartY, sizeZ * endPartZ);
            }
        }
    }

    private void UpdateSpineSize(Transform spinePart, float size)
    {
        if (spinePart != null)
        {
            spinePart.localScale = new Vector3(size, size, size);
        }
    }

    private void UpdateYPosition(Transform part, float newY)
    {
        if (part != null)
        {
            Vector3 currentPosition = part.localPosition;
            part.localPosition = new Vector3(currentPosition.x, newY, currentPosition.z);
        }
    }
}
