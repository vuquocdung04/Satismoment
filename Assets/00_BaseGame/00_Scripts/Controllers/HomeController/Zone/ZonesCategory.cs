using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ZonesCategory : MonoBehaviour
{
    public int idCategory;
    public List<ZoneItem> lsZoneItems;

    public void Init()
    {
        var dataLevel = GameController.Instance.dataContain.dataLevel;
        int maxlevel = UseProfile.MaxUnlockedLevel;
        for(int i = 0; i < this.lsZoneItems.Count; i++)
        {
            if (lsZoneItems[i].idLevel < maxlevel)
            {
                lsZoneItems[i].imgIcon.gameObject.SetActive(false);
                lsZoneItems[i].iconLevel.sprite = dataLevel.lsZones[idCategory].lsItems[i].iconLevel;
            }
            else if (lsZoneItems[i].idLevel == maxlevel)
            {
                lsZoneItems[i].imgIcon.gameObject.SetActive(true);
                lsZoneItems[i].iconLevel.sprite = dataLevel.lsZones[idCategory].lsItems[i].iconLevel;
            }
            else if (lsZoneItems[i].idLevel == maxlevel + 1)
            {
                lsZoneItems[i].iconLevel.sprite = dataLevel.iconLv_Lock;
            }
            else
            {
                lsZoneItems[i].iconLevel.sprite = dataLevel.frameLock;
                lsZoneItems[i].btnPlay.enabled = false;
            }
        }
    }
}
