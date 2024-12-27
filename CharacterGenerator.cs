using UnityEngine;

public class CharacterGenerator : MonoBehaviour
{
    [Header("Kol Boyutları")]
    [Range(0.5f, 3.0f)] public float armSizeX = 1.0f;
    [Range(0.5f, 3.0f)] public float armSizeY = 1.0f;
    [Range(0.5f, 3.0f)] public float armSizeZ = 1.0f;

    [Header("El Boyutu Ayarı")]
    [Range(0.1f, 1.0f)] public float handY = 0.5f;
    [Range(0.1f, 1.0f)] public float handX = 0.5f;
    [Range(0.1f, 1.0f)] public float handZ = 0.5f;

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

    [Header("Kafa ve Boyun Boyutları")]
    [Range(0.5f, 3.0f)] public float headSize = 1.0f;
    [Range(0.5f, 3.0f)] public float neckSize = 1.0f;
    public float headY = 0.0f;
    public float neckY = 0.0f;

    private Transform leftArm, rightArm, leftHand, rightHand;
    private Transform leftLeg, rightLeg, leftFoot, rightFoot;
    private Transform spineBelly, spineMiddle, spineUpper;
    private Transform head, neck;

    private void Awake()
    {
        AutoAssignReferences();
    }

    private void OnValidate()
    {
        // Kollar ve eller
        UpdateLimbSize(leftArm, leftHand, armSizeX, armSizeY, armSizeZ, handX, handY, handZ);
        UpdateLimbSize(rightArm, rightHand, armSizeX, armSizeY, armSizeZ, handX, handY, handZ);

        // Bacaklar ve ayaklar
        UpdateLimbSize(leftLeg, leftFoot, legSizeX, legSizeY, legSizeZ, footX, footY, footZ);
        UpdateLimbSize(rightLeg, rightFoot, legSizeX, legSizeY, legSizeZ, footX, footY, footZ);

        // Omurga
        UpdateSpineSize(spineBelly, spineBellySize, spineBellyY);
        UpdateSpineSize(spineMiddle, spineMiddleSize, spineMiddleY);
        UpdateSpineSize(spineUpper, spineUpperSize, spineUpperY);

        // Kafa ve boyun
        UpdateSpineSize(head, headSize, headY);
        UpdateSpineSize(neck, neckSize, neckY);
    }

    private void AutoAssignReferences()
    {
        leftArm = transform.Find("LeftArm");
        rightArm = transform.Find("RightArm");
        leftHand = transform.Find("LeftArm/LeftHand");
        rightHand = transform.Find("RightArm/RightHand");

        leftLeg = transform.Find("LeftLeg");
        rightLeg = transform.Find("RightLeg");
        leftFoot = transform.Find("LeftLeg/LeftFoot");
        rightFoot = transform.Find("RightLeg/RightFoot");

        spineBelly = transform.Find("Spine/SpineBelly");
        spineMiddle = transform.Find("Spine/SpineMiddle");
        spineUpper = transform.Find("Spine/SpineUpper");

        head = transform.Find("Head");
        neck = transform.Find("Neck");
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

    private void UpdateSpineSize(Transform part, float size, float newY)
    {
        if (part != null)
        {
            part.localScale = new Vector3(size, size, size);
            Vector3 currentPosition = part.localPosition;
            part.localPosition = new Vector3(currentPosition.x, newY, currentPosition.z);
        }
    }
}
