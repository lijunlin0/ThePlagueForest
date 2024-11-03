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
      Utility.InitScreen();
      
      SceneManager.LoadScene("Main");
   }

}
