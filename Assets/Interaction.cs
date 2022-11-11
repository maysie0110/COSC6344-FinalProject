using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Interaction : MonoBehaviour
{
    public VolumeRenderedObject volumeRenderedObject;
    // Update is called once per frame
    void Update()
    {
        if (OVRInput.Get(OVRInput.Button.One, OVRInput.Controller.RTouch))
        {
            //Debug.Log("Button One Pressed!");
            //Application.Quit();
            volumeRenderedObject.SetRenderMode(VolumeRenderMode.DirectVolumeRendering);
        }
        if (OVRInput.Get(OVRInput.Button.Two, OVRInput.Controller.RTouch))
        {
            //Debug.Log("Button Two Pressed!");
            volumeRenderedObject.SetRenderMode(VolumeRenderMode.IsosurfaceRendering);
        }

        // returns a Vector2 of the primary thumbstick’s current state.
        // (X/Y range of -1.0f to 1.0f)
        Vector2 value = OVRInput.Get(OVRInput.Axis2D.PrimaryThumbstick, OVRInput.Controller.RTouch);
        //Debug.Log(value.x);
        volumeRenderedObject.SetVisibilityWindowMin(value.x);
    }
}
