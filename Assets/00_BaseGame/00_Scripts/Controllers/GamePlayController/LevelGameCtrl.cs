
using UnityEngine;

public class LevelGameCtrl : MonoBehaviour
{
    public void Init()
    {
        GameController.Instance.remoteLoadTest.LoadLevelGameObject(UseProfile.CurrentLevel);
    }
}
