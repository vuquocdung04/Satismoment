using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class L15_BrokenEggShell : MonoBehaviour
{
    public float lifeTime = 3f;
    public float resetLifeTime = 3f;
    public float initialSpinForce = 10f;
    public Rigidbody2D rb;
    float randomTorque;
    private void Update()
    {
        Init();
    }


    void Init()
    {
        randomTorque = Random.Range(-initialSpinForce, initialSpinForce);
        rb.AddTorque(randomTorque);
        lifeTime -= Time.deltaTime;
        if(lifeTime < 0)
        {
            lifeTime = resetLifeTime;
            this.transform.localScale = Vector2.one;
            SimplePool2.Despawn(gameObject);
        }
    }
}
