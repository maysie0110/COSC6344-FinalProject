using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class MaxValueVisibility : MonoBehaviour
{
    public VolumeRenderedObject volumeRenderedObject;
    public void OnSliderValueChanged()
    { 
        Slider slider = GetComponent<Slider>();
        volumeRenderedObject.SetVisibilityWindowMax(slider.value);
    }
}
