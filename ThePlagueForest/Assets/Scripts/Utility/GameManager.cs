using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using DG.Tweening;

public class GameManager : Singleton<GameManager>
{
    //构造
    public void OnSingletonInit()
    {
        DOTween.Init();
        CharacterUtility.Init();
    }
}
