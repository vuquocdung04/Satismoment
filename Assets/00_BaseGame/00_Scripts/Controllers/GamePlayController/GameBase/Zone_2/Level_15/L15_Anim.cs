using DG.Tweening;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class L15_Anim : MonoBehaviour
{
    public Transform groundTargetPosition;
    public SpriteRenderer spritePet;
    public SpriteRenderer spriteEggShell;
    public SpriteRenderer eggShell;

    public List<Sprite> lsEggShells;
    public List<Sprite> lsPets;
    public Sprite spEggShell;
    enum L15_AnimType {Pet,EggShell }
    public void HandlePet()
    {
        spritePet.gameObject.SetActive(true);
        PlayAnimationSquense(L15_AnimType.Pet);
        
    }

    public void HandleEggShell()
    {
        PlayAnimationSquense(L15_AnimType.EggShell);

    }
    void PlayAnimationSquense(L15_AnimType type)
    {
        Sequence seq = DOTween.Sequence();

        switch (type)
        {
            case L15_AnimType.Pet:
                for (int i = 0; i < lsPets.Count; i++)
                {
                    int index = i;
                    seq.AppendCallback(() => spritePet.sprite = lsPets[index]);
                    seq.AppendInterval(1f); // 1f moi frame
                }
                break;
            case L15_AnimType.EggShell:
                spriteEggShell.sprite = lsEggShells[0];
                eggShell.gameObject.SetActive(true);
                eggShell.sprite = spEggShell;
                eggShell.gameObject.transform.position = new Vector2(0,0.2f);
                eggShell.sortingOrder = 6;
                RollToGround();
                break;
        }
    }

    void RollToGround()
    {
        eggShell.transform.DOMove(groundTargetPosition.position, 1.2f)
            .SetEase(Ease.OutQuad);

        eggShell.transform.DORotate(new Vector3(0, 0, -160f), 1.2f, RotateMode.LocalAxisAdd)
            .SetEase(Ease.InOutSine);
    }



    public void SimpleShake()
    {
        Vector3 originalRotation = spriteEggShell.transform.eulerAngles;
        eggShell.gameObject.SetActive(false);
        Sequence shakeSequence = DOTween.Sequence();

        shakeSequence.Append(spriteEggShell.transform.DORotate(new Vector3(0, 0, originalRotation.z + 15f), 0.2f).SetEase(Ease.InOutSine));
        shakeSequence.Append(spriteEggShell.transform.DORotate(new Vector3(0, 0, originalRotation.z - 15f), 0.2f).SetEase(Ease.InOutSine));
        shakeSequence.Append(spriteEggShell.transform.DORotate(originalRotation, 0.2f).SetEase(Ease.InOutSine));


        shakeSequence.OnComplete(() =>
        {
            HandleEggShell();
            HandlePet();
        });

        DOVirtual.DelayedCall(2.5f, () => WinBox.SetUp().Show());
    }

}
