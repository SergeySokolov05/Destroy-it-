using System;
using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
public class Bullet : MonoBehaviour
{
    [SerializeField] private Rigidbody rigidbody;

    public Rigidbody Rigidbody => rigidbody;
    

    private void OnTriggerEnter(Collider other)
    {
        if(other.gameObject.CompareTag("Zone"))
            return;
        
        if (other.gameObject.CompareTag("Gun"))
        {
            GameManager.instance.StarShot();
            Destroy(gameObject);
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.CompareTag("Zone"))
        {
            Destroy(gameObject);
            GameManager.instance.UiManager.CalculateCountBullet(true, GameManager.instance.BulletManager.CountBullet - 1);
        }
    }
}