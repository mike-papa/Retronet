using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RoomBehaviour : MonoBehaviour
{
    public GameObject[] walls; // 0 - Up, 1 - Down, 2 - Right, 3 - Left 
    public GameObject[] doors; // 0 - Up, 1 - Down, 2 - Right, 3 - Left 


    // Update is called once per frame
    void Update()
    {
        
    }
    
    //which doors are open and which are close, ex. [true, false, false, true] means Up and Left are open
    public void UpdateRoom(bool[] status)  
    {
        for(int i=0; i< status.Length;i++)
        {
            doors[i].SetActive(status[i]);
            walls[i].SetActive(!status[i]);
        }
    }
}
