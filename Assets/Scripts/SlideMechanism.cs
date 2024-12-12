using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SlideMechanism : MonoBehaviour
{
    public float forcePower;

    private Rigidbody rb;

    private void Awake()
    {
        rb = GetComponent<Rigidbody>();
    }
    public void Fire()
    {
            rb.AddRelativeForce(Vector3.right*forcePower);
        
    }
}

