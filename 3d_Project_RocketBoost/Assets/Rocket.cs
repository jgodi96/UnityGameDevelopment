using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Rocket : MonoBehaviour
{
    [SerializeField] float rcThrust = 100f;
    [SerializeField] float mainThrust = 100f;
    Rigidbody rigidBody;
    AudioSource audioSource;

    // Start is called before the first frame update
    void Start()
    {                                           // act on RigidBody Components in GUI
        rigidBody = GetComponent<Rigidbody>();//can get a component and work on multiple components
        audioSource = GetComponent<AudioSource>();
        
    }

    // Update is called once per frame
    void Update()
    {   Thrust();
        Rotate();     
    }

    void OnCollisionEnter(Collision collision)
    {
       switch(collision.gameObject.tag)
        {
            case "Friendly":
                print("OK");
                break;
            case "Fuel":
                print("Fuel");
                break;
            default:
                print("Dead");
                break;

        }

    }

    private void Thrust()
    {
        
        if (Input.GetKey(KeyCode.Space)) //can thrust while rotating
        {

            rigidBody.AddRelativeForce(Vector3.up * mainThrust);//if angeled, always thrusts towards the top

            if (!audioSource.isPlaying)
            {
                audioSource.Play();
            }

        }
        else
        {
            audioSource.Stop();
        }
    }

    private void Rotate()
    {
        rigidBody.freezeRotation = true; //take manual control of rotation
        float rotationThisFrame = rcThrust * Time.deltaTime;
        if (Input.GetKey(KeyCode.A))
        {
            
            transform.Rotate(Vector3.forward  * rotationThisFrame); //goes positive on z axis //anticlockwise
        }
        else if (Input.GetKey(KeyCode.D)) //cant rotate while clicking A
        {
       
            transform.Rotate(-Vector3.forward * rotationThisFrame);
        }
        rigidBody.freezeRotation = false; //resume to physics control

    }
}
