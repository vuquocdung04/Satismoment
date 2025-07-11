using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class L126_Fruit : MonoBehaviour
{
    public int id = 0;
    public Rigidbody2D rb;

    public void Falling()
    {
        rb.bodyType = RigidbodyType2D.Dynamic;
    }
    public void Init()
    {
        rb.bodyType = RigidbodyType2D.Kinematic;
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        var otherFruit = collision.collider.GetComponent<L126_Fruit>();
        if (otherFruit == null) return;
        if (otherFruit.id != id) return;
        if (GetInstanceID() > otherFruit.GetInstanceID()) return;

        int idClone = id + 1;
        var posSpawn = transform.position + otherFruit.transform.position;
        Level_126Ctrl.Instance.GetFruitWithId(idClone,posSpawn/2f);
        Debug.LogError("Check");
        SimplePool2.Despawn(gameObject);
        SimplePool2.Despawn(otherFruit.gameObject);
    }

}
