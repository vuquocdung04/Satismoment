
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
    void Init()
    {
        anim.Init();

        btnNext.onClick.AddListener(HandleNext);
        btnRestart.onClick.AddListener(OnClickRestart);
        btnHome.onClick.AddListener(OnClickHome);
    }
    void InitState()
    {

    }

    protected override void DoAppear(Action callback = null)
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
        yield return new WaitForSeconds(0.3f);
        base.DoAppear(callback);
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
    private void HandleNext()
    {
        Next();

        void Next()
        {
            if(UseProfile.CurrentLevel == UseProfile.MaxUnlockedLevel)
            {
                UseProfile.MaxUnlockedLevel++;
            }
            UseProfile.CurrentLevel++;
            GameController.Instance.musicManager.PlayUIClick();
            GameController.Instance.ChangeScene(SceneName.GAME_PLAY);
        }
    }

    // ReSharper disable Unity.PerformanceAnalysis
    private void OnClickHome()
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

    
}
