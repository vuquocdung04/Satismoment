using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameController : MonoBehaviour
{
    private static GameController _instance;
    public static GameController Instance
    {
        get
        {
            if(_instance == null)
            {
                _instance = FindAnyObjectByType<GameController>();
                if(_instance == null)
                {
                    GameObject obj = new GameObject("GameController");
                    _instance = obj.AddComponent<GameController>();
                    DontDestroyOnLoad(obj);
                }
            }
            return _instance;
        }
    }

    public int levelGame = 1;
    public StartLoading startLoading;


    protected void Awake()
    {
        if(_instance == null)
        {
            _instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else if (_instance != this)
        {
            Destroy(gameObject);
        }
        Init();

        DontDestroyOnLoad(this);
    }
    void Init()
    {
        startLoading.Init();
    }

}
