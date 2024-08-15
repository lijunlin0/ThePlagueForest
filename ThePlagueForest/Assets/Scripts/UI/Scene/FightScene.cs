using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.SceneManagement;

public class FightScene : MonoBehaviour
{
     FightManager mFightManager;
     public void Awake()
     {
          InitScene();
          mFightManager=new FightManager();
          Map.Create();
          SettingsButton.Create();
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
               InputManager inputManager=canvases[0].AddComponent<InputManager>();
          }
          // 遍历所有 Canvas
          foreach (Canvas canvas in canvases)
          {
               RectTransform rectTransform = canvas.GetComponent<RectTransform>();
               rectTransform.sizeDelta = new Vector2(Utility.WindowWidth,Utility.WindowHeight);
          }
          Joystick joystick=Joystick.Create();
          InputManager.mJoystick=joystick;
          InputManager.mJoystickCenter=joystick.transform.Find("Center").gameObject;
          InputManager.mJoystickBasePosition=joystick.transform.position;
     }
}
