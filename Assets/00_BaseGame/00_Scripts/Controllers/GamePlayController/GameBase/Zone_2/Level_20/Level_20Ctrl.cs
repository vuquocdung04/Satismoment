using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Level_20Ctrl : MonoBehaviour
{
    public int winProgress = 0;

    [Header("Hammer Prefab")]
    public Transform hammerPrefab;
    [Header("Hit Prefab")]
    public Transform hitPrefab;

    public L20_HoleUnit holeUnit;

    Vector3 mousePos;
    private void Update()
    {
        mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        mousePos.z = 0;
        if (Input.GetMouseButtonDown(0))
        {
            RaycastHit2D hit = Physics2D.Raycast(mousePos, Vector2.zero);
            if (hit.collider == null) return;
            holeUnit = hit.collider.GetComponentInParent<L20_HoleUnit>();
            if (holeUnit == null) return;
            SpawnHammerEffect();
            holeUnit.HandleHit();
            winProgress++;
        }

        if (Input.GetMouseButtonUp(0)) HandleWin();
    }
    void SpawnHammerEffect()
    {
        Vector3 newPos = new Vector3(0,0.5f,0);
        SimplePool2.Spawn(hammerPrefab.gameObject, mousePos + newPos, Quaternion.identity);
        SimplePool2.Spawn(hitPrefab.gameObject, mousePos, Quaternion.identity);
    }

    void HandleWin()
    {
        if(winProgress > 9)
        {
            WinBox.SetUp().Show();
        }
    }
}
