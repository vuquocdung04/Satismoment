using Sirenix.OdinInspector;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public enum L159_CircleType
{
    Purple,
    Red,
    Orange,
    Aqua,
    Green
}
public class L159_Circle : MonoBehaviour
{
    public L159_CircleType circleType;
    public Color circleColor;
    public bool isConnected = false;

    public void SetConnected(bool connected)
    {
        isConnected = connected;
    }

}
