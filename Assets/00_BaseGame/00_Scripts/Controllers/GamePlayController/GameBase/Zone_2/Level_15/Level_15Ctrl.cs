using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Level_15Ctrl : MonoBehaviour
{
    public L15_Anim anim;
    public Transform mask;
    public Transform eggShell;
    public int winProgress = 0;
    
    RaycastHit2D hit;
    Vector3 mousePos;
    Tween wobbleTween;


    private void Start()
    {
        StartWobble();
    }
    private void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            hit = Physics2D.Raycast(mousePos, Vector2.zero);

            if (hit.collider == null) return;

            StopWobble();

            winProgress++;
            if(winProgress % 3 == 0)
            {
                mask.transform.position += new Vector3(0.7f,0,0);
            }

            if(winProgress > 18)
            {
                eggShell.gameObject.SetActive(false);
                anim.HandleEggShell();
                anim.HandlePet();
            }
        }
    }

    void StartWobble()
    {
        wobbleTween = eggShell
            .DORotate(new Vector3(0, 0, 3), 0.5f)
            .SetEase(Ease.InOutSine)
            .SetLoops(-1, LoopType.Yoyo);
    }
    void StopWobble()
    {
        if (wobbleTween == null) return;
        wobbleTween.Kill();
        eggShell.rotation = Quaternion.identity;
    }
}
