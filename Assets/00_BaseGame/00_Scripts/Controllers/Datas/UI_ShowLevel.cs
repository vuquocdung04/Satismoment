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
        for (int i = 0; i < 10; i++)
        {
            lsZones[0].lsItems[i].level = i + 1;
            //lsZones[1].lsItems[i].level = i + 11;
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
