using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class AddCrossPlaneBtn : MonoBehaviour
{
    public VolumeRenderedObject volumeRenderedObject;
    public void OnBtnClick()
    {
        VolumeObjectFactory.SpawnCrossSectionPlane(volumeRenderedObject);
    }
}
