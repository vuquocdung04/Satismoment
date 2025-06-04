using Sirenix.OdinInspector;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class L52_Picture : MonoBehaviour
{
    public List<CircleCollider2D> lsCollider;

    [Button("Setup Collider", ButtonSizes.Large)]
    void SetupCollider(Transform[] pos)
    {

        for (int i = 0; i < pos.Length; i++)
        {
            var colli = gameObject.AddComponent<CircleCollider2D>();
            colli.radius = 0.3f;
            lsCollider.Add(colli);
            lsCollider[i].offset = pos[i].position - transform.position;
        }
    }
}
