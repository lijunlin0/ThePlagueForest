using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class FightScene : MonoBehaviour
{
     FightManager mFightManager;
     public void Awake()
     {
          mFightManager=new FightManager();
          Map.Create();
          SettingsButton.Create();
     }
     public void Update()
     {
          mFightManager.OnUpdate();
     }
}
