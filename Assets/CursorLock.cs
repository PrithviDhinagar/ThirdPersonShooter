using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CursorLock : MonoBehaviour
{
    Rigidbody rb;
    
    void Update()
    {
        if(Input.GetKey(KeyCode.Tab))
        {
            //Freeze all positions
            rb.constraints = RigidbodyConstraints.FreezeRotation;
        }

        //if(Input.GetKeyDown(KeyCode.Tab))
        {
            //Cursor.lockState = CursorLockMode.Locked;
            //Cursor.visible = !Cursor.visible;
        }
    }
}
