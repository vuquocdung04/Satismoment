using UnityEngine;
using UnityEngine.UI;

public class HintBox : BaseBox
{
    public static HintBox instance;

    public static HintBox SetUp()
    {
        if(instance == null)
        {
            instance = Instantiate(Resources.Load<HintBox>(PathPrefabs.HINT_BOX));
            instance.Init();
        }
        instance.InitState();
        return instance;
    }

    public Button btnClose;
    public Image hintImage;
    
    void Init()
    {
        btnClose.onClick.AddListener(Close);
        SetHintImage();
    }
    void InitState()
    {

    }

    void SetHintImage()
    {
        var levelData = GameController.Instance.dataContain.dataLevel;
        int currentLevel = UseProfile.CurrentLevel;
        
        int levelPerZone = 10;

        int zoneIndex = (currentLevel - 1) / levelPerZone;
        
        // 8 % 10 => 8, 
        int itemIndexInZone = (currentLevel - 1) % levelPerZone;

        if (zoneIndex >= levelData.lsZones.Count) return;
        
        var currentZone = levelData.lsZones[zoneIndex];
        
        if (itemIndexInZone >= currentZone.lsItems.Count) return;

        Sprite hintSprite = currentZone.lsItems[itemIndexInZone].hintLevel;
        if (hintSprite != null)
        {
            hintImage.sprite = hintSprite;
            hintImage.SetNativeSize();
        }
        else
        {
            Debug.LogError("Sprite level is null");
        }
    }
}
