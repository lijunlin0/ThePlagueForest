using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class InitScene : MonoBehaviour
{
   public void Awake()
   {
      Application.targetFrameRate=60;
      GameManager.GetInstance();
   }
   public void Start()
   {
      InitScreen();
      
      SceneManager.LoadScene("Main");
   }
   public void InitScreen()
   {
      if(Screen.width<Screen.height)
      {
         Utility.WindowWidth=Screen.width;
         Utility.WindowHeight=Screen.height;
         Utility.IsPC=false;
      }
      else
      {
         Utility.WindowWidth=Screen.width;
         Utility.WindowHeight=Screen.height;
         Utility.IsPC=true;
      }
   }
}
