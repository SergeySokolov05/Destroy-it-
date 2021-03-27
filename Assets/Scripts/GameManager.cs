using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager instance;

    [SerializeField]  private UIManager uiManager;
    [SerializeField]  private BulletManager bulletManager;
    [SerializeField]  private CameraManager cameraManager;
    [SerializeField]  private TargetsManager targetsManager;
    [SerializeField]  private Gun gun;
    
    private int _countDestroy;
    
    public UIManager UiManager => uiManager;
    public BulletManager BulletManager => bulletManager;

    private void Awake()
    {
        instance = this;
        _countDestroy = 0;
    }

    public void StartGame()
    {
        bulletManager.StartGame();
        uiManager.CalculateCountBullet(true, bulletManager.CountBullet);
        cameraManager.SetStartPosition();
        gun.RepeatShot();
    }

    public void StarShot()
    {
        bulletManager.IsShotStart = true;
        cameraManager.SetNextPosition();
        uiManager.OpenForgeGun(true);
        uiManager.CalculateCountBullet(false, 0);
        targetsManager.ActionTarget();
    }

    public void Shot(bool isHitTarget)
    {
        gun.Shot(isHitTarget);
    }

    public void RepeatShot()
    {
        bulletManager.IsShotStart = false;
        cameraManager.SetNextPosition();
        uiManager.OpenForgeGun(false);
        uiManager.CalculateCountBullet(true, bulletManager.CountBullet);
        gun.RepeatShot();
    }

    public void Victory()
    {
        uiManager.OpenForgeGun(false);
        uiManager.Victory();
        _countDestroy++;
    }

    public void Defeat()
    {
        uiManager.Defeat(_countDestroy);
        _countDestroy = 0;
    }
}