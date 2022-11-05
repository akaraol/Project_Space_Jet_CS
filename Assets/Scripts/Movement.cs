using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Movement : MonoBehaviour
{

    // PARAMETERS

    [SerializeField] float rocketThrustForce = 1f;
    [SerializeField] float tuneRotationSpeed = 1f;
    [SerializeField] AudioClip mainEngine;

    [SerializeField] ParticleSystem mainBoosterParticles;
    [SerializeField] ParticleSystem leftBoosterParticles;
    [SerializeField] ParticleSystem rightBoosterParticles;

    // CACHE

    Rigidbody rb;
    AudioSource audioSource;
    //bool firstJump = false;

    // STATE

    bool isAlive;


    // Start is called before the first frame update
    void Start()
    {
        audioSource = GetComponent<AudioSource>();
        rb = GetComponent<Rigidbody>();

    }

    void FixedUpdate(){

       ProcessThrust();
       ProcessRotate();

    }

    // Update is called once per frame
    void LateUpdate()
    {
    }    

    void ProcessThrust() 
    {
        if (Input.GetKey(KeyCode.Space))
        {
            StartThrusting();
        }
        else
        {
            StopThrusting();
        }
    }

    void ProcessRotate()
    {

        if (Input.GetKey(KeyCode.A))
        {
            RotateLeft();
        }
        else if (Input.GetKey(KeyCode.D))
        {
            RotateRight();
        }
        else
        {
            StopRotating();
            StopTorque();
        }
    }
    private void StartThrusting()
    {
        rb.AddRelativeForce(Vector3.up * rocketThrustForce * Time.deltaTime);
        if (!audioSource.isPlaying)
        {
            audioSource.PlayOneShot(mainEngine);
        }
        if (!mainBoosterParticles.isPlaying)
        {
            mainBoosterParticles.Play();
        }
    }
    
    private void RotateRight()
    {
        ApplyRotation(-tuneRotationSpeed);
        if (!leftBoosterParticles.isPlaying)
        {
            leftBoosterParticles.Play();
        }
    }

    private void RotateLeft()
    {
        ApplyRotation(tuneRotationSpeed);
        if (!rightBoosterParticles.isPlaying)
        {
            rightBoosterParticles.Play();

        }
    }
    
    private void StopThrusting()
    {
        audioSource.Stop();
        mainBoosterParticles.Stop();
    }

    void StopTorque ()
    {
        rb.angularVelocity = Vector3.zero;
    }
    
    private void StopRotating()
    {
        rightBoosterParticles.Stop();
        leftBoosterParticles.Stop();
    }
    
    private void ApplyRotation(float rotationThisFrame)
    {

      
      
      rb.AddTorque(new Vector3(rb.position.x, rb.position.y, rotationThisFrame));
      

        //rb.freezeRotation = true; // Freezing rotation so we can manually rotate
        //transform.Rotate(Vector3.forward * rotationThisFrame * Time.deltaTime);
        //rb.freezeRotation = false; // unfreezing rotation so physics system can take over
    }

    /* void FirstThrust()
    {

         if (Input.GetKeyDown(KeyCode.Space) && firstJump == false) 
          {
            
            rb.AddRelativeForce(Vector3.up * firstThrustForce);
            firstJump = true;
          }
    }
    */

}
