using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
public class Target : MonoBehaviour
{
    [SerializeField] private Collider checkCollider;
    [SerializeField] private Rigidbody[] arrayRigidbody;
    
    private bool _isDestroy;
    private GameObject _gameObjectBullet;

    public bool IsDestroy => _isDestroy;

    public void StartActionTarget()
    {
        for (var i = 0; i < arrayRigidbody.Length; i++)
        {
            arrayRigidbody[i].useGravity = false;
        }
        
        gameObject.SetActive(true);
    }

    public void RepeatTarget()
    {
        Destroy(gameObject);
        Destroy(_gameObjectBullet);
    }
    
    private void OnTriggerEnter(Collider other)
    {
        if(other.CompareTag("Target"))
            return;

        checkCollider.enabled = false;
        
        for (var i = 0; i < arrayRigidbody.Length; i++)
        {
            arrayRigidbody[i].useGravity = true;
        }
        
        GetComponent<Rigidbody>().AddForceAtPosition(other.transform.position, other.transform.forward, ForceMode.Impulse);
        _isDestroy = true;

        if(_gameObjectBullet == null)
          _gameObjectBullet = other.gameObject;
    }
}
