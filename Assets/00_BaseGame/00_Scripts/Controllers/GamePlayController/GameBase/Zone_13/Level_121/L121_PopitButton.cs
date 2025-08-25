
using UnityEngine;

public class L121_PopitButton : MonoBehaviour
{
    public int id;
    public SpriteRenderer objRenderer;
    public CircleCollider2D circleCollider;

    public void OnClicked(Sprite sprite)
    {
        objRenderer.sprite = sprite;
        circleCollider.enabled = false;
    }

    public void ResetState(Sprite sprite)
    {
        objRenderer.sprite = sprite;
        circleCollider.enabled = true;
        objRenderer.color = Color.white;
    }

    public void NotionStateStart(Sprite sprite)
    {
        objRenderer.sprite = sprite;
    }

    public void ChangeColor(Color newColor)
    {
        objRenderer.color = newColor;
    }

    public void InitSetup()
    {
        objRenderer = GetComponent<SpriteRenderer>();
        circleCollider = GetComponent<CircleCollider2D>();
    }
}
