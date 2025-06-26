using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class L89_TriggerRestHouse : MonoBehaviour
{

    public void Init()
    {
        transform.SetParent(Camera.main.transform);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        var houseClone = collision.GetComponent<L89_HousePrefab>();
        if (houseClone == null) return;
        if (houseClone.isComplete) return;
        houseClone.ResetStateAndSetParent();
    }
}
