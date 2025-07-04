using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class L110_Candle : MonoBehaviour
{
    public Level_110Ctrl levelCtrl;
    public Transform posSpawn;
    public void OnLitUp()
    {
        var candle = levelCtrl.SpawnFire(new Vector3(0.7f,1f,0.7f));
        candle.transform.SetParent(transform);
        candle.transform.position = posSpawn.position;
    }
}
