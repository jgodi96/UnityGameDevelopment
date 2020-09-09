using UnityEngine;
using UnityEngine.SceneManagement;

public class Rocket : MonoBehaviour
{
    
    [SerializeField] float rcThrust = 100f;
    [SerializeField] float mainThrust = 100f;
    [SerializeField] float levelLoadDelay = 2f;

    [SerializeField] AudioClip mainEngine;
    [SerializeField] AudioClip Success;
    [SerializeField] AudioClip Dead;

    [SerializeField] ParticleSystem mainEngineParticles;
    [SerializeField] ParticleSystem SuccessParticles;
    [SerializeField] ParticleSystem DeadParticles;


    Rigidbody rigidBody;
    AudioSource audioSource;

    enum State { Alive,Dying, Transcending}
    State state = State.Alive;

    // Start is called before the first frame update
    void Start()
    {                                           // act on RigidBody Components in GUI
        rigidBody = GetComponent<Rigidbody>();//can get a component and work on multiple components
        audioSource = GetComponent<AudioSource>();
        
    }

    // Update is called once per frame
    void Update()
    {  
        if (state == State.Alive)
        {
            RespondToThrustInput();
            RespondToRotateInput();
        }
    }

    void OnCollisionEnter(Collision collision)
    {
        if (state != State.Alive) //guard, doesnt run the switch statement //ignore collisions when dead
        {
            return;
        }
       switch(collision.gameObject.tag)
        {
            case "Friendly":
                break;
            case "Finish":
                StartSuccessSequence();
                break;
            default:
                StartDeathSequence();
                break;


        }

    }

    private void StartSuccessSequence()
    {
        //print("done");
        state = State.Transcending;
        audioSource.Stop();
        audioSource.PlayOneShot(Success);
        SuccessParticles.Play();
        Invoke("LoadNextScene", levelLoadDelay); //paramerterized time
    }

    private void StartDeathSequence()
    {
        //print("dead");
        state = State.Dying;
        audioSource.Stop();
        audioSource.PlayOneShot(Dead);
        DeadParticles.Play();
        mainEngineParticles.Stop();
        Invoke("LoadFirstLevel", levelLoadDelay);
    }

    private void LoadFirstLevel()
    {
        SceneManager.LoadScene(0);
    }

    private void LoadNextScene() //load level when hit
    {
        SceneManager.LoadScene(1);
    }

    private void RespondToThrustInput()
    {
        
        if (Input.GetKey(KeyCode.Space)) //can thrust while rotating
        {
            ApplyThrust();

        }
        else
        {
            audioSource.Stop();
            mainEngineParticles.Stop();
        }
       
    }

    private void ApplyThrust()
    {
        rigidBody.AddRelativeForce(Vector3.up * mainThrust * Time.deltaTime);//if angeled, always thrusts towards the top

        if (!audioSource.isPlaying)
        {
            audioSource.PlayOneShot(mainEngine);
        }
        mainEngineParticles.Play();
    }

    private void RespondToRotateInput()
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
