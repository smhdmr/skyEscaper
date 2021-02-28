using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;
using System.Linq;                  //for using ForEach method



/*
 *This script must work on the game manager.
 * It's managing camera and platform hole positions.
 */



public class gameMaker : MonoBehaviour
{
    //for access gameMaker script from platform class
    public static gameMaker Instance;
    
    
    //add components 
    public Camera cam;                  //add camera
    public GameObject character;        //add character
    public Transform barriers;          //add left and right barriers
    
    
    //platforms list
    [SerializeField] private List<Transform> platforms;
    
    
    //variables
    [SerializeField] private float cameraSpeed = 1.0f;              //camera speed
    [SerializeField] private float offsetY = 5.0f;                  //camera Y follow offset
    [SerializeField] private float barrierOffset = 3.0f;            //barriers offset
    [SerializeField] public Transform lastPlatform;                 //last platform position
    [SerializeField] public bool isLastPlatformChanged = false;     //a bool for change last platforms hole position
    
    

    // Start is called before the first frame update
    void Start()
    {
        changeStartHoles();     //change the first 3 platforms position randomly when game starts
    }


    
    //a unity method which is called when game awakes
    private void Awake()
    {
        Instance = this;        //for access this script from platform class
    }


    
    // Update is called once per frame
    void Update()
    {
        //change camera position according to the character position
        cam.transform.position = new Vector3(cam.transform.position.x, character.transform.position.y - offsetY, cam.transform.position.z);
        
        
        //change last platforms hole position randomly
        if (isLastPlatformChanged)
        {
            changeLastHole();
            isLastPlatformChanged = false;
            barriers.transform.position = new Vector3(barriers.transform.position.x, character.transform.position.y - barrierOffset, barriers.transform.position.z);
        }

    }


    
    //a method for changing hole positions when game starts
    void changeStartHoles()
    {
        //change all platforms position according to the given position range
        platforms.ForEach(p => p.position = new Vector3(Random.Range(-13f, -1f), p.position.y, p.position.z));
    }



    //a method for change last hole position when last platform changes
    void changeLastHole()
    {
        lastPlatform.transform.position = new Vector3(Random.Range(-13f, -1f), lastPlatform.position.y, lastPlatform.position.z);
    }



    void moveCamera()
    {
        
       
        
    }
    
}
