using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class GameManager : Singleton<GameManager>
{
    //构造
    public void OnSingletonInit()
    {
        CharacterUtility.Init();
    }
}
