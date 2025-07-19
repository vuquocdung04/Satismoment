using DG.Tweening;
using Sirenix.OdinInspector;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class Level_147Ctrl : MonoBehaviour
{
    public L147_Effect effect;
    public L147_Penguin penguin;
    public L147_Cake cakePrefab;
    public Transform btnPlay;
    public Transform posSpawnStart;
    public TMP_Text text;
    [Space(5)]
    public int currentCake = 0;
    public float forceJump = 5f;
    public bool isPlayGame = true; // Changed to public for external access in reset
    public List<L147_Cake> lsCakeHolders;
    public bool isWin = false;
    void Update()
    {
        if (isWin) return;
        if (Input.GetMouseButtonDown(0))
        {
            if (isPlayGame)
            {
                btnPlay.gameObject.SetActive(false);
                isPlayGame = false;
                SpawnInitialCake(); // Call a new method to spawn and start the first cake
            }
            else
            {
                penguin.AddForceY(forceJump);
            }
        }
    }

    // New method to spawn the very first cake
    void SpawnInitialCake()
    {
        if (lsCakeHolders.Count > 0)
        {
            // Clear any existing cakes if a reset is triggered before a full despawn
            foreach (var cake in lsCakeHolders)
            {
                if (cake != null && cake.gameObject.activeSelf)
                {
                    SimplePool2.Despawn(cake.gameObject);
                }
            }
            lsCakeHolders.Clear();
        }

        L147_Cake initialCake = SimplePool2.Spawn(cakePrefab);
        initialCake.levelCtrl = this; // Ensure the level controller is set for the new cake
        initialCake.transform.position = posSpawnStart.position;
        initialCake.isDone = false;
        initialCake.StartMoving();
        lsCakeHolders.Add(initialCake);
        currentCake = 0; // Reset currentCake count
    }

    public IEnumerator HandleWinCondition()
    {
        if(currentCake == 8)
        {
            isWin = true;
            yield return new WaitForSeconds(0.5f);
            WinBox.SetUp().Show();
        }
    }

    public void ResetGame()
    {
        Debug.Log("Resetting Game...");
        // Despawn all existing cakes
        foreach (var cake in lsCakeHolders)
        {
            if (cake != null && cake.gameObject.activeSelf) // Check if not null and active before despawn
            {
                cake.transform.DOKill();
                SimplePool2.Despawn(cake.gameObject);
            }
        }
        lsCakeHolders.Clear();
        currentCake = 0;
        text.text = currentCake.ToString();
        penguin.ResetPosition();

        btnPlay.gameObject.SetActive(true);
        isPlayGame = true;
        penguin.waitTimeResetGame = false;
    }


    [Button("Setup", ButtonSizes.Large)]
    void Setup()
    {
        posSpawnStart.transform.position = cakePrefab.transform.position;
    }
}