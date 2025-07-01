using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class L104_SoftDrink : MonoBehaviour
{
    public BoxCollider2D boxCollider2D;
    public Transform icon;
    public float maxDistanceX;
    public float posIconCorrect;

    public bool CheckDistanceCorrect()
    {
        float distanceX = icon.localPosition.x - posIconCorrect;

        // Làm tròn đến bội số gần nhất của maxDistanceX
        float multiple = Mathf.Round(distanceX / maxDistanceX);
        float snappedValue = multiple * maxDistanceX;

        // Kiểm tra nếu gần một bội số hợp lệ
        if (Mathf.Abs(distanceX - snappedValue) <= 0.1f)
        {
            // Chỉnh vị trí về điểm gần nhất
            icon.localPosition = new Vector2(posIconCorrect + snappedValue, icon.localPosition.y);

            boxCollider2D.enabled = false;
            return true;
        }

        return false;
    }


    public void Init()
    {
        //icon = transform.Find("icon");
        //maxDistanceX = icon.transform.GetComponent<SpriteRenderer>().bounds.size.x;
        //boxCollider2D = GetComponent<BoxCollider2D>();
        posIconCorrect = icon.localPosition.x;
    }
}
