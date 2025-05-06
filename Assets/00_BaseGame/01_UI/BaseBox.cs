using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Sirenix.OdinInspector;
using DG.Tweening;
using UnityEngine.UI;
using UnityEngine.Events;

public abstract class BaseBox : MonoBehaviour
{
    [SerializeField] protected RectTransform mainPanel;
    [SerializeField] protected bool isAnim = true;

    #region Anim Init
    protected virtual void OnEnable()
    {
        DoAppear();
        OnStart();
    }

    protected virtual void DoAppear()
    {
        if (!isAnim) return;
        if (mainPanel == null) return;

        mainPanel.localScale = Vector3.zero;
        mainPanel.DOScale(1, 0.5f).SetUpdate(true).SetEase(Ease.OutBack);
    }

    protected virtual void OnStart()
    {

    }

    #endregion


    public virtual void Show()
    {
        gameObject.SetActive(true);
    }

    public virtual void Close()
    {
        gameObject.SetActive(false);
    }
}
