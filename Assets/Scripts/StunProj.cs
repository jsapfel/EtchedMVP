using System;
using System.Collections;
using System.Collections.Generic;
using RPGCharacterAnimsFREE;
using UnityEngine;

public class StunProj : MonoBehaviour
{
    private PlayerController player;
    private Rigidbody _rb;

    private void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerController>();
        _rb = GetComponent<Rigidbody>();
    }

    private void OnTriggerEnter(Collider other)
    {
        if (!other.CompareTag("Enemy1")) return;
        
        //if (other.CompareTag("Player"))
          //  player.Stun(3f);
        Destroy(gameObject);
    }
}
