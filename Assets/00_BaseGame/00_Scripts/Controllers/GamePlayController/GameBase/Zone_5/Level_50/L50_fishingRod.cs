using UnityEngine;

public class L50_fishingRod : MonoBehaviour
{
    public bool rotationEnabled = true;
    public float swingSpeed = 15f;
    public float maxSwingAngle = 30f;
    private float currentPingPongTime = 0f;

    float pingPongValue;
    float currentSwingOffset;

    void Update()
    {
        if (!rotationEnabled)
        {
            return;
        }
        currentPingPongTime += Time.deltaTime * swingSpeed;

        pingPongValue = Mathf.PingPong(currentPingPongTime, maxSwingAngle * 2f);
        currentSwingOffset = pingPongValue - maxSwingAngle;

        transform.localEulerAngles = new Vector3(
            transform.localEulerAngles.x,
            transform.localEulerAngles.y,
            currentSwingOffset
        );
    }
}