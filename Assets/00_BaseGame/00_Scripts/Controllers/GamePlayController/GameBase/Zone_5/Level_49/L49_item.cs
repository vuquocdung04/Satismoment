using Sirenix.OdinInspector;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class L49_item : MonoBehaviour
{
    public float speed = 5f;
    public Vector2 posDefault;
    public bool isMove;
    public int curID;
    public List<int> lsId;
    public List<Vector2> lsSnapPos;

    private void Update()
    {
        if (isMove) return;
        transform.Translate(Vector3.down * 5f * Time.deltaTime);
        if (transform.position.y < -3.9f)
        {
            transform.position = posDefault;
        }
    }

    public void StopAndSnap()
    {
        isMove = true;
        Vector2 currentPos = transform.position;
        Vector2 closestPos = lsSnapPos[0]; // diem gan nhat

        float minDistance = Vector2.Distance(currentPos, closestPos);

        foreach(var snapPos in this.lsSnapPos)
        {
            float distance = Vector2.Distance(currentPos, snapPos);
            if(distance < minDistance)
            {
                minDistance = distance;
                closestPos = snapPos;
            }
        }
        transform.position = closestPos;
    }

    public void GetIdByPosStop()
    {
        for(int i = 0; i < lsSnapPos.Count; i++)
        {
            if(transform.position.y == lsSnapPos[i].y)
            {
                curID = lsId[i];
                break;
            }
        }
    }

    [Button("Setup", ButtonSizes.Large)]
    void Setup()
    {
        float[] pos = { 4.8f, 3.2f, 1.3f, -0.5f, -2.3f, -4 };

        posDefault = transform.position;
        for(int i = 0; i < lsSnapPos.Count; i++)
        {
            lsSnapPos[i] = new Vector2(transform.position.x, pos[i]);
        }
    }
}
