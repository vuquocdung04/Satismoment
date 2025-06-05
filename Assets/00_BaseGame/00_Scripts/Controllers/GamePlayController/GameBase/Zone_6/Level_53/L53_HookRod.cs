using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class L53_HookRod : MonoBehaviour
{
    public Level_53Ctrl levelCtrl;
    public bool isPullReady = false;
    public float minXPosition;
    public float maxXPosition;

    public Transform hookLeft;
    public Transform hookRight;
    public List<Vector3> lsAngle;
    private bool isCollided = false;

    private Transform currentToy;
    private void OnTriggerEnter2D(Collider2D collision)
    {
        currentToy = collision.GetComponent<Transform>();
        currentToy.SetParent(transform);
        currentToy.GetComponent<Rigidbody2D>().bodyType = RigidbodyType2D.Kinematic;
        isCollided = true;
    }

    public IEnumerator PullDown()
    {
        isPullReady = true;
        isCollided = false;
        float targetY = 1f;
        float speed = 5f;   

        RotateHook(hookLeft, hookRight, lsAngle[0], lsAngle[1]);
        while (transform.position.y > targetY)
        {
            if (isCollided)
            {
                break;
            }
            transform.position += Vector3.down * speed * Time.deltaTime;
            yield return null;
        }
        Tween pushHook = transform.DOMoveY(11f,1f);
        RotateHook(hookLeft, hookRight, lsAngle[2], lsAngle[3]);
        yield return pushHook.WaitForCompletion();

        if(currentToy != null)
        {
            levelCtrl.winProgress++;
            Destroy(currentToy.gameObject);
            currentToy = null;
            StartCoroutine(levelCtrl.HandleWinCodition());
        }
        transform.DOMoveY(5.48f, 1f);
        isPullReady = false;

    }

    void RotateHook(Transform hookLeftParam, Transform hookRightParam,Vector3 angleLeft, Vector3 angleRight)
    {
        hookLeftParam.DORotate(angleLeft, 1.25f);
        hookRightParam.DORotate(angleRight, 1.25f);
    }


}
