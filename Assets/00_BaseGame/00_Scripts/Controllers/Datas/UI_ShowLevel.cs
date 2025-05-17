using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Sirenix.OdinInspector;

[CreateAssetMenu(menuName = "SO/Display UI")]
public class UI_ShowLevel : ScriptableObject
{
    [PreviewField(50)]
    public Sprite frameLock;
    [PreviewField(50)]
    public Sprite iconLv_Lock;
    [Space(10)]
    public List<UI_ShowZone> lsZones;

    [Button("SetUp", ButtonSizes.Large)]
    void SetUp()
    {
        int currentLevel = 1;
        for(int i = 0; i < lsZones.Count; i++)
        {
            for(int j = 0; j < lsZones[i].lsItems.Count; j++)
            {
                lsZones[i].lsItems[j].level = currentLevel;
                currentLevel++;
            }
        }
    }
}

[System.Serializable]
public class UI_ShowZone
{
    public List<UI_ShowZoneItem> lsItems;
}

[System.Serializable]
public class UI_ShowZoneItem
{
    public int level;
    [PreviewField(50)]
    public Sprite iconLevel;
    public GameObject levelGame;
}
