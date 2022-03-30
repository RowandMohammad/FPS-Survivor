using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class DoorTrigger : MonoBehaviour
{
    private Animator anim;
    private bool isOpen = false;




    // Start is called before the first frame update
    private void OnTriggerEnter(Collider other)
    {
        if(other.gameObject.tag == "Player" || other.gameObject.tag == "Zombie" )
        {
            anim = GetComponentInParent<Animator>();
            if (!isOpen)
            {
                
                anim.SetTrigger("OpenDoor");
                isOpen = true;
            }
        }
        
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.tag == "Player" || other.gameObject.tag == "Zombie")
        {
            anim = GetComponentInParent<Animator>();
            if (isOpen)
            {
                
                anim.SetTrigger("CloseDoor");
                isOpen = false;
            }
        }

    }

}
