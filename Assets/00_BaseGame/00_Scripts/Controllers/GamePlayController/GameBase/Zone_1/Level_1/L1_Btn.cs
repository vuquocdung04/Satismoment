using UnityEngine;

namespace _00_BaseGame._00_Scripts.Controllers.GamePlayController.GameBase.Zone_1.Level_1
{
    public class L1_Btn : MonoBehaviour
    {
        public SpriteRenderer objRenderer;
        public Sprite spriteOn;
        public Sprite spriteOff;

        public void ChangeSpriteOn(System.Action callback = null)
        {
            objRenderer.sprite = spriteOn;
            callback?.Invoke();
        }
    }
}