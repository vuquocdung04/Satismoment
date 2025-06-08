using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum TurntablePartType
{
    vinylDisc,
    turntableArm,
}

public class L57_TurntablePart : MonoBehaviour
{
    public Vector2 posCorrect;
    public TurntablePartType type;
    public bool isMovevinylDisc;
}
