using System;
using DG.Tweening;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class SettingsWindow : MonoBehaviour
{
    private bool mMusicIsOn;
    public  static SettingsWindow Open()
    {
        Canvas canvas=GameObject.Find("WindowCanvas").GetComponent<Canvas>();
        GameObject prefab=Resources.Load<GameObject>("UI/SettingsWindow");
        GameObject gameObject=GameObject.Instantiate(prefab,canvas.transform);
        SettingsWindow window=gameObject.AddComponent<SettingsWindow>();
        window.Init();
        return window;
    }
    private void Init()
    {
        mMusicIsOn=true;
        //按钮点击事件
        Button mainMenuButton=transform.Find("Window/MainMenuButton").GetComponent<Button>();
        mainMenuButton.onClick.AddListener(()=>
        {
            SceneManager.LoadScene("Main");
        });
        Button continueButton=transform.Find("Window/ContinueButton").GetComponent<Button>();
        continueButton.onClick.AddListener(()=>
        {
            GameObject.Destroy(gameObject);
            Time.timeScale=1;
            //FightModel.sequence.Pause();
        });
        Button MusicButton=transform.Find("Window/MusicButton").GetComponent<Button>();
        Image onImgae=transform.Find("Window/MusicButton/MusicOnImage").GetComponent<Image>();
        Image offImgae=transform.Find("Window/MusicButton/MusicOffImage").GetComponent<Image>();
        MusicButton.onClick.AddListener(()=>
        {
            onImgae.transform.gameObject.SetActive(!mMusicIsOn);
            offImgae.transform.gameObject.SetActive(mMusicIsOn);
            mMusicIsOn=!mMusicIsOn;
            AudioListener.volume=mMusicIsOn ? 1 : 0;
            Debug.Log(AudioListener.volume);
        });
    }

}