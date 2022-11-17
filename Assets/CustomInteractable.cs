using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;

public class CustomInteractable : MonoBehaviour
{
    private MeshRenderer meshRenderer = null;
    private XRGrabInteractable grabInteractable = null;

    public GameObject plane;

    //private void Awake()
    //{
    //    meshRenderer = GetComponent<MeshRenderer>();
    //    grabInteractable = GetComponent<XRGrabInteractable>();

    //    grabInteractable.onActivate.AddListener(SetEnable);
    //    grabInteractable.onDeactivate.AddListener(SetDisable);
    //}

    //private void SetEnable(XRBaseInteractor interactor)
    //{
    //    meshRenderer.enabled = true;
    //}
    //private void SetDisable(XRBaseInteractor interactor)
    //{
    //    meshRenderer.enabled = false;
    //}

    public void SetEnable()
    {
        ////plane.SetActive(true);
        //meshRenderer = plane.GetComponent<MeshRenderer>();
        //meshRenderer.enabled = true;
        Debug.Log("Enable");

        plane.GetComponent<SlicingPlane>().enabled = true;
    }
    public void SetDisable()
    {
        ////plane.SetActive(false);
        //meshRenderer = plane.GetComponent<MeshRenderer>();
        //meshRenderer.sharedMaterial = false;
        Debug.Log("Disable");

        plane.GetComponent<SlicingPlane>().enabled = false;

    }
}
