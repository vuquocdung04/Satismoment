using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Level_125Ctrl : BaseDragController<L125_Food>
{
    public int foodConsumedTotal;
    public int foodNeededTotal = 10;
    public L125_Charcoal charCoal;
    public Collider2D charCoalGrillCollider;
    public L125_Cat cat;
    public List<L125_Food> lsFoodPrefabs;
    private void Start()
    {
        charCoal.StartAnimation();
        for (int i = 0; i < foodNeededTotal; i++)
        {
            int rand = Random.Range(0, lsFoodPrefabs.Count);
            float randPosX = Random.Range(-1.75f,1.75f);
            float randPosY = Random.Range(-2.3f,-4.3f);
            var foodClone = SimplePool2.Spawn(lsFoodPrefabs[rand], new Vector2(randPosX,randPosY), Quaternion.identity);
            foodClone.defaultPosition = new Vector2(randPosX,randPosY);
        }
    }

    protected override void OnDragEnded()
    {
        draggableComponent.HandleConllisionWithGrill(charCoalGrillCollider);
        if (draggableComponent.CheckCollisitonWithMount(cat.catCollider))
        {
            foodConsumedTotal++;
            cat.ResetChewingAnimation();
            SimplePool2.Despawn(draggableComponent.gameObject);
        }
    }

    protected override void OnDragLogic(Vector3 currentMousePosition, Vector3 deltaMousePosition)
    {
        draggableComponent.transform.position += mouseDelta;
    }

    protected override void OnDragStarted()
    {
        draggableComponent.OnStartDrag();
    }

    IEnumerator HandleWinCondition()
    {
        yield return new WaitForSeconds(0.5f);
        WinBox.SetUp().Show();
    }
}
