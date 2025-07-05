using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class L112_Plate : MonoBehaviour
{
    public Level_112Ctrl LevelCtrl;
    public float weight;
    public BoxCollider2D boxCollider2D;

    // Không cần biến item thành viên ở đây nữa

    public void UpdateWeight(float weight)
    {
        this.weight += weight;
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (LevelCtrl.isWin) return;

        // Lấy đối tượng item từ collider vừa va chạm
        L112_Item enteredItem = collision.collider.GetComponent<L112_Item>();

        if (enteredItem == null) return; // Nếu không phải L112_Item thì bỏ qua

        UpdateWeight(enteredItem.weight); // Cập nhật trọng lượng
        enteredItem.SetParen(LevelCtrl.beam.transform);
        LevelCtrl.beam.UpdateBeamTilt(); // Cập nhật độ nghiêng của beam
    }

    private void OnCollisionExit2D(Collision2D collision)
    {
        if (LevelCtrl.isWin) return;
        L112_Item exitedItem = collision.collider.GetComponent<L112_Item>();

        if (exitedItem == null) return; // Nếu không phải L112_Item thì bỏ qua
        StartCoroutine(WaitTimeSetParent(exitedItem));
        UpdateWeight(-exitedItem.weight); // Giảm trọng lượng
        LevelCtrl.beam.UpdateBeamTilt(); // Cập nhật độ nghiêng của beam
    }

    IEnumerator WaitTimeSetParent(L112_Item item)
    {
        yield return new WaitForEndOfFrame();
        item.SetParen(LevelCtrl.transform);
    }
}