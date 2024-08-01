using System;
using DG.Tweening;
using TMPro;
using UnityEditor.Experimental.GraphView;
using UnityEngine;
using UnityEngine.UI;

public class SettingsButton : MonoBehaviour
{
    public  static SettingsButton Create()
    {
        Canvas canvas=GameObject.Find("UICanvas").GetComponent<Canvas>();
        GameObject prefab=Resources.Load<GameObject>("UI/SettingsButton");
        GameObject gameObject=GameObject.Instantiate(prefab,canvas.transform);
        SettingsButton button=gameObject.AddComponent<SettingsButton>();
        button.Init();
        return button;
    }
    private void Init()
    {
        //按钮点击事件
        Button button=GetComponent<Button>();
        button.onClick.AddListener(()=>
        {
            if(EndWindow.IsOpen())
            {
                return;
            }
            SettingsWindow.Open();
        });
    }
}