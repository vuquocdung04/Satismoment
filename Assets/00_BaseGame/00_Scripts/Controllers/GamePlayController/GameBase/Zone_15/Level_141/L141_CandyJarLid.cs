using Sirenix.OdinInspector;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class L141_CandyJarLid : MonoBehaviour
{
    public Rigidbody2D rb;
    public Transform decor;
    public float sizeX;
    Vector2 initialPosition;
    bool isRotate;


    private void Start()
    {
        initialPosition = transform.position;
    }
    public void MoveDecor(Vector3 moveSpeed, System.Action callback = null)
    {
        if (isRotate) return;
        decor.localPosition += new Vector3(moveSpeed.x,0,0);
        if(decor.localPosition.x <= -sizeX)
        {
            decor.localPosition = Vector3.zero;
        }
        transform.position += new Vector3(0,moveSpeed.x/20f,0);
        if(transform.position.y < initialPosition.y - 0.3f)
        {
            isRotate = true;
            gameObject.layer = 2;
            rb.bodyType = RigidbodyType2D.Dynamic;
            callback?.Invoke();
        }
    }

    [Button("Setup",ButtonSizes.Large)]
    void Setup()
    {
        decor = transform.Find("decor");
        sizeX = decor.GetComponent<SpriteRenderer>().bounds.size.x;
    }
}
