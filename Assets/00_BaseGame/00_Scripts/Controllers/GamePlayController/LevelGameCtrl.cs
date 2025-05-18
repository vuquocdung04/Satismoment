using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelGameCtrl : MonoBehaviour
{
    public void Init()
    {
        var levelData = GameController.Instance.dataContain.dataLevel;
        int selectedLevel = UseProfile.SelectedLevel;

        int levelPerZone = 10;

        int zoneIndex = (selectedLevel - 1) / levelPerZone;

        // 8 % 10 => 8, 
        int itemIndexInZone = (selectedLevel - 1) % levelPerZone;

        if (zoneIndex >= levelData.lsZones.Count) return;

        var currentZone = levelData.lsZones[zoneIndex];
        if (itemIndexInZone >= currentZone.lsItems.Count) return;

        Instantiate(currentZone.lsItems[itemIndexInZone].levelGame);

    }
}
