using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Interaction : MonoBehaviour
{
    public List<GameObject> renderedObjects;
    public GameObject renderedObject;

    private VolumeRenderedObject volumeRenderedObject;


    private VolumeRenderMode renderMode;

    private Vector2 visibilityValue;

    private GameObject crossSectionObject;
    private GameObject meshContainer;

    private static List<VolumeRenderMode> renderModes;

    bool active = false;
    int i = 0;
    int j = 0;
    void Start()
    {
        volumeRenderedObject = renderedObject.GetComponent<VolumeRenderedObject>();
        crossSectionObject = volumeRenderedObject.transform.GetChild(1).gameObject;
        crossSectionObject.SetActive(active);

        meshContainer = volumeRenderedObject.transform.GetChild(0).gameObject;

        renderModes = new List<VolumeRenderMode>();
        renderModes.Add(VolumeRenderMode.DirectVolumeRendering);
        renderModes.Add(VolumeRenderMode.MaximumIntensityProjectipon);
        renderModes.Add(VolumeRenderMode.IsosurfaceRendering);
    }
    // Update is called once per frame
    void Update()
    {

        /*
         * Changing data set using button
         */
        if(OVRInput.GetDown(OVRInput.Button.One, OVRInput.Controller.LTouch))
        {
            i++;
            if (i > 2) i = 0; //Reset index

            if(renderedObject != renderedObjects[i])
            {
                renderedObject.SetActive(false);
                renderedObjects[i].SetActive(true);

                volumeRenderedObject = renderedObjects[i].GetComponent<VolumeRenderedObject>();
                crossSectionObject = volumeRenderedObject.transform.GetChild(1).gameObject;
                crossSectionObject.SetActive(active);
                meshContainer = volumeRenderedObject.transform.GetChild(0).gameObject;

                renderedObject = renderedObjects[i];
            }
        }
        

        /*
        * Changing render mode using button
        */
        renderMode = volumeRenderedObject.GetRenderMode();
        if (OVRInput.GetDown(OVRInput.Button.One, OVRInput.Controller.RTouch))
        {
            j++;
            if (j > 2) j = 0; //Reset index

            if(renderMode != renderModes[j])
                volumeRenderedObject.SetRenderMode(renderModes[j]);

            //if (renderMode != VolumeRenderMode.DirectVolumeRendering)
            //    volumeRenderedObject.SetRenderMode(VolumeRenderMode.DirectVolumeRendering);

            //else if (renderMode != VolumeRenderMode.IsosurfaceRendering)
            //    volumeRenderedObject.SetRenderMode(VolumeRenderMode.IsosurfaceRendering);

            //    if (renderMode != VolumeRenderMode.MaximumIntensityProjectipon)
            //        volumeRenderedObject.SetRenderMode(VolumeRenderMode.MaximumIntensityProjectipon);

        }


        /*
        * Change visibility using right thumbstick 
        */
        visibilityValue = volumeRenderedObject.GetVisibilityWindow();
        //if (OVRInput.GetDown(OVRInput.Touch.PrimaryThumbstick, OVRInput.Controller.RTouch))
        if (OVRInput.Get(OVRInput.Button.PrimaryThumbstick, OVRInput.Controller.RTouch))
        {
            Debug.Log("Pressed Thumbstick");

            //// returns a Vector2 of the primary thumbstick’s current state.
            //// (X/Y range of -1.0f to 1.0f)
            visibilityValue = OVRInput.Get(OVRInput.Axis2D.PrimaryThumbstick, OVRInput.Controller.RTouch);
            volumeRenderedObject.SetVisibilityWindowMin(visibilityValue.x);
            //Debug.Log(visibilityValue);
        }
        //else if (!OVRInput.Get(OVRInput.Button.PrimaryThumbstick, OVRInput.Controller.RTouch))
        //{
        //    Debug.Log("Released Thumbstick");
        //}


        /*
         * Enable/Disable Cut plane
         */
        
        if (OVRInput.GetDown(OVRInput.Button.Two, OVRInput.Controller.RTouch))
        {
            //volumeRenderedObject.GetCrossSectionManager().AddCrossSectionObject(crossSectionObject);
            active = !active;
            //Debug.Log(active);
            crossSectionObject.SetActive(active);
        }

        ///*
        // * Rotate/Move Cut plane 
        // */
        //if (OVRInput.Get(OVRInput.Button.PrimaryThumbstick, OVRInput.Controller.LTouch))
        //{
        //    Vector2 axis = OVRInput.Get(OVRInput.Axis2D.PrimaryThumbstick, OVRInput.Controller.LTouch);
        //    //Vector3 direction = new Vector3(axis.x, 0, 0);
        //    //Quaternion rotation = Quaternion.LookRotation(direction, Vector3.up);
        //    //crossSectionObject.transform.rotation = rotation;

        //    float angle = Mathf.Atan2(axis.y, axis.x) * Mathf.Rad2Deg;
        //    Debug.Log("Angle: " + angle);
        //}

        if (OVRInput.Get(OVRInput.Button.PrimaryIndexTrigger, OVRInput.Controller.RTouch))
        {
            Vector3 position = OVRInput.GetLocalControllerPosition(OVRInput.Controller.RTouch);
            Quaternion rotation = OVRInput.GetLocalControllerRotation(OVRInput.Controller.RTouch);

            //Vector3 position = UnityEngine.XR.InputTracking.GetLocalPosition(UnityEngine.XR.XRNode.CenterEye);
            //Debug.Log(position);
            ////Debug.Log(rotation);
            ////Debug.Log(rotation.eulerAngles);
            position.y = position.y + 1.0f;
            //Debug.Log(position);



            //crossSectionObject.transform.position = position;
            ////Debug.Log(meshContainer.GetComponent<Collider>().bounds.Contains(position));
            if (meshContainer.GetComponent<Collider>().bounds.Contains(position))
            {
                Debug.Log("Bounds contain the point : " + position);
                crossSectionObject.transform.position = position;
                crossSectionObject.transform.rotation = rotation;
            }

            //Debug.Log("MAX: " + meshContainer.GetComponent<Collider>().bounds.max);
            //Debug.Log("MIN: " + meshContainer.GetComponent<Collider>().bounds.min);



        }

    }
}
