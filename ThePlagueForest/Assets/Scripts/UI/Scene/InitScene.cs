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
         Utility.WindowWidth=Utility.PhoneWidth;
         Utility.WindowHeight=Utility.PhoneHeight;
         Utility.IsPC=false;
      }
      else
      {
         Utility.WindowWidth=Utility.PcWidth;
         Utility.WindowHeight=Utility.PcHeight;
         Utility.IsPC=true;
      }
   }
}
