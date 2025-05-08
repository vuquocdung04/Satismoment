using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LoadAutoComponents : MonoBehaviour
{
    private void Reset()
    {
        this.LoadComponents();
    }

    protected virtual void LoadComponents()
    {
        //overide
    }
}
