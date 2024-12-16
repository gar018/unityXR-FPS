using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;


/*
GUN AFFORDANCE PROVIDER SCRIPT
MADE BY: GORDON ROSE
DATE: 12/14/24
*/

//Weapon Affordance State Enum
public enum GunAffordanceState
{
    EMPTY,
    LOADED,
    OUTOFAMMO,
    CHAMBERED

}

public class GunAffordanceProvider : MonoBehaviour
{
    [Header("Audio")]

    [Tooltip("Audio Source provided on the gun.")]
    [SerializeField] private AudioSource audioSource;

    [Tooltip("Used for firing while the gun is chambered.")]
    [SerializeField] private AudioClip liveFire;

    [Tooltip("Used for firing while the gun is out of ammo or empty")]
    [SerializeField] private AudioClip dryFire;


    [Tooltip("Used for both inserting and ejecting the mag.")]
    [SerializeField] private AudioClip insertMag;

    [Tooltip("Used for both racking and releasing the slide.")]
    [SerializeField] private AudioClip rackSlide;

    [Tooltip("Used to notify when no ammo remains.")]
    [SerializeField] private AudioClip outOfAmmoAlertSound;

    [Tooltip("Used to notify when a bullet has been chambered.")]
    [SerializeField] private AudioClip chamberedAlertSound;
    

    [Header("Particles")]
    //[SerializeField] private ParticleSystem _smoke;
    [SerializeField] private ParticleSystem muzzleFlash;

    [Header("Prefabs")]
    [SerializeField] private GameObject bulletProjectile;
    [SerializeField] private GameObject bulletCasing;
    [SerializeField] private GameObject bullet;

    [Header("Gun Child Object Mapping")]
    [SerializeField] private SlideMechanism slide;
    [SerializeField] private LaunchProjectile muzzle;
    [SerializeField] private LaunchProjectile ejectionPort;

    [SerializeField] private XRSocketInteractor magazineSocket;


    
    //Weapon Logic Variables
    private GunAffordanceState affordanceState = GunAffordanceState.EMPTY; //Holds the state of the gun
    
    private Magazine magazine = null;// The magazine socketed in the weapon (can be Null)
    
    private bool rackingSlide = false;    // dictates certain circumstances whether the gun can fire despite being 
                                    // in a state where it should. An example of this is attempting to fire while
                                    // racking back the slide. not only should it not fire but it should not make any noise,
                                    // including dry fire.

    //Action Methods (public methods)
    public void Fire()
    {
        if (rackingSlide)
        {
            return;
        }
        if (affordanceState == GunAffordanceState.CHAMBERED)
        {
            bool gunHasMoreBullets = (magazine != null && magazine.ammoCount > 0);

            //AUDIO EFFECTS
            PlaySound(liveFire, Random.Range(0.9f, 1.1f));
            if (!gunHasMoreBullets) {
                PlaySound(outOfAmmoAlertSound, -1.0f);
            }

            //VISUAL EFFECTS
            muzzleFlash.Play();
            slide.Fire();
            muzzle.Fire();
            ejectionPort.Fire();
            
            //STATE LOGIC
            if (gunHasMoreBullets)
            {
                magazine.Decrement();
            }
            else
            {
                if (magazine == null) {
                    affordanceState = GunAffordanceState.EMPTY;
                }
                else
                {
                    affordanceState = GunAffordanceState.OUTOFAMMO;
                    magazine.EnableGlow();
                }
            }
            
        }
        else //affordanceState of OUTOFAMMO, EMPTY, LOADED will just make a dry fire sound
        {
            //AUDIO EFFECTS
            PlaySound(dryFire, 1.0f);
        }
    }

    public void RackSlide(SelectEnterEventArgs args)
    {
        rackingSlide = true;

        if (affordanceState == GunAffordanceState.CHAMBERED)
        {
            bool gunHasMoreBullets = (magazine != null && magazine.ammoCount > 0);

            //AUDIO EFFECTS
            PlaySound(rackSlide, 0.6f);
            if (!gunHasMoreBullets) {
                PlaySound(outOfAmmoAlertSound, -1.0f);
            }

            //VISUAL EFFECTS
            slide.PullbackSlide();
            ejectionPort.Fire();

            //STATE LOGIC
            if (magazine == null) {
                    affordanceState = GunAffordanceState.EMPTY;
            }
            else if (!(magazine.ammoCount > 0))
            {
                affordanceState = GunAffordanceState.OUTOFAMMO;
                magazine.EnableGlow();
            }
        }
        else
        {
            //AUDIO EFFECTS
            PlaySound(rackSlide, 0.6f);

            //VISUAL EFFECTS
            slide.PullbackSlide();
        }
    }

