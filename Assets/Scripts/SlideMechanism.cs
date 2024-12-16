using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;

public class SlideMechanism : XRSimpleInteractable
{
    public float forcePower;
    public float slidePullbackDistance;

    public ChangeMaterial affordanceGlow;

    private Rigidbody rb;
    private ConfigurableJoint cj;

    override protected void Awake()
    {
        rb = GetComponent<Rigidbody>();
        cj = GetComponent<ConfigurableJoint>();

        base.Awake();
    }
    public void Fire()
    {
            rb.AddRelativeForce(Vector3.right*forcePower);
        
    }

    public void DoBlowBack()
    {
            rb.AddRelativeForce(Vector3.right*forcePower);
        
    }

    public void PullbackSlide() 
    {
        //Debug.Log("AIMASSIST: I ACTUALLY PULL BACK THE SLIDE HAHAHAHAHAHAHAH");
        cj.targetPosition = new Vector3(slidePullbackDistance,0,0);
    }

    public void ReleaseSlide()
    {
        //Debug.Log("AIMASSIST: I ACTUALLY RELEASE THE SLIDE HAHAHAHAHAHAHAH");
        cj.targetPosition = Vector3.zero;
    }

    public void EnableGlow()
    {
        affordanceGlow.SetOtherMaterial();
    }

    public void DisableGlow()
    {
        affordanceGlow.SetOriginalMaterial();
    }
}

