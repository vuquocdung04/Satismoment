
using UnityEngine;
using System.Collections;

namespace _00_BaseGame._00_Scripts.Controllers.GamePlayController.GameBase.Zone_1.Level_3
{
    public class L3_Picture : MonoBehaviour
    {
        public SpriteRenderer spriteRenderer;
        public Sprite spriteWhenCompleted;
        public Transform icon;

        public void ChangeSprite(System.Action callback = null)
        {
            StartCoroutine(Animation(callback));
        }

        private IEnumerator Animation(System.Action callback = null)
        {
            icon.gameObject.SetActive(false);
            yield return new WaitForSeconds(0.5f);
            spriteRenderer.sprite = spriteWhenCompleted;
            callback?.Invoke();
        }

        private void OnDestroy()
        {
            StopAllCoroutines();
        }
    }
}
