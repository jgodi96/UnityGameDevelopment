using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Rocket : MonoBehaviour
{
    Rigidbody rigidBody;

    // Start is called before the first frame update
    void Start()
    {                                           // act on RigidBody Components in GUI
        rigidBody = GetComponent<Rigidbody>(); //can get a component and work on multiple components
        
    }

    // Update is called once per frame
    void Update()
    {
        ProcessInput();
    }

    private void ProcessInput()
    {

        if (Input.GetKey(KeyCode.Space)) //can thrust while rotating
        {
            rigidBody.AddRelativeForce(Vector3.up);//if angeled, always thrusts towards the top
            print("Thrusting");
        }
        if (Input.GetKey(KeyCode.A))
        {
            transform.Rotate(Vector3.forward); //goes positive on z axis //anticlockwise
        }
        else if (Input.GetKey(KeyCode.D)) //cant rotate while clicking A
        {
            print("Rotating Right");
        }
      
        
    }
}
