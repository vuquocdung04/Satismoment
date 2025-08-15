
using UnityEngine;

public class Level_20Ctrl : MonoBehaviour
{
    public AudioClip hitSound;
    public int winProgress;

    [Header("Hammer Prefab")]
    public Transform hammerPrefab;
    [Header("Hit Prefab")]
    public Transform hitPrefab;

    public L20_HoleUnit holeUnit;

    Vector3 mousePos;
    private void Update()
    {
        if (Camera.main != null) mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        mousePos.z = 0;
        if (Input.GetMouseButtonDown(0))
        {
            RaycastHit2D hit = Physics2D.Raycast(mousePos, Vector2.zero);
            if (hit.collider == null) return;
            holeUnit = hit.collider.GetComponentInParent<L20_HoleUnit>();
            if (holeUnit == null) return;
            SpawnHammerEffect();
            GameController.Instance.musicManager.PlaySingle(hitSound);
            holeUnit.HandleHit();
            winProgress++;
        }

        if (Input.GetMouseButtonUp(0)) StartCoroutine(HandleWin());
    }
    void SpawnHammerEffect()
    {
        Vector3 newPos = new Vector3(0,0.5f,0);
        SimplePool2.Spawn(hammerPrefab.gameObject, mousePos + newPos, Quaternion.identity);
        SimplePool2.Spawn(hitPrefab.gameObject, mousePos, Quaternion.identity);
    }

    System.Collections.IEnumerator HandleWin()
    {
        if(winProgress > 9)
        {
            yield return new WaitForSeconds(0.5f);
            WinBox.SetUp().Show();
        }
    }
}
