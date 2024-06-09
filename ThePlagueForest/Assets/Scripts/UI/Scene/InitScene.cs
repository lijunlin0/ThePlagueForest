using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class InitScene : MonoBehaviour
{
   public static bool isFirst=true;
   public void Awake()
   {
      Application.targetFrameRate=60;
      GameManager.GetInstance();
   }
   public void Start()
   {
      SceneManager.LoadScene("Main");

   }
}
