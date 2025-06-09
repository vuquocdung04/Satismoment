using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Level_59Ctrl : MonoBehaviour
{
    public bool isWin = false;

    [Header("Prefabs")]
    public List<Transform> lsPrefabs;
    public List<float> yPositions = new List<float> { -1.5f, -0.2f, 1.1f, 2.4f };
    public float[] randScales = { 0.8f, 0.9f, 1f };

    [Header("Movement")]
    public float moveDuration = 5f;
    public float xStart = -4f;
    public float xEnd = 4f;

    [Header("Pig")]
    public L59_Pig pig;
    public float spaceMovePig = 1.3f;

    private List<Tween> activeTweens = new List<Tween>();

    void Start()
    {
        StartCoroutine(SpawnLoop());
    }

    void Update()
    {
        if (isWin) return;

        if (Input.GetMouseButtonDown(0))
        {
            pig.transform.position += Vector3.up * spaceMovePig;
            pig.transform.SetParent(null);
        }
    }

    public int winProgress = 0;

    public IEnumerator OnWin()
    {
        isWin = true;
        StopAllCoroutines();
        yield return new WaitForSeconds(0.1f);
        WinBox.SetUp().Show();
    }

    IEnumerator SpawnLoop()
    {
        var waitTime = new WaitForSeconds(2.5f);
        while (!isWin)
        {
            yield return StartCoroutine(SpawnPrefabsStaggered());
            yield return waitTime;
        }
    }

    IEnumerator SpawnPrefabsStaggered()
    {
        var shuffledY = new List<float>(yPositions);
        ShuffleList(shuffledY);
        var waiTime = new WaitForSeconds(Random.Range(0.1f, 0.3f));
        foreach (float y in shuffledY)
        {
            SpawnAtY(y);
            yield return waiTime;
        }
    }

    void SpawnAtY(float y)
    {
        if (lsPrefabs.Count < 2) return;

        int rand = Random.Range(0, 5);

        Transform prefabToSpawn = (rand == 0) ? lsPrefabs[0] : lsPrefabs[1];

        var spawnPos = new Vector3(xStart, y, 0f);
        var obj = SimplePool2.Spawn(prefabToSpawn, spawnPos, Quaternion.identity);

        float scale = randScales[Random.Range(0, randScales.Length)];
        var tf = obj.transform;

        tf.localScale = Vector3.one * scale;
        Tween t = tf.DOMoveX(xEnd, moveDuration).SetEase(Ease.Linear).OnComplete(() =>
        {
            tf.DOKill();
            SimplePool2.Despawn(obj.gameObject);
        });

        activeTweens.Add(t);
    }

    void ShuffleList<T>(List<T> list)
    {
        for (int i = 0; i < list.Count - 1; i++)
        {
            int j = Random.Range(i, list.Count);
            (list[i], list[j]) = (list[j], list[i]);
        }
    }

    void OnDestroy()
    {
        foreach (var tween in activeTweens)
        {
            if (tween.IsActive()) tween.Kill();
        }

        activeTweens.Clear();
    }
}
