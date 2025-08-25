using DG.Tweening;
using UnityEngine;

public class Level_15Ctrl : MonoBehaviour
{
    public Transform brokenEggShell;
    public L15_Anim anim;
    public Transform mask;
    public Transform eggShell;
    public int eggShellSpamCount = 2;
    public int winProgress;
    private RaycastHit2D hit;
    private Vector3 mousePos;
    private Tween wobbleTween;
    [Space(5)]
    [SerializeField] AudioClip brokenEggSound;

    private void Start()
    {
        StartWobble();
    }
    private void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            if (Camera.main != null) mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            hit = Physics2D.Raycast(mousePos, Vector2.zero);

            if (!hit.collider) return;

            StopWobble();

            winProgress++;
            if(winProgress % 3 == 0)
            {
                GameController.Instance.musicManager.PlaySingle(brokenEggSound);
                mask.transform.position += new Vector3(0.7f, 0, 0);
                SpamEggShell();
            }

            if(winProgress > 18)
            {
                eggShell.GetComponent<CapsuleCollider2D>().enabled = false;
                anim.SimpleShake();

            }
        }
    }

    void SpamEggShell()
    {
        float rand = Random.Range(0.4f, 1f);
        for (int i = 0; i < eggShellSpamCount; i++)
        {
            brokenEggShell.localScale = new Vector3(rand, rand);
            SimplePool2.Spawn(brokenEggShell.gameObject, mask.transform.position - Vector3.right * 2.5f, Quaternion.identity);
        }

    }

    void StartWobble()
    {
        wobbleTween = eggShell
            .DORotate(new Vector3(0, 0, 3), 0.5f)
            .SetEase(Ease.InOutSine)
            .SetLoops(-1, LoopType.Yoyo);
    }
    void StopWobble()
    {
        if (wobbleTween == null) return;
        wobbleTween.Kill();
        eggShell.rotation = Quaternion.identity;
    }
}
