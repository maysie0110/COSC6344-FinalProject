using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MinValueVisibility : MonoBehaviour
{
    public VolumeRenderedObject volumeRenderedObject;
    public void OnSliderValueChanged()
    {
        Slider slider = GetComponent<Slider>();
        volumeRenderedObject.SetVisibilityWindowMin(slider.value);
    }
}