    public void ReleaseSlide(SelectExitEventArgs args)
    {
        rackingSlide = false;

        if (affordanceState == GunAffordanceState.CHAMBERED)
        {
            bool gunHasMoreBullets = (magazine != null && magazine.ammoCount > 0);

            //AUDIO EFFECTS
            PlaySound(rackSlide, 0f);

            //VISUAL EFFECTS
            slide.ReleaseSlide();

            //STATE LOGIC
            if (gunHasMoreBullets)
            {
                magazine.Decrement();
            }
            else
            {
                if (magazine == null)
                {
                    affordanceState = GunAffordanceState.EMPTY;
                }
                else
                {
                    affordanceState = GunAffordanceState.OUTOFAMMO;
                }
            }
        }

        
        else if (affordanceState == GunAffordanceState.LOADED)
        {
            //AUDIO EFFECTS
            PlaySound(rackSlide, 0f);
            PlaySound(chamberedAlertSound, 1.0f);

            //VISUAL EFFECTS
            slide.ReleaseSlide();

            //STATE LOGIC
            if(magazine != null)
            {
                magazine.Decrement();
                affordanceState = GunAffordanceState.CHAMBERED;
                slide.DisableGlow();
            }
            else //this state should never be reached. however, this does prevent any weird logic from happening.
            {
                Debug.LogWarning("AIMASSIST: A weird state transition occurred. This needs to be looked into.");
                affordanceState = GunAffordanceState.EMPTY;
            }
            
        }
        else 
        {
            //AUDIO EFFECTS
            PlaySound(rackSlide, 0f);

            //VISUAL EFFECTS
            slide.ReleaseSlide();

        }
    }

    void InsertMag(SelectEnterEventArgs args)
    {
        magazine = args.interactableObject.transform.GetComponent<Magazine>();
        /*if (magazine == null) 
        {
            throw new Exception( "NULL POINTER EXCEPTION: MAGAZINE INSERTED WAS REFERENCED AS NULL" );
            return;
        }*/
        
        if(affordanceState == GunAffordanceState.EMPTY)
        {
            //AUDIO EFFECTS
            PlaySound(insertMag, 1.0f);

            //STATE LOGIC
            if(magazine.ammoCount > 0 ) {
                affordanceState = GunAffordanceState.LOADED;
                slide.EnableGlow();
            }
            else
            {
                affordanceState = GunAffordanceState.OUTOFAMMO;
                magazine.EnableGlow();
            }
            
        }
        else
        {
            //AUDIO EFFECTS
            PlaySound(insertMag, 1.0f);
            
        }
    }

    void EjectMag(SelectExitEventArgs args)
    {
        magazine.DisableGlow();
        slide.DisableGlow();
        magazine = null;
        if(affordanceState == GunAffordanceState.CHAMBERED)
        {
            //AUDIO EFFECTS
            PlaySound(insertMag, 0.5f);
            
        }
        else
        {
            //AUDIO EFFECTS
            PlaySound(insertMag, 0.5f);

            //STATE LOGIC
            affordanceState = GunAffordanceState.EMPTY;
        }

        
    }


    //private methods
    void PlaySound(AudioClip sound, float pitch = 1.0f, float volume = 1.0f)
    {
        float prevPitch = audioSource.pitch;

        audioSource.pitch = pitch;
        audioSource.PlayOneShot(sound, volume);

        audioSource.pitch = prevPitch;
    }

    void OnEnable()
    {
        magazineSocket.selectEntered.AddListener(InsertMag);
        magazineSocket.selectExited.AddListener(EjectMag);
        slide.selectEntered.AddListener(RackSlide);
        slide.selectExited.AddListener(ReleaseSlide);
        
    }

    void OnDisable()
    {
        magazineSocket.selectEntered.RemoveListener(InsertMag);
        magazineSocket.selectExited.RemoveListener(EjectMag);
        slide.selectEntered.RemoveListener(RackSlide);
        slide.selectExited.RemoveListener(ReleaseSlide);
    }

    void UpdateAffordances()
    {
        switch (affordanceState)
        {
            case GunAffordanceState.EMPTY:
                slide.DisableGlow();
            break;

            case GunAffordanceState.LOADED:

            break;

            case GunAffordanceState.OUTOFAMMO:

            break;
            
            case GunAffordanceState.CHAMBERED:

            break;
            
            
        }
    }
}
