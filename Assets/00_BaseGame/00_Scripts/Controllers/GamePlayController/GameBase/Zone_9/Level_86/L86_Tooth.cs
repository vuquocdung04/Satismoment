using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class L86_Tooth : MonoBehaviour
{
    
    public int idTooth;
    public BoxCollider2D boxCollider2d;
    public void HiddenTooth()
    {
        boxCollider2d.enabled = false;
        gameObject.SetActive(false);
    }
    public void ResetState()
    {
        gameObject.SetActive(true);
        boxCollider2d.enabled = true;
    }
}
