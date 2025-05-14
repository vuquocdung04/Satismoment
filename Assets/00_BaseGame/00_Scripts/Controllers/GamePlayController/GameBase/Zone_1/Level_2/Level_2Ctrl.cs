using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
public class Level_2Ctrl : MonoBehaviour
{
    public L2_CakeItem selectedCake;
    public bool isDragging = false;
    public List<L2_CakeItem> lsCake;
    public List<L2_CakeItemPlate> lsCakeItemPlates;
    public int amount = 0;
    private void Start()
    {
        foreach (var cake in this.lsCake) cake.Init();
        foreach (var cake in this.lsCakeItemPlates) cake.Init();
    }
    private void Update()
    {
        SelectedCake1();
    }

    void SelectedCake1()
    {
        Vector2 posMouse = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        if (Input.GetMouseButtonDown(0))
        {
            RaycastHit2D hit = Physics2D.Raycast(posMouse, Vector3.zero);

            if (hit.collider == null) return;
            selectedCake = hit.collider.GetComponent<L2_CakeItem>();
            if(selectedCake != null)
            {
                isDragging = true;
                selectedCake.spriteRenderer.sortingOrder = 3;
                selectedCake.HandleIconDrag();
            }
        }

        if(isDragging && selectedCake != null)
        {
            selectedCake.transform.position = posMouse;
        }

        if (Input.GetMouseButtonUp(0))
        {
            if (selectedCake == null) return;
            selectedCake.HandleIconStart();
            selectedCake.transform.position = selectedCake.pos;
            selectedCake.spriteRenderer.sortingOrder =1;
            selectedCake = null;
            isDragging = false;
        }

    }

    public void IncreaseAmount()
    {
        amount++;
        if (amount == 5)
        {
            DOVirtual.DelayedCall(0.5f, () => WinBox.SetUp().Show());
        }
    }

}
