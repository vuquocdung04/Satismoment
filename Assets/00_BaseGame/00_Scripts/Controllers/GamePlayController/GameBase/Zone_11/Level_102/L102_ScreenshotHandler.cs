using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
public class L102_ScreenshotHandler : MonoBehaviour
{
    public Level_102Ctrl levelCtrl;
    public SpriteRenderer spriteRenderer;
    public List<Sprite> lsFrames;
    private bool isClicked;
    bool isCatched;
    private void OnMouseDown()
    {
        if (levelCtrl.isWin) return;
        if (isClicked) return;
        TakeScreenshotAndAssign();
    }


    public void TakeScreenshotAndAssign()
    {
        StartCoroutine(CaptureScreenshot());
    }

    private IEnumerator CaptureScreenshot()
    {
        
        isClicked = true;
        levelCtrl.PlayScreenShotSound();
        isCatched = levelCtrl.cat.IsPeeking();
        spriteRenderer.sprite = lsFrames[0];
        levelCtrl.FadingEffect();
        levelCtrl.HideObj();
        yield return levelCtrl.HideEffect().WaitForCompletion();
        yield return new WaitForEndOfFrame();
        Texture2D screenshotTexture = new Texture2D(Screen.width, Screen.height, TextureFormat.RGB24, false);
        screenshotTexture.ReadPixels(new Rect(0, 0, Screen.width, Screen.height), 0, 0);
        screenshotTexture.Apply();

        // Tạo một Sprite từ Texture2D
        Sprite newSprite = Sprite.Create(screenshotTexture, new Rect(0, 0, screenshotTexture.width, screenshotTexture.height), new Vector2(0.5f, 0.5f),300f);
        StartCoroutine(levelCtrl.framePicture.HandleAction(newSprite));
        levelCtrl.ShowObj();
        if (isCatched)
        {
            Debug.LogError("catch");
            levelCtrl.MovingMask();
            levelCtrl.winProgress++;
            if(levelCtrl.winProgress == 5)
            {
                StartCoroutine(levelCtrl.HandleWinCondition());
            }
        }
        else
        {
            Debug.LogError("catch fail");
        }
        yield return new WaitForSeconds(1f);
        spriteRenderer.sprite = lsFrames[1];
        isClicked = false;

    }
}