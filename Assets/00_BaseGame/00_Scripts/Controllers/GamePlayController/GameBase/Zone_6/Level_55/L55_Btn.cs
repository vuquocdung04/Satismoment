using Sirenix.OdinInspector;
using UnityEngine;
using UnityEngine.UI;

public class L55_Btn : MonoBehaviour
{
    public int idBtn;
    public Image icon;
    public Image progress;

    public Vector3 GetWorldPosition()
    {
        RectTransform rt = icon.GetComponent<RectTransform>();
        Vector2 screenPos = RectTransformUtility.WorldToScreenPoint(null, rt.position);
        Ray ray = Camera.main.ScreenPointToRay(screenPos);
        float distance = (0 - ray.origin.z) / ray.direction.z;
        return ray.GetPoint(distance);
    } 

    [Button("Setup", ButtonSizes.Large)]
    void Setup()
    {
        icon = transform.Find("icon").GetComponent<Image>();
        progress = transform.Find("progress").GetComponent <Image>();
    }



}
