using System.Runtime.CompilerServices;
using UnityEngine;

/// <summary>
/// Apply forward force to instantiated prefab
/// </summary>
public class LaunchProjectile : MonoBehaviour
{
    [Tooltip("The projectile that's created")]
    public GameObject projectilePrefab = null;

    [Tooltip("The point that the project is created")]
    public Transform startPoint = null;

    [Tooltip("The speed at which the projectile is launched")]
    public float launchSpeed = 1.0f;

    [Tooltip("The lifetime of the projectile before being removed from the scene")]
    public float lifeTime = 5.0f;

    public void Fire()
    {
        GameObject newObject = Instantiate(projectilePrefab, startPoint.position, startPoint.rotation);
        
        if (newObject.TryGetComponent(out Rigidbody rigidBody))
            ApplyForce(rigidBody);
        Destroy(newObject, lifeTime);
        
    }

    private void ApplyForce(Rigidbody rigidBody)
    {
        Vector3 force = startPoint.forward * launchSpeed;
        rigidBody.AddForce(force);
    }

}