using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;
using TMPro;

public class HealthBar : MonoBehaviour
{
    private const int YOffset=50;
    private Character mCharacter;
    private Slider mHealthSlider;
    private Slider mDamageSlider;
    private Tween mTween;
    public static HealthBar Create(Character character)
    {
        GameObject prefab=Resources.Load<GameObject>("UI/HealthBar");
        if(character.GetCharacterId()==CharacterId.Boss)
        {   
            Canvas canvas=GameObject.Find("WindowCanvas").GetComponent<Canvas>();
            GameObject gameObject=GameObject.Instantiate(prefab,new Vector3(0,480,-1),Quaternion.identity,canvas.transform);
            GameObject bossDisplay=gameObject.transform.Find("Display").gameObject;
            bossDisplay.transform.localScale = new Vector3(10,6,1);
            TextMeshProUGUI  text=gameObject.transform.Find("Name").GetComponent<TextMeshProUGUI >();
            text.text="火蠕虫";
            HealthBar bar=gameObject.AddComponent<HealthBar>();
            bar.Init(character);
            return bar;
        }

        GameObject healthObject=GameObject.Instantiate(prefab,character.transform.position,Quaternion.identity,GameObject.Find("Canvas").transform);
        GameObject display=healthObject.transform.Find("Display").gameObject;

        //玩家
        if(character.GetCharacterId()==CharacterId.Player)
        {   
            Image image=healthObject.transform.Find("Display/Health/HealthFill").GetComponent<Image>();
            image.color=new Color(0,0.5f,0,1);

        }
        else if(character.GetCharacterId()==CharacterId.Enemy1)
        {   
            display.transform.localScale=new Vector3(0.8f,0.4f,1);
        }
        else
        {
            display.transform.localScale=new Vector3(1.2f,0.4f,1);
        }
        HealthBar healthBar=healthObject.AddComponent<HealthBar>();
        
        healthBar.Init(character);
        return healthBar;
    }
    
    private void Init(Character character)
    {
        mCharacter=character;
        mHealthSlider=transform.Find("Display/Health").GetComponent<Slider>();
        mDamageSlider=transform.Find("Display/Damage").GetComponent<Slider>();
    }

    public void OnHealthChanged()
    {
        float maxHealth=mCharacter.GetCurrentPropertySheet().GetMaxHealth();
        float currentHealth=mCharacter.GetHealth();
        float targetValue=currentHealth/maxHealth;
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
        if(mCharacter.GetCharacterId()!=CharacterId.Boss)
        {
            transform.position = mCharacter.transform.position + new Vector3(0, YOffset, 0);
        }

    }

    public void UpdateContent()
    {
        OnHealthChanged();
    }
}
