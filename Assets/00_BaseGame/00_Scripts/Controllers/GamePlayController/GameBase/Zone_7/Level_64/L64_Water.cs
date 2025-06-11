using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class L64_Water : MonoBehaviour
{
    public List<Sprite> lsSpriteWaters;
    private SpriteRenderer spriteRenderer;
    private int currentSpriteIndex = 0;

    private void Start()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
        
        // Bắt đầu coroutine để đổi sprite mỗi 0.5 giây
        StartCoroutine(ChangeSpriteRoutine());
    }

    IEnumerator ChangeSpriteRoutine()
    {
        while (true)
        {
            // Kiểm tra nếu list rỗng thì thoát
            if (lsSpriteWaters.Count == 0)
                yield break;

            // Đổi sprite
            currentSpriteIndex = (currentSpriteIndex + 1) % lsSpriteWaters.Count;
            spriteRenderer.sprite = lsSpriteWaters[currentSpriteIndex];

            // Chờ 0.5 giây trước khi đổi lần nữa
            yield return new WaitForSeconds(0.5f);
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        var dino = collision.GetComponent<L64_Dino>();
        if (dino == null) return;
        dino.levelCtrl.StopMoveMap();
        dino.levelCtrl.btnRetry.gameObject.SetActive(true);
        dino.gameObject.SetActive(false);
    }
}