using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class L124_ObjDragable : MonoBehaviour
{
    public Level_124Ctrl levelCtrl;
    public L124_ObjType objType;
    public BoxCollider2D objCollider;
    public SpriteRenderer objRenderer;
    public abstract void HandleCollisionWithObj();
}
