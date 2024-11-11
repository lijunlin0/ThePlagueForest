using System;
using DG.Tweening;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class FightScene : MonoBehaviour
{
     FightManager mFightManager;
     public TextMeshProUGUI timerText;

     public void Awake()
     {
          InitScene();
          mFightManager=new FightManager();
          Map.Create();
     }
     public void Update()
     {
          mFightManager.OnUpdate();
     }

     public void InitScene()
     {
          Camera camera=Camera.main;
          camera.orthographicSize=Utility.WindowStartHeight/2;

          // 获取场景中的所有 Canvas
          Canvas[] canvases = FindObjectsOfType<Canvas>();
          // 遍历所有 Canvas
          foreach (Canvas canvas in canvases)
          {
               RectTransform rectTransform = canvas.GetComponent<RectTransform>();
               rectTransform.sizeDelta = new Vector2(Utility.WindowWidth,Utility.WindowHeight);
          }
     }
}
