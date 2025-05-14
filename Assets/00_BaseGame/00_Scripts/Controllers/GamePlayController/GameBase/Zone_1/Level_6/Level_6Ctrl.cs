using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Level_6Ctrl : MonoBehaviour
{
    public bool isPressing;

    public L6_Balloon balloon;
    public L6_BtnPress btnPress;
    public float scaleSpeed = 5f;
    public List<Sprite> lsSpriteBreakBalloons;
    Vector3 mousePos;
    Vector3 currentScale;
    float scaleIncreament;
    private void Update()
    {
        mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        mousePos.z = 0;
        HandlePressButton();
    }

    void HandlePressButton()
    {
        if (Input.GetMouseButtonDown(0))
        {
            RaycastHit2D hit = Physics2D.Raycast(mousePos, Vector3.zero);

            if (hit.collider == null) return;
            btnPress = hit.collider.GetComponent<L6_BtnPress>();

            if (btnPress == null) return;
            btnPress.spriteRenderer.sprite = btnPress.iconPress;
            isPressing = true;
        }
        if(isPressing && btnPress != null)
        {
            currentScale = balloon.transform.localScale;
            scaleIncreament = scaleSpeed * Time.deltaTime;
            balloon.transform.localScale = currentScale + new Vector3(scaleIncreament, scaleIncreament);


            if(balloon.transform.localScale.x > 0.95f)
            {
                btnPress = null;
                isPressing = false;
                StartCoroutine(ChangeSprite());
            }
        }

        if (Input.GetMouseButtonUp(0))
        {
            isPressing = false;
            if (btnPress == null) return;
            btnPress.spriteRenderer.sprite = btnPress.iconHold;
            btnPress = null;
        }

    }

    IEnumerator ChangeSprite()
    {
        var time = new WaitForSeconds(0.1f);
        int i = 0;
        while (true)
        {
            balloon.spriteRenderer.sprite = lsSpriteBreakBalloons[i];
            i++;
            yield return time;

            if(i >= lsSpriteBreakBalloons.Count)
            {
                yield return new WaitForSeconds(0.5f);
                WinBox.SetUp().Show();
                break;
            }
        }
    }
}
