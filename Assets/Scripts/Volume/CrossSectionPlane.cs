using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CrossSectionPlane : MonoBehaviour, CrossSectionObject
{
    /// <summary>
    /// Volume dataset to cross section.
    /// </summary>
    [SerializeField]
    private VolumeRenderedObject targetObject;

    private void OnEnable()
    {
        if (targetObject != null)
            targetObject.GetCrossSectionManager().AddCrossSectionObject(this);
    }

    private void OnDisable()
    {
        if (targetObject != null)
            targetObject.GetCrossSectionManager().RemoveCrossSectionObject(this);
    }

    public void SetTargetObject(VolumeRenderedObject target)
    {
        if (this.enabled && targetObject != null)
            targetObject.GetCrossSectionManager().RemoveCrossSectionObject(this);

        targetObject = target;

        if (this.enabled && targetObject != null)
            targetObject.GetCrossSectionManager().AddCrossSectionObject(this);
    }

    public CrossSectionType GetCrossSectionType()
    {
        return CrossSectionType.Plane;
    }

    public Matrix4x4 GetMatrix()
    {
        return transform.worldToLocalMatrix * targetObject.transform.localToWorldMatrix;
    }

    public void SetEnable()
    {
        Debug.Log("Enable");
        if (targetObject != null)
            targetObject.GetCrossSectionManager().AddCrossSectionObject(this);
    }

    public void SetDisable()
    {
        Debug.Log("Disable");
        if (targetObject != null)
            targetObject.GetCrossSectionManager().RemoveCrossSectionObject(this);
    }
}
