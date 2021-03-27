using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Gun : MonoBehaviour
{
    [SerializeField] private Zone zone;
    [SerializeField] private Transform positionShotBullet;
    [SerializeField] private float forgeBullet;
    
    private BoxCollider _boxCollider;
    private Animator _animator;
    private bool _isHitTarget;
    
    public int CountBullet => zone.GetCountBullet();
    
    private void Start()
    {
        _boxCollider = GetComponent<BoxCollider>();
        _animator = GetComponent<Animator>();
    }

    public void Shot(bool isHitTarget)
    {
        _isHitTarget = isHitTarget;
        _animator.SetTrigger("Shoot");
        zone.CalculateZone(false);
    }

    public void RepeatShot()
    {
        zone.CalculateZone(true);
        _boxCollider.enabled = true;
    }

    //Animation Trigger
    private void FinishAnimation()
    {
        _boxCollider.enabled = false;
        Bullet tempBullet = Instantiate(GameManager.instance.BulletManager.Bullet, positionShotBullet.position, Quaternion.identity);
        tempBullet.Rigidbody.AddForce(positionShotBullet.position * (_isHitTarget ? forgeBullet : forgeBullet / 2), ForceMode.Impulse);
    }
}
