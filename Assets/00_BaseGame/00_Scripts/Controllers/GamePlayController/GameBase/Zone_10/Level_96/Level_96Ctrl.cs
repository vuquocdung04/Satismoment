using DG.Tweening;
using Sirenix.OdinInspector;
using System.Collections;
using System.Collections.Generic;
using _00_BaseGame._00_Scripts.Controllers.GamePlayController.GameBase;
using UnityEngine;

public class Level_96Ctrl : BaseDragController<L96_KitchenObj>
{
    public AudioClip grillFoodSound;
    public int winProgress;
    public Transform kitchen;
    public Transform plate;
    public Transform potatoRicer;
    public L96_EffectSmoke effectSmoke;
    public List<L96_PiecePotato> lsPiecePotatos;
    public List<L96_KitchenObj> lsKitchenObjs;

    private float currentRotation = 40f;
    private bool hasFallen ;

    protected override void OnDragEnded()
    {
        if (draggableComponent.CheckCorrectPosition() && draggableComponent.objType != L96_KitchenObjType.PressHandle)
        {
            winProgress++;
            if (draggableComponent.objType == L96_KitchenObjType.Potato)
            {
                draggableComponent.transform.SetParent(plate);
                draggableComponent.spriteRenderer.maskInteraction = SpriteMaskInteraction.VisibleOutsideMask;
            }
        }

        if (winProgress == lsKitchenObjs.Count)
        {
            StartCoroutine(HandleWinCondition());
        }
    }

    protected override void OnDragLogic(Vector3 currentMousePosition, Vector3 deltaMousePosition)
    {
        switch (draggableComponent.objType)
        {
            case L96_KitchenObjType.PressHandle:
                currentRotation += mouseDelta.y * 15f;
                currentRotation = Mathf.Clamp(currentRotation, 0f, 40f);

                draggableComponent.transform.localRotation = Quaternion.Euler(0, 0, currentRotation);
                UpdatePlatePosition();

                if (!hasFallen && currentRotation == 0f)
                {
                    FallDownAllPotatoes();
                    winProgress++;
                    hasFallen = true;
                }
                break;

            default:
                draggableComponent.transform.localPosition += mouseDelta;
                break;
        }
    }

    private void FallDownAllPotatoes()
    {
        foreach (var potato in lsPiecePotatos)
        {
            potato.FallDown();
            potato.transform.SetParent(potato.targetPosition);
        }
    }

    IEnumerator HandleWinCondition()
    {
        // Tắt các object không cần thiết
        lsKitchenObjs[0].gameObject.SetActive(false);
        lsKitchenObjs[2].gameObject.SetActive(false);
        potatoRicer.gameObject.SetActive(false);
        plate.gameObject.SetActive(false);

        // Di chuyển bếp xuống
        yield return kitchen.DOMove(new Vector2(0, -0.51f), 1f).SetEase(Ease.Linear).WaitForCompletion();
        GameController.Instance.musicManager.PlaySingle(grillFoodSound);
        // Hiệu ứng khói + màu khoai tây
        foreach (var potato in lsPiecePotatos)
        {
            SpawnEffectSmoke();
            yield return new WaitForSeconds(0.1f);
            potato.spriteRenderer.color = new Color32(255, 200, 0, 255);
        }

        yield return new WaitForSeconds(0.5f);
        WinBox.SetUp().Show();
    }

    void SpawnEffectSmoke()
    {
        Vector3 spawnPos = lsKitchenObjs[1].transform.position;
        float rand = Random.Range(-0.5f, 0.5f);
        var effectGo = Instantiate(effectSmoke, new Vector2(spawnPos.x + rand, spawnPos.y + 0.5f), Quaternion.identity);
        effectGo.SpawnEffect();
    }

    private void UpdatePlatePosition()
    {
        float normalized = Mathf.InverseLerp(0f, 40f, currentRotation);
        float targetY = Mathf.Lerp(0.74f, 1.74f, normalized);

        plate.transform.localPosition = new Vector3(
            plate.transform.localPosition.x,
            targetY,
            plate.transform.localPosition.z
        );
    }

    [Button("Setup", ButtonSizes.Large)]
    void Setup()
    {
        foreach (var child in lsKitchenObjs)
        {
            child.InitCorrect();
            child.InitDefault();
        }
    }

    protected override void OnDragStarted()
    {
        GameController.Instance.musicManager.PlayPick();
    }
}