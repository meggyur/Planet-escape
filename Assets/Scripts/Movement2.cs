using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Movement2 : MonoBehaviour
{
    Rigidbody rb;
    AudioSource aus;
    [SerializeField] float rocketSpeed = 1000f;
    [SerializeField] float turnspeed = 65f;

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody>();
        aus = GetComponent<AudioSource>();
    }

    // Update is called once per frame
    void Update()
    {
        RocketPowerUp();
        ProcessRocketTurn();
    }

    void ProcessRocketTurn()
    {
        if(Input.GetKey(KeyCode.D))
        {
            RocketTurn(turnspeed);
        }
        else if (Input.GetKey(KeyCode.A))
        {
            RocketTurn(-turnspeed);
        }
    }

    void RocketTurn(float thisSpeed)
    {
        rb.freezeRotation = true;
        rb.transform.Rotate(Vector3.forward * thisSpeed * Time.deltaTime);
        rb.freezeRotation = false;
    }


    void RocketPowerUp()
    {
        if(Input.GetKey(KeyCode.Space))
        {
            rb.AddRelativeForce(Vector3.up * rocketSpeed * Time.deltaTime);
            if(!aus.isPlaying)
            {
            aus.Play();
            }
            
        }
        else
        {
            aus.Stop();
        }
    }
}
