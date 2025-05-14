using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class L4_Item : LoadAutoComponents
{
    public BoxCollider2D boxCollider2D;
    Vector3 newPos;
    private void Update()
    {

        if(this.transform.position.x > 1f)
        {
            newPos = this.transform.position;
            newPos.x = 1f;
            this.transform.position = newPos;
        }
        else if (this.transform.position.x < -1f)
        {
            newPos = this.transform.position;
            newPos.x = -1f;
            this.transform.position = newPos;
        }
    }


    protected override void LoadComponents()
    {
        base.LoadComponents();
        boxCollider2D = GetComponent<BoxCollider2D>();
    }
}
