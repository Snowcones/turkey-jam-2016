﻿using UnityEngine;
using System.Collections;



public class Zombie : MonoBehaviour
{

    public AudioSource zombie;

    // Use this for initialization
    void Awake()
    {
       zombie = GetComponent<AudioSource>();
    }

    // Update is called once per frame
    void Update()
    {

    }
    void OnCollisionEnter(Collision col)
    {
        if (col.gameObject.name == "Bullet")
        {  Destroy(gameObject);
           zombie.Play();
            
        }
    }
}
