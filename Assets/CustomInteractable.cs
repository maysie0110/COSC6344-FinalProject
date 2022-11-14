using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;

public class CustomInteractable : MonoBehaviour
{
    private MeshRenderer meshRenderer = null;
    private XRGrabInteractable grabInteractable = null;

    private void Awake()
    {
        meshRenderer = GetComponent<MeshRenderer>();
        grabInteractable = GetComponent<XRGrabInteractable>();

        grabInteractable.onActivate.AddListener(SetEnable);
        grabInteractable.onDeactivate.AddListener(SetDisable);
    }

    private void SetEnable(XRBaseInteractor interactor)
    {
        meshRenderer.enabled = true;
    }
    private void SetDisable(XRBaseInteractor interactor)
    {
        meshRenderer.enabled = false;
    }
}
