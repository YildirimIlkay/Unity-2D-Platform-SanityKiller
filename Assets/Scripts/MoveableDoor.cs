using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveableDoorTrigger : MonoBehaviour
{
    //private bool triggered = false;
    public int timeTriggered = 0;
    public Transform Door;
    public BoxCollider2D DoorBc;
    public GameObject ground;
    public GameObject brickLvl5;




    void OnTriggerEnter2D(Collider2D other)
    {
        //if (triggered) return;

        if (other.CompareTag("Player"))
        {
            //triggered = true;

            if (timeTriggered ==1)
            {
                GameManager.instance.NextLevel();
            }
            brickLvl5.SetActive(true); 
            timeTriggered++;
            Door.transform.position = new Vector3(167,0,5);
            DoorBc.transform.position = new Vector3(167,0,5);
            Destroy(ground);
        }
    }
}
