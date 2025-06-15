using DG.Tweening;
using UnityEngine;

public class L69_DryLeaf : MonoBehaviour
{
    public Transform greenLeaf;
    public Vector2 defaultPos;

    public void InteractWithLeaf()
    {
        float posHere = this.transform.position.y;
        this.transform.DOMoveY(posHere - 10f,1f);
        SpawnNewLeaf();

    }

    public void SpawnNewLeaf()
    {
        greenLeaf.gameObject.SetActive(true);
        greenLeaf.transform.position = defaultPos;
        greenLeaf.transform.localScale = Vector3.zero;
        greenLeaf.DOScale(Vector3.one,1f).OnComplete(()=>Destroy(gameObject));
    }

    private void Reset()
    {
        defaultPos = this.transform.position;
    }
}
