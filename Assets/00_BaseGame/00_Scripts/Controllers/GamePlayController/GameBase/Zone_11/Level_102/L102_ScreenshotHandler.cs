using UnityEngine;
using System.Collections;
public class L102_ScreenshotHandler : MonoBehaviour
{
    public Level_102Ctrl levelCtrl;
    private bool isClicked;
    private void OnMouseDown()
    {
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
        yield return new WaitForEndOfFrame();
        Texture2D screenshotTexture = new Texture2D(Screen.width, Screen.height, TextureFormat.RGB24, false);
        screenshotTexture.ReadPixels(new Rect(0, 0, Screen.width, Screen.height), 0, 0);
        screenshotTexture.Apply();

        // Tạo một Sprite từ Texture2D
        Sprite newSprite = Sprite.Create(screenshotTexture, new Rect(0, 0, screenshotTexture.width, screenshotTexture.height), new Vector2(0.5f, 0.5f),300f);
        levelCtrl.MovingMask();
        yield return StartCoroutine(levelCtrl.framePicture.HandleAction(newSprite));
        isClicked = false;

    }
}