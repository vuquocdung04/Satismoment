using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class L21_Pinecone : MonoBehaviour
{
    public Level_21Ctrl levelCtrl;
    void OnTriggerEnter2D(Collider2D collision)
    {
        levelCtrl.isWin = true;
        levelCtrl.squirriel.ChangeAnimWin();
        Destroy(gameObject);
    }
}
