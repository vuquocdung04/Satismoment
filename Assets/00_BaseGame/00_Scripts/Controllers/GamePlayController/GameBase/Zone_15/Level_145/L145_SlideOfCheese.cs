using DG.Tweening;
using UnityEngine;

public class L145_SlideOfCheese : MonoBehaviour
{
    public Level_145Ctrl levelCtrl;
    public Transform model; // Phô mai chính
    public L145_CheeseShaving cheeseShavingPrefabs;
    public Transform posSpawn;

    private int triggerCount = 0;
    private int maxTrigger = 7;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        // Kiểm tra nếu đã trigger đủ 7 lần thì dừng
        if (triggerCount >= maxTrigger) return;

        // Tạo mảnh phô mai rơi
        float randX = Random.Range(-0.4f, 0.4f);
        float randY = Random.Range(0f, 0.4f);
        var spawnPos = posSpawn.transform.position + new Vector3(randX, randY);
        var cheeseClone = Instantiate(cheeseShavingPrefabs, spawnPos, Quaternion.identity);
        cheeseClone.Falling();

        triggerCount++;
        float fallStep = -1.55f / 7;
        Vector3 targetPosition = model.localPosition + new Vector3(0, fallStep, 0);
        model.DOLocalMove(targetPosition, 0.1f).SetEase(Ease.OutQuad);
        levelCtrl.winProgress++;
    }
}