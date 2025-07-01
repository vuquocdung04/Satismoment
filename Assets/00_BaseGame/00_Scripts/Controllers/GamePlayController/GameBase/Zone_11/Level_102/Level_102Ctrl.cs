using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Level_102Ctrl : MonoBehaviour
{
    public int winProgress;
    public bool isWin = false;
    public Transform frame;
    public Transform timmingBar;
    public Transform maskTimingBar;
    public L102_Cat cat;
    public SpriteRenderer effect;
    public L102_FramePicture framePicture;
    public List<L102_Animal> lsAnimals;

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
        if(winProgress <= 5)
        maskTimingBar.DOMoveX(maskTimingBar.transform.position.x + 0.45f,0.2f).SetEase(Ease.Linear);
    }

    public void FadingEffect()
    {
        effect.DOFade(1f,0.1f);
    }
    public Tween HideEffect()
    {
        return effect.DOFade(0f, 0.1f);
    }



    public IEnumerator HandleWinCondition()
    {
        isWin = true;
        foreach(var animal in this.lsAnimals)
        {
            animal.StopAll();
        }
        cat.StopAll();

        yield return new WaitForSeconds(2f);
        WinBox.SetUp().Show();

    }
}
