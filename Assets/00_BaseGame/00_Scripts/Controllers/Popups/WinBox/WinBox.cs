
using System;
using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class WinBox : BaseBox
{
    static WinBox instance;
    public static WinBox SetUp()
    {
        if (instance == null)
        {
            instance = Instantiate(Resources.Load<WinBox>(PathPrefabs.WIN_BOX));
            instance.Init();
        }
        instance.InitState();
        return instance;
    }

    public Button btnNext;
    public Button btnRestart;
    public Button btnHome;
    public CharactorAnim anim;
    public Image panel;
    public Transform posSpawn;
    private bool isReady;
    void Init()
    {
        anim.Init();

        btnNext.onClick.AddListener(delegate{AntiSpamBtn(GoToNextLevel);});
        btnRestart.onClick.AddListener(delegate { AntiSpamBtn(OnClickRestart);});
        btnHome.onClick.AddListener(delegate{AntiSpamBtn(GoToHomeScene);});
    }
    void InitState()
    {
        isReady = false;
        //Game max 185 = max level
        if(UseProfile.CurrentLevel == UseProfile.MaxUnlockedLevel && UseProfile.MaxUnlockedLevel <= 185)
        {
            UseProfile.MaxUnlockedLevel++;
        }
        if (UseProfile.CurrentLevel == 185 && UseProfile.MaxUnlockedLevel == 185)
        {
            btnNext.gameObject.SetActive(false);
            Debug.Log("Unlock Full Game");
        }
        else
        {
            btnNext.gameObject.SetActive(true);
        }
    }

    protected override void DoAppear()
    {
        StartCoroutine(DoShowingPopup(delegate
        {
            GameController.Instance.adsController.ShowInterstitial();
            if (!GameController.Instance.useProfile.IsRemoveAds)
            {
                btnHome.enabled = true;
                btnNext.enabled = true;
                btnRestart.enabled = true;
            }
            else
            {
                Debug.Log("Da mua goi remove ads");
            }
        }));
    }

    // ReSharper disable Unity.PerformanceAnalysis
    IEnumerator DoShowingPopup(Action callback = null)
    {
        ThumbUpBox.SetUp().Show();
        yield return new WaitForSeconds(1f);
        ThumbUpBox.SetUp().Close();
        panel.color = new Color32(0, 0, 0, 215);
        GameController.Instance.musicManager.PlayWinLevelSound();
        HandleStateBtn();
        base.DoAppear();
        yield return new WaitForSeconds(0.75f);
        callback?.Invoke();
    }

    private void HandleStateBtn(bool isActive = true)
    {
        if (GameController.Instance.useProfile.IsRemoveAds)
        {
            btnHome.enabled = isActive;
            btnNext.enabled = isActive;
            btnRestart.enabled = isActive;
        }
        else
        {
            btnHome.enabled = !isActive;
            btnNext.enabled = !isActive;
            btnRestart.enabled = !isActive;
        }
    }


    // ReSharper disable Unity.PerformanceAnalysis
    private void GoToNextLevel()
    {
        Next();

        void Next()
        {
            if (UseProfile.CurrentLevel <= UseProfile.MaxUnlockedLevel &&  UseProfile.CurrentLevel <= 185)
                UseProfile.CurrentLevel++;
            GameController.Instance.musicManager.PlayUIClick();
            GameController.Instance.ChangeScene(SceneName.GAME_PLAY);
        }
    }

    // ReSharper disable Unity.PerformanceAnalysis
    private void GoToHomeScene()
    {
        GameController.Instance.musicManager.PlayUIClick();
        GameController.Instance.ChangeScene(SceneName.HOME_SCENE);
    }

    // ReSharper disable Unity.PerformanceAnalysis
    private void OnClickRestart()
    {
        GameController.Instance.musicManager.PlayUIClick();
        GameController.Instance.ChangeScene(SceneName.GAME_PLAY);
    }


    private void AntiSpamBtn(Action callback = null)
    {
        if (!isReady)
        {
            isReady = true;
            callback?.Invoke();
        }
        else
        {
            Debug.LogError("Spawn it thoi =))");
        }
    }
    
}
