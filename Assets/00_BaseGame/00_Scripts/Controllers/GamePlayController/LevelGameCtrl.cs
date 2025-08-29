
using System;
using UnityEngine;

public class LevelGameCtrl : MonoBehaviour
{
    public void Init()
    {
        // Không cần truyền tham số nữa và hàm này giờ trả về GameObject
        GameController.Instance.levelBundleManager.InstantiateLevelFromPreloaded();
    }
}
