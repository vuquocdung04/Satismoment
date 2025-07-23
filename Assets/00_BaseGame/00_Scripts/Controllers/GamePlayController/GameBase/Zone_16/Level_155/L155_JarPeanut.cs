using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class L155_JarPeanut : MonoBehaviour
{
    public Level_155Ctrl levelCtrl;
    public List<L155_Peanut> peanutsInJar = new List<L155_Peanut>();

    private HashSet<Collider2D> cachedColliders = new HashSet<Collider2D>();

    private void OnTriggerStay2D(Collider2D collider)
    {
        if (cachedColliders.Contains(collider)) return;

        L155_Peanut peanut = collider.GetComponent<L155_Peanut>();
        if (peanut != null)
        {
            if (!peanutsInJar.Contains(peanut))
            {
                peanutsInJar.Add(peanut);
                Debug.Log("Added peanut with id: " + peanut.id);

                // Thông báo cho controller kiểm tra điều kiện win
                if (levelCtrl != null)
                {
                    levelCtrl.CheckWinCondition();
                }
            }
            cachedColliders.Add(collider);
        }
    }

    private void OnTriggerExit2D(Collider2D collider)
    {
        L155_Peanut peanut = collider.GetComponent<L155_Peanut>();
        if (peanut != null)
        {
            if (peanutsInJar.Contains(peanut))
            {
                peanutsInJar.Remove(peanut);
                Debug.Log("Removed peanut with id: " + peanut.id);

                if (levelCtrl != null)
                {
                    levelCtrl.CheckWinCondition();
                }
            }
            cachedColliders.Remove(collider);
        }
    }
}
