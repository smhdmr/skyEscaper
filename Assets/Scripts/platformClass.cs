using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;



/*
 * This script must work on all platforms.
 * It's managing the platform teleports.
 */



public class platformClass : MonoBehaviour
{
    public SpriteRenderer spriteRenderer;                       //platforms sprite renderer
    private bool isPlatformsReady = false;                      //a variable for fixing platform teleport issues
    [SerializeField] private float teleportOffset = 5.0f;       //distance between platforms
    

    
    private void Start()
    {
        //call platform teleport method with delay
        Invoke("startPlatformTeleport", 1);
    }

    
    
    private void Update()
    {
        //Debug.Log(spriteRenderer.isVisible);
        
        /*
         * if platform isn't seen by camera
         * platforms ready for teleporting and
         * this platform isn't the last platform
         */
        if ((spriteRenderer.isVisible == false) && (isPlatformsReady) && (this.transform != gameMaker.Instance.lastPlatform))
        {
            Debug.Log(this.name+"invisible");                                                                //for debugging
            transform.position = gameMaker.Instance.lastPlatform.position + (Vector3.down * teleportOffset);        //teleport this platform to the bottom             
            gameMaker.Instance.lastPlatform = transform;                                                            //this platform is last platform now
            gameMaker.Instance.isLastPlatformChanged = true;                                                        //last platform changed
        }
        
    }

    
    
    //a method for fixing teleport issues
    void startPlatformTeleport()
    {
        isPlatformsReady = true;
    }
    
}
