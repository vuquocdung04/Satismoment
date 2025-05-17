using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class L15_Anim : MonoBehaviour
{
    public SpriteRenderer spritePet;
    public SpriteRenderer spriteEggShell;

    public List<Sprite> lsEggShells;
    public List<Sprite> lsPets;

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
                for (int i = 0; i < lsEggShells.Count; i++)
                {
                    int index = i;
                    seq.AppendCallback(() => spriteEggShell.sprite = lsEggShells[index]);
                    seq.AppendInterval(1f); // 1f moi frame
                }
                break;
        }


        
    }

}
