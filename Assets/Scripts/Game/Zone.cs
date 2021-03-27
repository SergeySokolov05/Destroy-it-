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

    public void CalculateZone(bool isAction)
    {
        _collider.enabled = isAction;
    }
}
