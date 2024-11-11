using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;
using TMPro;

public class ExpBar : MonoBehaviour
{
    protected float mLevelUpExp;
    protected float mCurrentExp;
    private Slider mExpSlider;
    public static ExpBar Create()
    {
        Canvas canvas=GameObject.Find("UICanvas").GetComponent<Canvas>();
        GameObject ExpBarPrefab=Resources.Load<GameObject>("UI/ExpBar");
        GameObject gameObject=Instantiate(ExpBarPrefab,canvas.transform);
        gameObject.GetComponent<CanvasGroup>().alpha=0;
        RectTransform rectTransform=gameObject.GetComponent<RectTransform>();
        rectTransform.offsetMin=new Vector2(50,50);
        rectTransform.offsetMax=new Vector2(-50,-(Utility.WindowHeight-70));
        ExpBar bar=gameObject.AddComponent<ExpBar>();
        bar.Init();
        return bar;
        
    }
    
    private void Init()
    {
        mExpSlider=transform.Find("Display/Exp").GetComponent<Slider>();
        mExpSlider.value=0;
    }

    public void SetLevelUpExp(int exp)
    {
        mLevelUpExp=exp;
    }

    public void OnExpChanged(float targetExp)
    {
        float targetValue=targetExp/mLevelUpExp;
        mExpSlider.value=targetValue;
    }

    public void OnLevelUp(float LevelUPExp)
    {
        mExpSlider.value=0;
        mLevelUpExp=LevelUPExp;
    }
}
