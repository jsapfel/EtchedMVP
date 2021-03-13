using System;
using System.Collections;
using System.Collections.Generic;
using RPGCharacterAnimsFREE;
using UnityEngine;

public class KbProj : MonoBehaviour
{
    private RPGCharacterControllerFREE player;
    private Rigidbody _rb;

    private void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player").GetComponent<RPGCharacterControllerFREE>();
        _rb = GetComponent<Rigidbody>();
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Enemy1")) return;
        
        if (other.CompareTag("Player"))
            player.GetHit(_rb.velocity.normalized);
        Destroy(gameObject);
    }
}
