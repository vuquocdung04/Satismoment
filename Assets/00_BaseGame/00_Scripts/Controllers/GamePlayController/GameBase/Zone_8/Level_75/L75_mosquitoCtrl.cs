using UnityEngine;
using UnityEngine.UI;

public class L75_mosquitoCtrl : MonoBehaviour
{
    public bool isDead;
    public Level_75Ctrl levelCtrl;
    [SerializeField] private Image hpBar;
    [SerializeField] private float damageRate = 0.1f;
    [SerializeField] private float damageInterval = 1f;

    [Header("References")]
    [SerializeField] private L75_mosquitoAnim mosquitoAnim;
    [SerializeField] private L75_mosquitoMove mosquitoMove;

    private float timer;

    private void OnTriggerStay2D(Collider2D collision)
    {
        if (isDead) return;

        timer += Time.deltaTime;

        if (timer >= damageInterval)
        {
            if (hpBar.fillAmount > 0)
            {
                hpBar.fillAmount = Mathf.Max(hpBar.fillAmount - damageRate, 0f);
                timer = 0f;

                if (hpBar.fillAmount == 0)
                {
                    Die();
                    levelCtrl.winProgress++;
                }
            }
        }
    }

    private void Die()
    {
        isDead = true;

        // Chuyển sang sprite chết
        if (mosquitoAnim != null)
        {
            mosquitoAnim.PlayDeathAnimation(); // hàm mới trong Anim để xử lý chết
        }

        // Dừng di chuyển
        if (mosquitoMove != null)
        {
            mosquitoMove.StopMoving();
        }
    }
}