using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Zone : MonoBehaviour
{
    private Collider _collider;

    void Start()
    {
        _collider = GetComponent<Collider>();
    }

    public int GetCountBullet()
    {
        int tempCountBullet = 0;
        Collider[] overlapSphere = Physics.OverlapSphere(transform.position, (_collider as SphereCollider).radius);
        
        for (var i = 0; i < overlapSphere.Length; i++)
        {
            if (overlapSphere[i].GetComponent<Bullet>() != null)
                tempCountBullet++;
        }

        return tempCountBullet;
    }

    public void CalculateZone(bool isAction)
    {
        _collider.enabled = isAction;
    }
}
