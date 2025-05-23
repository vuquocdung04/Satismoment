using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Sirenix.OdinInspector;
using System.Linq;
using DG.Tweening; // Cần thêm để sử dụng OrderBy/Sort

public class Level_31Ctrl : Singleton<Level_31Ctrl>
{
    protected override void OnAwake()
    {
        base.OnAwake();
        m_DontDestroyOnLoad = false;
    }
    public bool isWin = false;
    public int progressWin = 0;
    public Transform starPrefab;
    public Transform progressBar;
    public float spacingX = 0.35f;
    public float spacingY = 0.35f;
    public Transform block_Hold;
    public List<L31_block> lsBlocks_Prefabs;
    public List<L31_block> lsBlocks = new List<L31_block>();

    // Biến để tính toán vị trí
    private float startPosX = -1.6f;
    private float startPosY = 1.625f;
    private int gridWidth = 10;
    private int gridHeight = 10;


    private void Start()
    {
        InitMatrix();
    }

    void InitMatrix()
    {
        lsBlocks.Clear();

        for (int i = 0; i < gridWidth; i++)
        {
            for (int j = 0; j < gridHeight; j++)
            {
                int rand = Random.Range(0, lsBlocks_Prefabs.Count);
                Vector2 posSpawn = new Vector2(startPosX + i * spacingX, startPosY - j * spacingY);
                L31_block block = Instantiate(lsBlocks_Prefabs[rand], posSpawn, Quaternion.identity, block_Hold);
                block.posBlock = posSpawn;
                lsBlocks.Add(block);
            }
        }

        foreach (L31_block block in lsBlocks)
        {
            block.SetNeighbor();
        }
    }

    public L31_block GetBlock(float posX, float posY)
    {
        foreach (var block in this.lsBlocks)
        {
            // Kiểm tra block có null không và so sánh vị trí
            if (block != null && Mathf.Abs(block.posBlock.x - posX) <= 0.02f && Mathf.Abs(block.posBlock.y - posY) <= 0.02f)
                return block;
        }
        return null;
    }

    public void CheckAndDestroyConnectedBlocks(L31_block startBlock)
    {
        if (startBlock == null) return;

        int targetID = startBlock.idBlock;
        List<L31_block> blocksToDestroy = FindConnectedBlocks(startBlock, targetID);

        if (blocksToDestroy.Count > 1)
        {
            DestroyBlocks(blocksToDestroy);
        }
    }
    
    // BFS
    private List<L31_block> FindConnectedBlocks(L31_block startBlock, int targetID)
    {
        List<L31_block> connectedBlocks = new List<L31_block>();
        Queue<L31_block> queue = new Queue<L31_block>();
        HashSet<L31_block> visited = new HashSet<L31_block>();

        queue.Enqueue(startBlock);
        visited.Add(startBlock);
        connectedBlocks.Add(startBlock);

        while (queue.Count > 0)
        {
            L31_block currentBlock = queue.Dequeue();
            L31_block[] neighbors = {
                currentBlock.neighbor_Left,
                currentBlock.neighbor_Right,
                currentBlock.neighbor_Top,
                currentBlock.neighbor_Bottom
            };

            foreach (L31_block neighbor in neighbors)
            {
                if (neighbor != null && neighbor.idBlock == targetID && !visited.Contains(neighbor))
                {
                    visited.Add(neighbor);
                    queue.Enqueue(neighbor);
                    connectedBlocks.Add(neighbor);
                }
            }
        }
        return connectedBlocks;
    }

    // Destroy
    private void DestroyBlocks(List<L31_block> blocksToDestroy)
    {
        bool blocksWereDestroyed = blocksToDestroy.Count > 0;

        int i = 0;
        foreach (L31_block block in blocksToDestroy)
        {
            if (block != null)
            {
                lsBlocks.Remove(block);
                var star = SimplePool2.Spawn(starPrefab, block.posBlock, Quaternion.identity);
                star.DOMove(new Vector3(2.17f, 2.41f),0.5f).OnComplete(()=> SimplePool2.Despawn(star.gameObject));
                DestroyWithAnimation(block);
                i++;
            }
        }
        StartCoroutine(HandleWin());

        if (blocksWereDestroyed)
        {
            ApplyGravity();
            UpdateAllNeighbors();
        }

        IEnumerator HandleWin()
        {
            float targetX;
            progressWin += i;
            targetX = 4.3f - progressWin * 4.3f / 30;

            if (progressWin >= 30)
            {
                isWin = true;
                targetX = 0;
                foreach (var block in this.lsBlocks) DestroyWithAnimation(block);
                yield return new WaitForSeconds(1f);
                WinBox.SetUp().Show();
            }
            progressBar.DOMoveX(targetX, 0.75f).SetEase(Ease.OutQuad);
        }

        //
        void DestroyWithAnimation(L31_block block)
        {
            Sequence seq = DOTween.Sequence();
            seq.Append(block.transform.DOScale(block.transform.localScale * 1.2f, 0.1f));
            seq.Append(block.transform.DOScale(block.transform.localScale, 0.15f));

            seq.OnComplete(() =>
            {
                Destroy(block.gameObject);
            });
        }
    }

    

    /// <summary>
    /// Xử lý logic rơi của các block trong từng cột.
    /// </summary>
    private void ApplyGravity()
    {
        for (int i = 0; i < gridWidth; i++)
        {
            float currentX = startPosX + i * spacingX;
            List<L31_block> columnBlocks = new List<L31_block>();

            foreach (L31_block block in lsBlocks)
            {
                if (block != null && Mathf.Abs(block.posBlock.x - currentX) <= 0.02f)
                {
                    columnBlocks.Add(block);
                }
            }
            columnBlocks = columnBlocks.OrderBy(b => b.posBlock.y).ToList();

            for (int j = 0; j < columnBlocks.Count; j++)
            {
                L31_block block = columnBlocks[j];

                int newRowIndex = gridHeight - 1 - j;
                float newY = startPosY - newRowIndex * spacingY;

                if (Mathf.Abs(block.posBlock.y - newY) > 0.02f)
                {
                    block.posBlock = new Vector2(currentX, newY);

                    StartCoroutine(MoveBlockSmoothly(block, block.posBlock, 0.3f));
                }
            }
        }
    }

    /// <summary>
    /// Coroutine để di chuyển block mượt mà (Tùy chọn)
    /// </summary>
    IEnumerator MoveBlockSmoothly(L31_block block, Vector2 targetPosition, float duration)
    {
        if (block == null) yield break; // Kiểm tra nếu block đã bị hủy trong quá trình
        Vector2 startPosition = block.transform.position;
        float time = 0;

        while (time < duration)
        {
            if (block == null) yield break; // Kiểm tra lại
            block.transform.position = Vector2.Lerp(startPosition, targetPosition, time / duration);
            time += Time.deltaTime;
            yield return null;
        }
        if (block != null) // Kiểm tra lần cuối
            block.transform.position = targetPosition;
    }


    /// <summary>
    /// Cập nhật lại neighbor cho tất cả các block còn lại.
    /// </summary>
    private void UpdateAllNeighbors()
    {
        StartCoroutine(DelayedUpdateNeighbors(0.35f)); // Chờ 0.3s (hơn thời gian rơi)
    }

    IEnumerator DelayedUpdateNeighbors(float delay)
    {
        yield return new WaitForSeconds(delay); // Chờ animation

        foreach (L31_block block in lsBlocks)
        {
            if (block != null)
            {
                block.SetNeighbor();
            }
        }
        Debug.Log("Neighbors updated.");
    }
    // --------------------------
}