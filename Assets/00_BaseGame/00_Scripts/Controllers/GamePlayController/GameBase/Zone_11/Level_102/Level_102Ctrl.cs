using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Level_102Ctrl : MonoBehaviour
{
    public Transform frame;
    public Transform timmingBar;
    public Transform maskTimingBar;
    public L102_FramePicture framePicture;


    public void HideObj()
    {
        frame.gameObject.SetActive(false);
        timmingBar.gameObject.SetActive(false);
    }
    public void ShowObj()
    {
        frame.gameObject.SetActive(true);
        timmingBar.gameObject.SetActive(true);
    }
    public void MovingMask()
    {
        maskTimingBar.DOMoveX(maskTimingBar.transform.position.x + 0.45f,0.2f).SetEase(Ease.Linear);
    }
}
