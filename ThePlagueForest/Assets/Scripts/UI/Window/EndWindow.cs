using DG.Tweening;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class EndWindow : MonoBehaviour
{
    private static bool mIsOpen=false;
    public static EndWindow Open()
    {
        Canvas canvas = GameObject.Find("Main Camera/WindowCanvas").GetComponent<Canvas>();
        GameObject prefab=Resources.Load<GameObject>("UI/EndWindow");
        GameObject gameObject=GameObject.Instantiate(prefab,canvas.transform);
        EndWindow endWindow=gameObject.AddComponent<EndWindow>();
        endWindow.Init();
        return endWindow;
    }

    private void Init()
    {
        mIsOpen=true;
        TMP_Text text=transform.Find("Window/Border/LiveTimeText").GetComponent<TextMeshProUGUI>();
        text.text="生存时间:"+FightManager.GetCurrent().GetLiveTime().GetTimeText();
        Button button=GameObject.Find("Window/Button").GetComponent<Button>();
        button.onClick.AddListener(()=>
        {
            mIsOpen=false;
            SceneManager.sceneUnloaded += OnSceneUnloaded;
            SceneManager.LoadScene("Main");
            Destroy(gameObject);
        });
    }
    public void OnSceneUnloaded(Scene scene)
    {
        DOTween.KillAll();
    }
    public static bool IsOpen()
    {
        return mIsOpen;
    } 
    public static void SetOpen(bool open)
    {
        mIsOpen=open;
    }

    private void OnDestroy()
    {
        mIsOpen = false;
    }
}