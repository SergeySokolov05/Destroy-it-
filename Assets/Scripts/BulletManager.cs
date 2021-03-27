using System;
using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using UnityEngine;

public class BulletManager : MonoBehaviour
{
    [SerializeField] private Transform[] arrayPositionBullet;

    private List<Bullet> _listBulleds;
    private bool _isShotStart;
    private Ray _ray;
    private RaycastHit _hit;
    private Camera _camera;
    private Bullet _target;
    private Bullet _bullet;

    public bool IsShotStart
    {
        set => _isShotStart = value;
    }
    public Bullet Bullet => _bullet;

    public int CountBullet
    {
        get
        {
            var tempCountBullet = 0;

            for (var i = 0; i < _listBulleds.Count; i++)
            {
                if (_listBulleds[i] != null)
                    tempCountBullet++;
            }

            return tempCountBullet;
        }
    }
    
    private void Start()
    {
        _listBulleds = new List<Bullet>();
        _bullet = Resources.Load<Bullet>("Prefabs/Bullet");
        _camera = Camera.main;
    }

    private void Update()
    {
        if(_isShotStart)
            return;
        
        if (Input.GetMouseButtonDown(0))
        {
            _ray = _camera.ScreenPointToRay(Input.mousePosition);
         
            if (Physics.Raycast(_ray, out _hit, 100, LayerMask.GetMask("Bullet")))
                _target = _hit.collider.gameObject.GetComponent<Bullet>();
        }
        else if (Input.GetMouseButton(0))
        {
            if(_target == null)
                return;
         
            _ray = _camera.ScreenPointToRay(Input.mousePosition);
         
            if (Physics.Raycast(_ray, out _hit, 100, LayerMask.GetMask("Plane")))
            {
                _target.transform.DOMove(new Vector3(_hit.point.x, _target.transform.position.y, _hit.point.z), 
                    Time.deltaTime);
            }
        }
        else if(Input.GetMouseButtonUp(0))
        {
            if(_target == null)
                return;
                
            _target.Rigidbody.Sleep();
            _target = null;
        }
    }

    public void StartGame()
    {
        _isShotStart = false;
        
        for (var i = 0; i < _listBulleds.Count; i++)
        {
            if(_listBulleds[i] != null)
                Destroy(_listBulleds[i].gameObject);
        }
        
        _listBulleds.Clear();
        
        for (var i = 0; i < arrayPositionBullet.Length; i++)
        {
            Bullet bullet = Instantiate(_bullet, arrayPositionBullet[i].position, Quaternion.identity, arrayPositionBullet[i]);
            bullet.transform.localScale = Vector3.zero;
            _listBulleds.Add(bullet);
            
            Sequence sequence = DOTween.Sequence();
            sequence.Append(bullet.transform.DOScale(Vector3.one, 0.5f).SetEase(Ease.InBounce));
        }
    }
}