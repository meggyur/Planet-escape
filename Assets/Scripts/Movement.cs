using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Movement : MonoBehaviour
{
    [SerializeField] float flyingForce = 1000f;
    [SerializeField] float rotationSpeed = 60f;
    [SerializeField] AudioClip mainEngine;
    [SerializeField] ParticleSystem thrusting;
    [SerializeField] ParticleSystem leftThrusting;
    [SerializeField] ParticleSystem rightThrusting;

    Rigidbody rb;
    AudioSource aus;
    
    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody>();
        aus = GetComponent<AudioSource>();
    }

    // Update is called once per frame
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
            StopThrusting();
        }
    }

void ProcessRotation()
    {
         if (Input.GetKey(KeyCode.A))
        {
            StartRotatingRight();
        }
        else if (Input.GetKey(KeyCode.D))
        {
            StartRotatingLeft();
        }
        else
        {
            StopRotation();
        }
    }

    void StartThrusting()
    {
        rb.AddRelativeForce(Vector3.up * Time.deltaTime * flyingForce);

        if (!aus.isPlaying && !thrusting.isPlaying)
        {
            aus.PlayOneShot(mainEngine);
            thrusting.Play();
        }
    }

    void StopThrusting()
    {
        aus.Stop();
        thrusting.Stop();
    }

    void StartRotatingRight()
    {
        ApplyRotation(rotationSpeed);
        if (!rightThrusting.isPlaying)
        {
            rightThrusting.Play();
        }
    }

    void StartRotatingLeft()
    {
        ApplyRotation(-rotationSpeed);
        if (!leftThrusting.isPlaying)
        {
            leftThrusting.Play();
        }
    }

    void ApplyRotation(float rotationThisframe)
    {
        rb.freezeRotation = true;
        transform.Rotate(Vector3.forward * rotationThisframe * Time.deltaTime);
        rb.freezeRotation = false;
    }

    void StopRotation()
    {
        rightThrusting.Stop();
        leftThrusting.Stop();
    }
}
