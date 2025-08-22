using System.Collections;
using System.Collections.Generic;
using Sirenix.OdinInspector;
using UnityEngine;

public class ScaleBG : MonoBehaviour
{
    public List<Transform> lsLevels;

    [Button("Scale BG", ButtonSizes.Large)]
    void SetupBG()
    {
        foreach (var level in this.lsLevels)
        {
            var bg = level.Find("bg");
            bg.localScale = new Vector3(20, 20, 20);
            bg.localPosition = Vector3.zero;
        }
    }
}
