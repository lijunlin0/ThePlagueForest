
using System;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class LiveTime : MonoBehaviour
{
    private float mElapsedTime;
    private TextMeshProUGUI mText;
    private bool mIsPaused=false;

    public  static LiveTime Create()
    {
        Canvas canvas=GameObject.Find("UICanvas").GetComponent<Canvas>();
        GameObject prefab=Resources.Load<GameObject>("UI/LiveTime");
        GameObject gameObject=Instantiate(prefab,canvas.transform);
        gameObject.GetComponent<CanvasGroup>().alpha=0;
        LiveTime LiveTimeText=gameObject.AddComponent<LiveTime>();
        LiveTimeText.Init();
        return LiveTimeText;
    }
    private void Init()
    {
        mText=GetComponent<TextMeshProUGUI>();
    }

    public string GetTimeText()
    {
        int minutes = Mathf.FloorToInt(mElapsedTime/60);
        int seconds = Mathf.FloorToInt(mElapsedTime%60);
        string res = string.Format("{0:D2}分{1:D2}秒", minutes, seconds);
        return res;
    }

    private void Update()
    {
        if(EndWindow.IsOpen()||EquipmentSelectWindow.IsOpen()||SettingsWindow.IsOpen())
        {
            return;
        }
        mElapsedTime+=Time.deltaTime;
        int minutes = Mathf.FloorToInt(mElapsedTime/60);
        int seconds = Mathf.FloorToInt(mElapsedTime%60);
        // 格式化时间字符串
        mText.text = string.Format("{0:00}:{1:00}", minutes, seconds);
    }
}