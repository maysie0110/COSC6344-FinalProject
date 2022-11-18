using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class SelectRenderMode : MonoBehaviour
{
    public VolumeRenderedObject volumeRenderedObject;
    public void DropdownValueChanged()
    {
        TMP_Dropdown tMP_Dropdown = GetComponent<TMP_Dropdown>();

        //Debug.Log(tMP_Dropdown.value);
        if (tMP_Dropdown.value == 0)
        {
            volumeRenderedObject.SetRenderMode(VolumeRenderMode.DirectVolumeRendering);
        }
        else if (tMP_Dropdown.value == 1)
        {
            volumeRenderedObject.SetRenderMode(VolumeRenderMode.IsosurfaceRendering);
        }
        else if (tMP_Dropdown.value == 2)
        {
            volumeRenderedObject.SetRenderMode(VolumeRenderMode.LocalMaximumIntensityProjectipon);
        }
        else if (tMP_Dropdown.value == 3)
        {
            volumeRenderedObject.SetRenderMode(VolumeRenderMode.MaximumIntensityProjectipon);
        }
    }
}
