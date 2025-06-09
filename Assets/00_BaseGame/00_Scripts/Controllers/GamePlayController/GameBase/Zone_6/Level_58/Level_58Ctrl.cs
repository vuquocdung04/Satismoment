using DG.Tweening;
using Sirenix.OdinInspector;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Level_58Ctrl : BaseDragController<L58_Pencil>
{
    public List<Transform> lsSlotPoss;
    private bool[] slotUsed;
    private Dictionary<L58_Pencil, int> pencilToSlot = new Dictionary<L58_Pencil, int>();

    private void Start()
    {
        slotUsed = new bool[lsSlotPoss.Count];
    }
    public void PlacePencil(L58_Pencil pencil)
    {
        if (pencilToSlot.TryGetValue(pencil, out int oldSlot))
        {
            slotUsed[oldSlot] = false;
            pencilToSlot.Remove(pencil);
        }

        float minDist = float.MaxValue;
        int nearestIndex = -1;

        for (int i = 0; i < lsSlotPoss.Count; i++)
        {
            if (slotUsed[i]) continue;

            float dist = Vector2.Distance(pencil.transform.position, lsSlotPoss[i].position);
            if (dist < minDist)
            {
                minDist = dist;
                nearestIndex = i;
            }
        }
        if (nearestIndex != -1 && minDist < 0.5f)
        {
            pencil.transform.position = lsSlotPoss[nearestIndex].position;
            slotUsed[nearestIndex] = true;
            pencilToSlot[pencil] = nearestIndex;
        }
        else
        {
            pencil.RotateAngleDefault(RotateMode.Fast);
        }
    }

    protected override void OnDragEnded()
    {
        PlacePencil(draggableComponent);
        CheckCorrectOrder();
    }

    protected override void OnDragLogic(Vector3 currentMousePosition, Vector3 deltaMousePosition)
    {
        draggableComponent.transform.position += mouseDelta;
    }

    protected override void OnDragStarted()
    {
        draggableComponent.RotateToZero();
    }
    private void CheckCorrectOrder()
    {
        if (pencilToSlot.Count != lsSlotPoss.Count) return;

        for (int i = 0; i < lsSlotPoss.Count; i++)
        {
            bool found = false;
            foreach (var kvp in pencilToSlot)
            {
                if (kvp.Value == i) 
                {
                    if (kvp.Key.idPencil != i)
                    {
                        return;
                    }
                    found = true;
                    break;
                }
            }

            if (!found)
            {
                return;
            }
        }
        StartCoroutine(HandleWinCodition());
    }

    IEnumerator HandleWinCodition()
    {
        isWin = true;
        yield return new WaitForSeconds(0.1f);
        WinBox.SetUp().Show();
    }
}
