using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static UnityEngine.GraphicsBuffer;

public class L120_LightBulb : MonoBehaviour
{
    public float moveLimit;
    public Transform bulbEnvelope;
    public Transform filament;
    public Transform lightYellow;
    public BoxCollider2D boxCollider;

    public void RotateFilament(float rotationAmount)
    {
        if (IsRotationCompleted()) return;
        filament.Rotate(0, rotationAmount, 0);
    }
    public void MoveBulbEnvelope(float movementAmount, Vector3 newPos)
    {
        if (IsRotationCompleted()) return;

        newPos = bulbEnvelope.localPosition + Vector3.up * movementAmount;
        newPos.y = Mathf.Clamp(newPos.y, moveLimit, moveLimit + 0.25f);
        bulbEnvelope.localPosition = newPos;
    }

    public bool IsRotationCompleted()
    {

        float distanceY = bulbEnvelope.localPosition.y - (moveLimit + 0.25f);
        Debug.LogError(distanceY);
        if (Mathf.Abs(distanceY) < 0.02) return true;
        return false;
    }


    public void DoAnimCompleted()
    {
        lightYellow.gameObject.SetActive(true);
        boxCollider.enabled = false;
    }

    public void InitSetup()
    {
        boxCollider = GetComponent<BoxCollider2D>();
        bulbEnvelope = transform.Find("Bulb Envelope");
        filament = transform.Find("Bulb Envelope").Find("Filament");
        lightYellow = transform.Find("Bulb Envelope").Find("light");
        lightYellow.gameObject.SetActive(false);
        moveLimit = bulbEnvelope.transform.localPosition.y;
    }
}
