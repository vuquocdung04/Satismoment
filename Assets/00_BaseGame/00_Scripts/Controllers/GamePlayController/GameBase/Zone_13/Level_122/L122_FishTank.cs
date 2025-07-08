using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class L122_FishTank : MonoBehaviour
{

    public Transform waterInFishTank;
    float maxLimitY = -0.1f;
    void MoveWater(float movementAmount)
    {
        if (IsFishTankFull()) return;
        waterInFishTank.localPosition += Vector3.up * movementAmount;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        var waterDrop = collision.GetComponent<L122_WaterDrop>();
        if (waterDrop == null) return;
        MoveWater(0.05f);
    }

    public bool IsFishTankFull()
    {
        if(waterInFishTank.localPosition.y >= maxLimitY) return true;
        return false;
    }

    
}
