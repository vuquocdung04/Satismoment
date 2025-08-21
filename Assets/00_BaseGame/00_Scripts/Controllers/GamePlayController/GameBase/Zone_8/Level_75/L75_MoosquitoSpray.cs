
using UnityEngine;

public class L75_MoosquitoSpray : MonoBehaviour
{
    public Transform sprayEffect;
    public void StartSpray()
    {
        sprayEffect.gameObject.SetActive(true);
    }

    public void StopSpray()
    {
        sprayEffect.gameObject.SetActive(false);
    }
}
