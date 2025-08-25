

using UnityEngine;

public class GamePlayController : Singleton<GamePlayController>
{
    public GameScene gameScene;
    public LevelGameCtrl levelGameCtrl;
    public CameraAspectFitter cameraAspectFitter;
    protected override void OnAwake()
    {
        base.OnAwake();
        m_DontDestroyOnLoad = false;
        Init();
    }

    void Init()
    {
        cameraAspectFitter.Init();
        gameScene.Init();
        levelGameCtrl.Init();
        GameController.Instance.adsController.RequestBanner();
        GameController.Instance.adsController.RequestInterstitial();
    }
}

