using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;
using TMPro;

public class HealthBar : MonoBehaviour
{
    private const int YOffset=60;
    private Character mCharacter;
    private Slider mHealthSlider;
    private Slider mDamageSlider;
    private TMP_Text mHealthPointsText;
    private Tween mTween;
    public static HealthBar Create(Character character)
    {
        GameObject prefab=Resources.Load<GameObject>("UI/HealthBar");
        GameObject healthObject=GameObject.Instantiate(prefab,GameObject.Find("Canvas").transform);
        HealthBar healthBar=healthObject.AddComponent<HealthBar>();
        healthBar.Init(character);
        return healthBar;
    }
    
    private void Init(Character character)
    {
        mCharacter=character;
        mHealthSlider=transform.Find("Health").GetComponent<Slider>();
        mDamageSlider=transform.Find("Damage").GetComponent<Slider>();
    }

    public void OnHealthChanged()
    {
        float maxHealth=mCharacter.GetCurrentPropertySheet().GetMaxHealth();
        float currentHealth=mCharacter.GetHealth();
        float targetValue=currentHealth/maxHealth;
        //Debug.Log(mCharacter.name+":最大生命值"+maxHealth+"当前生命值:"+currentHealth+",Value:"+targetValue);
        mHealthSlider.value=targetValue;
        if(mDamageSlider.value<targetValue)
        {
            mDamageSlider.value=targetValue;
        }
        else
        {
            if(mTween!=null)
            {
                mTween.Kill();
            }
            mTween=mDamageSlider.DOValue(targetValue,0.2f).SetEase(Ease.InQuint).OnComplete(()=>
            {
                mTween=null;
            });
        }
    }

    public void Update()
    {
        if(mCharacter.IsDead())
        {
            mHealthSlider.value=0;
            Destroy(this);
        }
        transform.position = mCharacter.transform.position + new Vector3(0, YOffset, 0);

    }

    public void UpdateContent()
    {
        OnHealthChanged();
    }
}
