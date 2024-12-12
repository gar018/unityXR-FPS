using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnProjectile : MonoBehaviour
{
    /*
    References a user-specified projectile and velocity, 
    and summons a projectile of that type at that velocity
    in the Z direction of the game object the script component is applied.
    Note: this applies a Rigidbody to be functional
    */
    public GameObject projectilePrefab = null;
    public float projectileSpeed = 0f;

    

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public GameObject FireProjectile()
    {
        if (projectilePrefab != null) {
            //Create a projectile at the parent transform
            GameObject activeProjectile = Instantiate(projectilePrefab, this.transform);

            //TODO delete this old line of code when certain everything is fine and dandy
            //Rigidbody projectilePhysics; 

            //Checks initially if a Rigidbody component DOESNT exist for an object, if so, make one
            if (activeProjectile.TryGetComponent<Rigidbody>(out Rigidbody projectilePhysics)) {
                activeProjectile.AddComponent<Rigidbody>();
            }
            //... then add force
            projectilePhysics.AddForce(this.transform.forward * projectileSpeed);
            return activeProjectile;
        }
        return null;
    }
}
