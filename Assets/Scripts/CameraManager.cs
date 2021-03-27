using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using UnityEngine;

public class CameraManager : MonoBehaviour
{
    [SerializeField] private float timeMoveCamera = 5;
    [SerializeField] private Transform[] arrayPositionCamera;

    private int _indexCameraPosition;

    public void SetStartPosition()
    {
        _indexCameraPosition = 0;
        transform.DOMove(arrayPositionCamera[arrayPositionCamera.Length - 1].position, timeMoveCamera);
        transform.DORotate(arrayPositionCamera[arrayPositionCamera.Length - 1].rotation.eulerAngles, timeMoveCamera);
    }
    
    public void SetNextPosition()
    {
        transform.DOMove(arrayPositionCamera[_indexCameraPosition].position, timeMoveCamera);
        transform.DORotate(arrayPositionCamera[_indexCameraPosition].rotation.eulerAngles, timeMoveCamera);
        
        _indexCameraPosition++;

        if (_indexCameraPosition == arrayPositionCamera.Length)
            _indexCameraPosition = 0;
    }
}
