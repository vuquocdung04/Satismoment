
using UnityEngine;

public class L11_N : MonoBehaviour
{
    public int id;
    public Transform dish;
    public Level_11Ctrl levelCtrl;
    private void OnTriggerEnter2D(Collider2D collision)
    {
        var item = collision.GetComponent<L11_Item>();
        if (item == null) return;

        if(item.id == id)
        {
            item.gameObject.SetActive(false);
            dish.gameObject.SetActive(true);
            GameController.Instance.musicManager.PlaySingle(levelCtrl.sound);
            levelCtrl.amount++;
        }
        levelCtrl.CheckWinShowPopup();
    }
}
