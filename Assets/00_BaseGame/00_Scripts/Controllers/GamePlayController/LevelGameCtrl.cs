using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelGameCtrl : MonoBehaviour
{
    public void Init()
    {
        var level = GameController.Instance.dataContain.dataLevel;
        if (UseProfile.SelectedLevel <= 10)
            Instantiate(level.lsZones[0].lsItems[UseProfile.SelectedLevel - 1].levelGame);
        else if(UseProfile.SelectedLevel <= 20)
            Instantiate(level.lsZones[1].lsItems[UseProfile.SelectedLevel - 11].levelGame);

    }
}
