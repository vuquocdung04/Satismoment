using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Sirenix.OdinInspector;

public class ZoneCtrl : MonoBehaviour
{
    public List<ZonesCategory> lsZoneCategorys;
    public void Init()
    {
        foreach(var zone in this.lsZoneCategorys)
        {
            zone.Init();
        }
    }


    [Button("SetUp id", ButtonSizes.Large)]
    void SetUp()
    {
        for(int i = 0; i < lsZoneCategorys[0].lsZoneItems.Count; i++)
        {
            lsZoneCategorys[0].lsZoneItems[i].idLevel = i + 1;
        }
    }
}
