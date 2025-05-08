using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HomeController : Singleton<HomeController>
{
    public HomeScene homeScene;

    protected override void OnAwake()
    {
        base.OnAwake();
        m_DontDestroyOnLoad = false;
    }

    private void Start()
    {
        homeScene.Init();
    }
}
