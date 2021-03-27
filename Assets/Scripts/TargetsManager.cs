using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using DG.Tweening;
using UnityEngine;
using Random = UnityEngine.Random;

public class TargetsManager : MonoBehaviour
{
    private List<Target> _listTarget;
    private Target _target;
    private Coroutine _coroutineVictory;
    
    private void Start()
    {
        _listTarget = new List<Target>();
        var tempObjects = Resources.LoadAll<Target>("Prefabs");
        
        for (var i = 0; i < tempObjects.Length; i++)
        {
            Target target = Instantiate(tempObjects[i], transform);
            target.gameObject.SetActive(false);
            _listTarget.Add(target);
        }
    }

    public void ActionTarget()
    {
        _target = _listTarget[Random.Range(0, _listTarget.Count)];
        _target = Instantiate(_target, transform);
        _target.StartActionTarget();

        _coroutineVictory = StartCoroutine(CheckVictory());
    }

    private void OnCollisionEnter(Collision other)
    {
        if (other.collider.CompareTag("Bullet"))
        {
            GameManager.instance.RepeatShot();
            StopCoroutine(_coroutineVictory);
            StartCoroutine(ShootShowing(other.gameObject));
        }
    }

    private IEnumerator ShootShowing(GameObject gameObject)
    {
        yield return new WaitForSeconds(1);
        _target.RepeatTarget();
        Destroy(gameObject);
    }

    private IEnumerator CheckVictory()
    {
        yield return new WaitUntil(()=> _target.IsDestroy);
        yield return new WaitForSeconds(2);
        GameManager.instance.Victory();
        _target.RepeatTarget();
    }
    
}