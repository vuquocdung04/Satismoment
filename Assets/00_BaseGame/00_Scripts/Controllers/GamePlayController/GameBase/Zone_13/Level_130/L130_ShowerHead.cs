using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class L130_ShowerHead : MonoBehaviour
{
    public Transform cold;
    public Transform hot;

    public void ActiveHotEffect()
    {
        hot.gameObject.SetActive(true);
        cold.gameObject.SetActive(false);
    }
    public void ActiveColdEffect()
    {
        cold.gameObject.SetActive(true);
        hot.gameObject.SetActive(false);
    }
    public void DeactiveAllEffects()
    {
        cold.gameObject.SetActive(false);
        hot.gameObject.SetActive(false);
    }
}
