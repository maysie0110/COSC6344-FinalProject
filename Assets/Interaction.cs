using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

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

    public TMP_Text dataNameDisplay;
    public TMP_Text renderModeDisplay;




    bool active = false;
    int i = 0;
    int j = 0;



    public GameObject rightController;

    void Start()
    {
        volumeRenderedObject = renderedObject.GetComponent<VolumeRenderedObject>();
        //crossSectionObject = volumeRenderedObject.transform.GetChild(1).gameObject;
        crossSectionObject = rightController.transform.GetChild(0).gameObject;
        crossSectionObject.SetActive(active);

        meshContainer = volumeRenderedObject.transform.GetChild(0).gameObject;

        renderModes = new List<VolumeRenderMode>();
        renderModes.Add(VolumeRenderMode.DirectVolumeRendering);
        renderModes.Add(VolumeRenderMode.MaximumIntensityProjectipon);
        renderModes.Add(VolumeRenderMode.LocalMaximumIntensityProjectipon);
        renderModes.Add(VolumeRenderMode.IsosurfaceRendering);

        dataNameDisplay.text = GetStringBetweenCharacters(renderedObject.name, '_', '.');
        renderMode = VolumeRenderMode.DirectVolumeRendering;
        renderModeDisplay.text = "Direct Volume Rendering";
    }
    // Update is called once per frame
    void Update()
    {

        /*
         * Changing data set using button
         */
        if (OVRInput.GetDown(OVRInput.Button.One, OVRInput.Controller.LTouch))
        {
            i++;
            if (i > renderedObjects.Count-1) i = 0; //Reset index

            if(renderedObject != renderedObjects[i])
            {
                renderedObject.SetActive(false); // Disable previous data
                renderedObjects[i].SetActive(true); // Enable current data
                renderedObject = renderedObjects[i];

                volumeRenderedObject = renderedObject.GetComponent<VolumeRenderedObject>();
                volumeRenderedObject.SetRenderMode(renderMode);
                //crossSectionObject = volumeRenderedObject.transform.GetChild(1).gameObject;
                crossSectionObject.GetComponent<CrossSectionPlane>().SetTargetObject(volumeRenderedObject);
                crossSectionObject.SetActive(active);
                if (!active) crossSectionObject.GetComponent<CrossSectionPlane>().SetDisable();
                meshContainer = volumeRenderedObject.transform.GetChild(0).gameObject;

                dataNameDisplay.text = GetStringBetweenCharacters(renderedObject.name, '_', '.'); // Update data name display
            }
        }
        

        /*
        * Changing render mode using button
        */
        renderMode = volumeRenderedObject.GetRenderMode();
        if (OVRInput.GetDown(OVRInput.Button.One, OVRInput.Controller.RTouch))
        {
            j++;
            j %= renderModes.Count;

            //if (j > 2) j = 0; //Reset index

            if (renderMode != renderModes[j])
            {
                volumeRenderedObject.SetRenderMode(renderModes[j]);

                if (renderModes[j] == VolumeRenderMode.DirectVolumeRendering)
                    renderModeDisplay.text = "Direct Volume Rendering";
                else if (renderModes[j] == VolumeRenderMode.IsosurfaceRendering)
                    renderModeDisplay.text = "Isosurface Rendering";
                else if (renderModes[j] == VolumeRenderMode.MaximumIntensityProjectipon)
                    renderModeDisplay.text = "Maximum Intensity Projection";
                else if (renderModes[j] == VolumeRenderMode.LocalMaximumIntensityProjectipon)
                    renderModeDisplay.text = "Local Maximum Intensity Projection";
            }
        }


        /*
        * Change visibility or max density using right thumbstick 
        */
        if(renderMode == VolumeRenderMode.LocalMaximumIntensityProjectipon)
        {
            if (OVRInput.Get(OVRInput.Button.PrimaryThumbstick, OVRInput.Controller.RTouch))
            {
                Debug.Log("Pressed Thumbstick");

                // returns a Vector2 of the primary thumbstick’s current state.
                // (X/Y range of -1.0f to 1.0f)
                Vector2 maxDensity = OVRInput.Get(OVRInput.Axis2D.PrimaryThumbstick, OVRInput.Controller.RTouch);
                volumeRenderedObject.SetMaxDensity(maxDensity.x);
            }
        }
        else
        {
            visibilityValue = volumeRenderedObject.GetVisibilityWindow();
            //if (OVRInput.GetDown(OVRInput.Touch.PrimaryThumbstick, OVRInput.Controller.RTouch))
            if (OVRInput.Get(OVRInput.Button.PrimaryThumbstick, OVRInput.Controller.RTouch))
            {
                Debug.Log("Pressed Thumbstick");

                // returns a Vector2 of the primary thumbstick’s current state.
                // (X/Y range of -1.0f to 1.0f)
                visibilityValue = OVRInput.Get(OVRInput.Axis2D.PrimaryThumbstick, OVRInput.Controller.RTouch);
                volumeRenderedObject.SetVisibilityWindowMin(visibilityValue.x);
                //Debug.Log(visibilityValue);
            }
        }



        /*
         * Enable/Disable Cut plane
         */

        if (OVRInput.GetDown(OVRInput.Button.Two, OVRInput.Controller.RTouch))
        {
            
            active = !active;
            crossSectionObject.GetComponent<CrossSectionPlane>().SetTargetObject(volumeRenderedObject);
            crossSectionObject.SetActive(active);

            if(!active) crossSectionObject.GetComponent<CrossSectionPlane>().SetDisable();
        }

        //if (active)
        //{
        //    Vector3 position = rightController.transform.position;
        //    Quaternion rotation = rightController.transform.rotation;

        //    Debug.Log(position);
        //    //Vector3 position = OVRInput.GetLocalControllerPosition(OVRInput.Controller.RTouch);
        //    //Quaternion rotation = OVRInput.GetLocalControllerRotation(OVRInput.Controller.RTouch);
        //    //position.y = position.y + 1.0f;
        //    Debug.Log("MAX: " + meshContainer.GetComponent<Collider>().bounds.max);
        //    Debug.Log("MIN: " + meshContainer.GetComponent<Collider>().bounds.min);
        //    if (meshContainer.GetComponent<Collider>().bounds.Contains(position))
        //    {
                
        //        Debug.Log("Bounds contain the point : " + position);
        //        crossSectionObject.transform.position = position;
        //        crossSectionObject.transform.rotation = rotation;
        //    }
        //}else { crossSectionObject.GetComponent<CrossSectionPlane>().SetDisable(); }

        ///*
        // * Rotate/Move Cut plane 
        // */
        //if (OVRInput.Get(OVRInput.Button.PrimaryIndexTrigger, OVRInput.Controller.RTouch))
        //{
        //    Vector3 position = OVRInput.GetLocalControllerPosition(OVRInput.Controller.RTouch);
        //    Quaternion rotation = OVRInput.GetLocalControllerRotation(OVRInput.Controller.RTouch);

        //    //Vector3 position = UnityEngine.XR.InputTracking.GetLocalPosition(UnityEngine.XR.XRNode.CenterEye);
        //    //Debug.Log(position);
        //    ////Debug.Log(rotation);
        //    ////Debug.Log(rotation.eulerAngles);
        //    position.y = position.y + 1.0f;
        //    //Debug.Log(position);



        //    ////crossSectionObject.transform.position = position;
        //    //Debug.Log(meshContainer.GetComponent<Collider>().bounds.Contains(position));
        //    if (meshContainer.GetComponent<Collider>().bounds.Contains(position))
        //    {
        //        Debug.Log("Bounds contain the point : " + position);
        //        crossSectionObject.transform.position = position;
        //        crossSectionObject.transform.rotation = rotation;
        //    }

        //    //Debug.Log("MAX: " + meshContainer.GetComponent<Collider>().bounds.max);
        //    //Debug.Log("MIN: " + meshContainer.GetComponent<Collider>().bounds.min);



        //}

    }

    /*
     * https://stackoverflow.com/questions/12108582/extracting-string-between-two-characters
     */
    public static string GetStringBetweenCharacters(string input, char charFrom, char charTo)
    {
        int posFrom = input.IndexOf(charFrom);
        if (posFrom != -1) //if found char
        {
            int posTo = input.IndexOf(charTo, posFrom + 1);
            if (posTo != -1) //if found char
            {
                return input.Substring(posFrom + 1, posTo - posFrom - 1);
            }
        }

        return string.Empty;
    }
}
