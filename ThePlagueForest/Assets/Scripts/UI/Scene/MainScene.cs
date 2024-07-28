using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class MainScene : MonoBehaviour
{
  public void Awake()
  {
    //获取Canvas
     GameObject CanvasObj=GameObject.Find("Canvas");
     //加载UI预制件
     GameObject buttonPrefab=Resources.Load<GameObject>("UI/Button");
     //实例化button
     GameObject buttonInstance=Instantiate(buttonPrefab,CanvasObj.transform);
     Button startButton= buttonInstance.GetComponent<Button>();
     startButton.onClick.AddListener(StartButtonOnClick);
     TMP_Text startText=startButton.GetComponentInChildren<TMP_Text>();
  }
  public void StartButtonOnClick()
  {
    SceneManager.LoadScene("Fight");
  }
}
