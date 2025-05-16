using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class StartLoading : MonoBehaviour
{
    public TextMeshProUGUI txtLoading;
    public Image progressBar;
    public bool wasCoolDown;
    Coroutine coroutineLoad;
    public int countSecond;
    public Image bgAim;
    public List<Sprite> lsAnim;
    public void Init()
    {
        this.wasCoolDown = false;
        this.progressBar.fillAmount = 0;
        this.countSecond = 0;
        coroutineLoad = StartCoroutine(LoadAdsToChangeScene());
        StartCoroutine(LoadingText());
        StartCoroutine(LoadingAnimBG());
    }

    IEnumerator ChangeScene()
    {
        yield return new WaitForSeconds(1);
        progressBar.fillAmount = 0;
        string name = "";
        name =SceneName.HOME_SCENE;
        var _asyncOperation = SceneManager.LoadSceneAsync(name, LoadSceneMode.Single);
        while (!_asyncOperation.isDone)
        {
            progressBar.fillAmount = Mathf.Clamp01(_asyncOperation.progress / 0.9f);
            Debug.LogError(progressBar.fillAmount);
            yield return null;
        }
    }

    IEnumerator LoadAdsToChangeScene()
    {
        yield return new WaitForSeconds(1);
        StartCoroutine(ChangeScene());
    }

    IEnumerator LoadingText()
    {
        var wait = new WaitForSeconds(1);

        while (true)
        {
            txtLoading.text = "LOADING.";
            yield return wait;

            txtLoading.text = "LOADING..";
            yield return wait;

            txtLoading.text = "LOADING...";
            yield return wait;
        }
    }

    IEnumerator LoadingAnimBG()
    {
        var wait = new WaitForSeconds(0.8f);
        while (true)
        {
            bgAim.sprite = lsAnim[0];
            yield return wait;
            bgAim.sprite = lsAnim[1];
            yield return wait;
        }
    }
}
