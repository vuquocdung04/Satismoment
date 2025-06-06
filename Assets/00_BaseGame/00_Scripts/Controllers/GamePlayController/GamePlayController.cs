using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GamePlayController : Singleton<GamePlayController>
{
    public GameScene gameScene;
    public LevelGameCtrl levelGameCtrl;
    protected override void OnAwake()
    {
        base.OnAwake();
        m_DontDestroyOnLoad = false;
        Init();
    }

    void Init()
    {
        gameScene.Init();
        levelGameCtrl.Init();
    }
}
