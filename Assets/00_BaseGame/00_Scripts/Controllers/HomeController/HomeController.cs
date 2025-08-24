
using UnityEngine;
using Sirenix.OdinInspector;

public class HomeController : Singleton<HomeController>
{
    public HomeScene homeScene;

    protected override void OnAwake()
    {
        base.OnAwake();
        m_DontDestroyOnLoad = false;
    }

    private void Start()
    {
        homeScene.Init();
        GameController.Instance.adsController.ShowBanner();
    }
    private void OnDestroy()
    {
        if (GameController.Instance != null && 
            GameController.Instance.adsController != null)
        {
            GameController.Instance.adsController.HideBanner();
        }
    }
    
    
    [Button("Test level", ButtonSizes.Large)]
    private void NextLevel(int levelID)
    {
        var levelMax = UseProfile.MaxUnlockedLevel;
        if (levelID > levelMax)
        {
            Debug.LogError("Error max level");
            return;
        }
        UseProfile.CurrentLevel = levelID;
        Initiate.Fade(SceneName.GAME_PLAY,Color.black, 3f);
    }
}
