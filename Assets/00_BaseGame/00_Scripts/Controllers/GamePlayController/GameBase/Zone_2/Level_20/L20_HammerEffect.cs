using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class L20_HammerEffect : MonoBehaviour
{
    public float lifeTime = 1f;
    public float resetLifeTime = 1f;
    private void Update()
    {
        lifeTime -= Time.deltaTime;
        if(lifeTime < 0)
        {
            lifeTime = resetLifeTime;
            SimplePool2.Despawn(gameObject);
        }
    }
}
