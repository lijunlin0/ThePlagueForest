using System;
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
          SettingsButton.Create();
          LiveTime.Create();
     }
     public void Update()
     {
          mFightManager.OnUpdate();
     }

     public void InitScene()
     {
          Camera camera=Camera.main;
          camera.orthographicSize=Utility.WindowHeight/2;
          // 获取场景中的所有 Canvas
          Canvas[] canvases = FindObjectsOfType<Canvas>();
          if(!Utility.IsPC)
          {
               canvases[0].AddComponent<InputManager>();
          }
          // 遍历所有 Canvas
          foreach (Canvas canvas in canvases)
          {
               RectTransform rectTransform = canvas.GetComponent<RectTransform>();
               rectTransform.sizeDelta = new Vector2(Utility.WindowWidth,Utility.WindowHeight);
          }
          //生成摇杆
          if(!Utility.IsPC)
          {
               Joystick joystick=Joystick.Create();
               InputManager.mJoystick=joystick;
               InputManager.mJoystickCenter=joystick.transform.Find("Center").gameObject;
               InputManager.mJoystickBasePosition=joystick.transform.position;
          }
     }
}
