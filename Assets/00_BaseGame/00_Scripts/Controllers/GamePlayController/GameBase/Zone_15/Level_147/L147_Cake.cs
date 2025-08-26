using DG.Tweening;
using Sirenix.OdinInspector;
using System.Collections;
using UnityEngine;

public class L147_Cake : MonoBehaviour
{
    public Level_147Ctrl levelCtrl;
    public SpriteRenderer objRenderer;
    public float sizeY;
    public bool isDone = false;

    public void StartMoving()
    {
        // Di chuyển Cake qua lại
        transform.DOMoveX(3.55f, 4f)
            .SetEase(Ease.Linear)
            .SetLoops(-1, LoopType.Yoyo); // Qua lại liên tục
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (isDone) return;

        // Kiểm tra va chạm với Penguin
        L147_Penguin penguin = collision.transform.GetComponent<L147_Penguin>();
        if (penguin == null) return;

        float hitDirectionY = collision.transform.position.y - transform.position.y;

        // 1. Va chạm từ phía trên → dừng Cake
        if (hitDirectionY > sizeY / 2)
        {
            isDone = true;
            levelCtrl.currentCake++;
            levelCtrl.text.text = levelCtrl.currentCake.ToString();
            Debug.Log("Penguin rơi xuống → Dừng Cake. Current Cake Count: " + levelCtrl.currentCake);
            StartCoroutine(levelCtrl.HandleWinCondition());
            // Stop the movement of the current cake
            transform.DOKill();
            var effectClone = SimplePool2.Spawn(levelCtrl.effect, transform.position, Quaternion.identity);
            effectClone.InitEffect();

            if (!levelCtrl.isWin)
            {
                var cakeClone = SimplePool2.Spawn(levelCtrl.cakePrefab);
                cakeClone.isDone = false;
                cakeClone.levelCtrl = this.levelCtrl;
                cakeClone.transform.position = levelCtrl.posSpawnStart.position + new Vector3(0, sizeY - 0.1f) * levelCtrl.currentCake;
                cakeClone.StartMoving();
                levelCtrl.lsCakeHolders.Add(cakeClone);
            }
        }
        // 2. Va chạm từ ngang → đẩy Penguin và reset lại
        else
        {
            Debug.Log("Va chạm từ ngang → Đẩy Penguin và Reset lại");

            // Xác định hướng va chạm
            float hitDirectionX = collision.transform.position.x - transform.position.x;
            Vector2 forceDirection = hitDirectionX > 0 ? Vector2.right : Vector2.left;

            // Thêm lực đẩy
            penguin.rb.AddForce(forceDirection * 5f, ForceMode2D.Impulse);
            penguin.waitTimeResetGame = true;
            // Kill current cake's tween immediately upon lateral collision
            transform.DOKill();

            StartCoroutine(ResetGameAfterDelay()); // Call new coroutine to reset game
        }
    }

    private IEnumerator ResetGameAfterDelay()
    {
        // Đợi 0.5 giây (có thể thay đổi theo ý bạn)
        yield return new WaitForSeconds(0.5f);
        levelCtrl.ResetGame(); // Call the reset method on the Level_147Ctrl
        Debug.Log("Reset hoàn tất");
    }
    [Button("Setup", ButtonSizes.Large)]
    void Setup()
    {
        // Ensure objRenderer is assigned in the Inspector
        if (objRenderer != null)
        {
            sizeY = objRenderer.bounds.size.y;
        }
        else
        {
            Debug.LogWarning("objRenderer is not assigned on " + gameObject.name);
        }
    }
}