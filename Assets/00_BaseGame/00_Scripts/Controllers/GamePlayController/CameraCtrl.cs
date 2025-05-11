using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraCtrl : MonoBehaviour
{
    public Camera _camera;

    public void Init()
    {
        var pos = GameController.Instance.dataContain.levelDesign.lsLevelDesigns[UseProfile.SelectedLevel-1].size;
        _camera.transform.position = new Vector3((float)(pos - 1)/2,_camera.transform.position.y,-10);
    }
}
