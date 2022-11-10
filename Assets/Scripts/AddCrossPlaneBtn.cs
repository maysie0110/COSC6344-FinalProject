using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AddCrossPlaneBtn : MonoBehaviour
{
    public VolumeRenderedObject volumeRenderedObject;
    public void OnBtnClick()
    {
        VolumeObjectFactory.SpawnCrossSectionPlane(volumeRenderedObject);
    }
}
