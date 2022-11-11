using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class AddSlicePlaneBtn : MonoBehaviour
{
    public VolumeRenderedObject volumeRenderedObject;
    public void OnBtnClick()
    {
        volumeRenderedObject.CreateSlicingPlane();
    }
}
