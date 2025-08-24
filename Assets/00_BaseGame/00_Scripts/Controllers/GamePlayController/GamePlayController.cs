

public class GamePlayController : Singleton<GamePlayController>
{
    public GameScene gameScene;
    public LevelGameCtrl levelGameCtrl;
    protected override void OnAwake()
    {
        base.OnAwake();
        m_DontDestroyOnLoad = false;
        Init();
    }

    void Init()
    {
        gameScene.Init();
        levelGameCtrl.Init();
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
}

