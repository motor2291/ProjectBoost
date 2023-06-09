using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Movement : MonoBehaviour
{
    // PARAMETERS - for turning, typically set the editor
    // CACHE - e.g. references for readability or speed
    // STATE - private instance (member) variables

    [SerializeField] float mainThrust = 1000f;
    [SerializeField] float rotationThrust = 100f;
    [SerializeField] AudioClip mainEngine;

    [SerializeField] ParticleSystem mainParticles;
    [SerializeField] ParticleSystem sideOneParticles;
    [SerializeField] ParticleSystem sideTwoParticles;
    [SerializeField] ParticleSystem sideThreeParticles;

    Rigidbody rb;
    AudioSource audioSource;
   
    void Start()
    {
        rb = GetComponent<Rigidbody>();
        audioSource = GetComponent<AudioSource>();
    }

    void Update()
    {
        ProcessThrust();
        ProcessRotation();
        
    }

    void ProcessThrust()
    {
        if (Input.GetKey(KeyCode.Space))
        {
            StartThrusting();
        }
        else
        {
            audioSource.Stop();
            mainParticles.Stop();
        }
    }

    void StartThrusting()
    {
        rb.AddRelativeForce(Vector3.up * mainThrust * Time.deltaTime);
        if (!audioSource.isPlaying)
        {
            audioSource.PlayOneShot(mainEngine);
        }
        if (!mainParticles.isPlaying)
        {
            mainParticles.Play();
        }
    }

    void ProcessRotation()
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
            sideOneParticles.Stop();
            sideTwoParticles.Stop();
            sideThreeParticles.Stop();
        }
    }

    private void RotateRight()
    {
        ApplyRotation(-rotationThrust);
        if (!sideOneParticles.isPlaying)
        {
            sideOneParticles.Play();
            sideTwoParticles.Play();
            sideThreeParticles.Play();
        }
    }

    void RotateLeft()
    {
        ApplyRotation(rotationThrust);
        if (!sideOneParticles.isPlaying)
        {
            sideOneParticles.Play();
            sideTwoParticles.Play();
            sideThreeParticles.Play();
        }
    }

    void ApplyRotation(float rotationThisFrame)
    {
        rb.freezeRotation = true; // freezing roatation so we can manually rotate
        transform.Rotate(Vector3.forward * rotationThisFrame * Time.deltaTime);
        rb.freezeRotation = false; // unfreezing rotation so the physic system can take over
    }
}
