using UnityEngine;

/// <summary>
/// This script makes it easier to toggle between a new material, and the objects original material.
/// </summary>
public class ChangeMaterial : MonoBehaviour
{
    [Tooltip("The material that's switched to.")]
    public Material otherMaterial = null;

    private bool usingOther = false;
    public MeshRenderer meshRenderer;
    private Material originalMaterial = null;

    private void Awake()
    {
        if (meshRenderer == null)
        {
            meshRenderer = GetComponent<MeshRenderer>();
        }
        originalMaterial = meshRenderer.material;
    }

    public void SetOtherMaterial()
    {
        usingOther = true;
        meshRenderer.material = otherMaterial;
    }

    public void SetOriginalMaterial()
    {
        usingOther = false;
        meshRenderer.material = originalMaterial;
    }

    public void ToggleMaterial()
    {
        usingOther = !usingOther;

        if(usingOther)
        {
            meshRenderer.material = otherMaterial;
        }
        else
        {
            meshRenderer.material = originalMaterial;
        }
    }
}
