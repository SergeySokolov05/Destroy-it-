using System;
using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using UnityEngine;
using UnityEngine.UI;

public class UIManager : MonoBehaviour
{
   [SerializeField] private Button buttStartNewGame;
   [SerializeField] private Image imageIndicator;
   [SerializeField] private float speedIndicator;
   [SerializeField] private Text textCountBullet;
   [SerializeField] private Text textVictory;
   [SerializeField] private Text textDefeat;

   private bool _isStartCoroutine;
   private bool _isStartIndicator;
   private float _widthIndicator;
   private int _vectorMove;
   
   private void Start()
   {
      _widthIndicator = ((RectTransform) imageIndicator.transform.parent.transform).rect.width;
      buttStartNewGame.onClick.AddListener(StartGame);
   }

   private void Update()
   {
      if(!_isStartIndicator)
         return;

      if (Input.GetMouseButtonDown(0))
      {
         _vectorMove = 1;
         StartCoroutine(MoveIndicator());
      }
      else if(Input.GetMouseButtonUp(0) && _isStartCoroutine)
      {
         _isStartIndicator = false;
         _isStartCoroutine = false;
      }
   }

   public void OpenForgeGun(bool isAction)
   {
      Sequence sequence = DOTween.Sequence();
      sequence.Append(imageIndicator.transform.parent.transform.DOScale(isAction ? Vector3.one : Vector3.zero, 0.3f))
         .AppendCallback(() =>
         {
            _isStartIndicator = isAction;

            if(!isAction)
               imageIndicator.transform.localPosition = 
                  new Vector3(0, imageIndicator.transform.localPosition.y, imageIndicator.transform.localPosition.z);
            
         });
   }

   public void CalculateCountBullet(bool isAction, int currentBullet)
   {
      textCountBullet.transform.parent.transform.DOScale(isAction ? Vector3.one : Vector3.zero, 0.3f);
      textCountBullet.text = "Bullet: " + currentBullet;
      
      if(currentBullet == 0 && isAction)
         GameManager.instance.Defeat();
   }

   public void Victory()
   {
      buttStartNewGame.transform.DOScale(Vector3.one, 0.3f).SetEase(Ease.Flash);
      textVictory.transform.DOScale(Vector3.one, 0.3f);
   }

   public void Defeat(int countDestroy_Object)
   {
      textCountBullet.transform.parent.transform.DOScale(Vector3.zero, 0.3f);
      
      textDefeat.transform.DOScale(Vector3.one, 0.3f);
      textDefeat.text = "GameOver " + "CountDestroy: " + countDestroy_Object;
      
      buttStartNewGame.transform.DOScale(Vector3.one, 0.3f).SetEase(Ease.Flash);
   }

   private void StartGame()
   {
      Sequence sequence = DOTween.Sequence();
      sequence.Append(buttStartNewGame.transform.DOScale(Vector3.zero, 0.3f)).SetEase(Ease.Flash)
         .AppendCallback(() =>
         {
            GameManager.instance.StartGame();
            textVictory.transform.DOScale(Vector3.zero, 0.3f);
            textDefeat.transform.DOScale(Vector3.zero, 0.3f);
         });
   }
   
   private void CalculateForgeBullet()
   {
      var tempIsHitTarget = imageIndicator.transform.localPosition.x > -120 &&
                            imageIndicator.transform.localPosition.x < 120;
      
      GameManager.instance.Shot(tempIsHitTarget);
      StopAllCoroutines();
   }

   private IEnumerator MoveIndicator()
   {
      _isStartCoroutine = true;
      float tempLimitation = _widthIndicator / 2 * _vectorMove;
      
      while (imageIndicator.transform.localPosition.x < tempLimitation)
      {
         imageIndicator.transform.localPosition += Vector3.right * (_vectorMove * Time.deltaTime * speedIndicator);
         
         if (!_isStartIndicator)
         {
            CalculateForgeBullet();
            yield break;
         }

         yield return null;
      }

      _vectorMove *= -1;
      tempLimitation = _widthIndicator / 2 * _vectorMove;
      
      while (imageIndicator.transform.localPosition.x > tempLimitation)
      {
         imageIndicator.transform.localPosition += Vector3.right * (_vectorMove * Time.deltaTime * speedIndicator);
         
         if (!_isStartIndicator)
         {
            CalculateForgeBullet();
            yield break;
         }

         yield return null;
      }

      _vectorMove *= -1;

      StartCoroutine(MoveIndicator());
   }
}
