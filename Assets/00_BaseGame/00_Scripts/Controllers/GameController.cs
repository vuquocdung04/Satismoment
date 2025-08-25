
using _00_BaseGame._00_Scripts.Controllers.MusicManager;
using UnityEngine;

public class GameController : MonoBehaviour
{
    private static GameController instance;
    public static GameController Instance
    {
        get
        {
            if(instance == null)
            {
                instance = FindAnyObjectByType<GameController>();
            }
            return instance;
        }
    }
    protected void Awake()
    {
        if(instance == null)
        {
            instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else if (instance != this)
        {
            Destroy(gameObject);
        }
        Init();

        DontDestroyOnLoad(this);
    }
    public StartLoading startLoading;
    public UseProfile useProfile;
    public DataContain dataContain;
    public MusicManagerBase musicManager;
    public AdsController adsController;
    public ConfettiEffectController confettiEffectController;
    void Init()
    {
        ResetTimeScale();
        Application.targetFrameRate = 60;
        useProfile.IsRemoveAds = true;
        adsController.Init();
        musicManager.Init();
        dataContain.Init();
        startLoading.Init();
    }

    public void ChangeScene(string sceneName, float duration = 3f)
    {
        ResetTimeScale();
        adsController.DestroyBanner();
        Initiate.Fade(sceneName, Color.black, duration);
    }

    public void ResetTimeScale()
    {
        Time.timeScale = 1;
    }

    public void PauseTimeScale()
    {
        Time.timeScale = 0;
    }
    
}
