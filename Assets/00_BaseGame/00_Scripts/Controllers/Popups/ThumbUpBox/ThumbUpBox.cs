using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;
public class ThumbUpBox : BaseBox
{
    static ThumbUpBox instance;
    public static ThumbUpBox SetUp()
    {
        if (instance == null)
        {
            instance = Instantiate(Resources.Load<ThumbUpBox>(PathPrefabs.THUMB_UP_BOX));
            instance.Init();
        }
        instance.InitState();
        return instance;
    }

    public RectTransform thumbUp;

    void Init()
    {
        AnimThumbUp();
    }

    void InitState()
    {

    }

    void AnimThumbUp()
    {
        thumbUp.DORotate(new Vector3(0,0,30),0.3f)
            .SetLoops(-1,LoopType.Yoyo)
            .SetEase(Ease.InOutSine);
    }
}
