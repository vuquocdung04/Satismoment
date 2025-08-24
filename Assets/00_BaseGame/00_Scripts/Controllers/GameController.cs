
using _00_BaseGame._00_Scripts.Controllers.MusicManager;
using UnityEngine;

public class GameController : MonoBehaviour
{
    private static GameController _instance;
    public static GameController Instance
    {
        get
        {
            if(_instance == null)
            {
                _instance = FindAnyObjectByType<GameController>();
            }
            return _instance;
        }
    }
    protected void Awake()
    {
        if(_instance == null)
        {
            _instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else if (_instance != this)
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
        adsController.Init();
        musicManager.Init();
        dataContain.Init();
        startLoading.Init();
    }

}
