using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum L132_MouseSide
{
    Left,
    Center,
    Right
}

public class Level_132Ctrl : MonoBehaviour
{
    public L132_SpaceShip spaceShip;

    private L132_MouseSide newMouseSide;
    private L132_MouseSide currentMouseSide;

    private bool isClicked;
    private Vector3 mousePosition;
    public bool isWin = false;
    void Update()
    {
        if (isWin) return;
        HandleMouseInput();
    }

    void HandleMouseInput()
    {
        UpdateMousePosition();
        UpdateMouseSide();

        if (Input.GetMouseButtonDown(0))
        {
            isClicked = true;
            spaceShip.OnStartState();
            HandleRotationIfNeeded();
            currentMouseSide = L132_MouseSide.Center;
        }

        if (isClicked)
        {
            HandleRotationIfNeeded();
            spaceShip.SpaceShipFlying(); // Bay theo hướng hiện tại
        }

        if (Input.GetMouseButtonUp(0))
        {
            spaceShip.OnEndState();
            isClicked = false;
        }
    }

    void UpdateMousePosition()
    {
        mousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
    }

    void UpdateMouseSide()
    {
        if (mousePosition.x > 0)
            newMouseSide = L132_MouseSide.Right;
        else if (mousePosition.x < 0)
            newMouseSide = L132_MouseSide.Left;
        else
            newMouseSide = L132_MouseSide.Center;
    }

    void HandleRotationIfNeeded()
    {
        if (newMouseSide == currentMouseSide)
            return;

        switch (newMouseSide)
        {
            case L132_MouseSide.Right:
                spaceShip.RotateShipRight();
                Debug.Log("Mouse Side: Right");
                break;

            case L132_MouseSide.Left:
                spaceShip.RotateShipLeft();
                Debug.Log("Mouse Side: Left");
                break;

            case L132_MouseSide.Center:
                spaceShip.RotateShipCenter();
                Debug.Log("Mouse Side: Center");
                break;
        }

        currentMouseSide = newMouseSide;
    }

    public IEnumerator HandleWinCondition()
    {
        isWin = true;
        yield return new WaitForSeconds(0.5f);
        WinBox.SetUp().Show();
    }
}