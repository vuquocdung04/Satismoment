using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum L21_BtnType
{
    Left, Right, Top, Bottom
}


public class L21_Btn : MonoBehaviour
{
    public L21_BtnType btnType;
    public SpriteRenderer spriteRenderer;
    public Sprite spriteDefault;
    public Sprite spriteClick;



}
