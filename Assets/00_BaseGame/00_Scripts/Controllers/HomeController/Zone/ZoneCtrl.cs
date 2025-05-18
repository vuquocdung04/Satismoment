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
        int index = 1;
        for(int i = 0; i < lsZoneCategorys.Count; i++)
        {
            lsZoneCategorys[i].idCategory = i;
            for(int j = 0; j < lsZoneCategorys[i].lsZoneItems.Count; j++)
            {
                lsZoneCategorys[i].lsZoneItems[j].idLevel = index;
                index++;
            }
        }
    }
}
