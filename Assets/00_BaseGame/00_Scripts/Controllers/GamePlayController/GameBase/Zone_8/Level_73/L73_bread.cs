using DG.Tweening;
using UnityEngine;

public class L73_bread : MonoBehaviour
{
    private SpriteRenderer spriteRenderer;
    public Sprite sprite;
    private void Start()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
    }


    public void DoActionBeforeToasting(System.Action callback = null)
    {
        transform.DOMoveY(1.146f,0.5f).SetEase(Ease.OutBounce).OnComplete(delegate
        {
            callback?.Invoke();
        });
        spriteRenderer.sprite = sprite;
    }
}
