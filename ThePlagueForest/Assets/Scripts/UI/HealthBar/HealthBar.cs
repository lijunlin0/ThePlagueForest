using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;
using TMPro;

public class HealthBar : MonoBehaviour
{
    private const int YOffset=60;
    private Character mCharacter;
    private Slider mHealthSlider;
    private RectTransform mHealthRectTransform;
    private TMP_Text mHealthPointsText;
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
        mHealthRectTransform=transform.GetComponent<RectTransform>();
    }

    public void OnHealthChanged()
    {
        float maxHealth=mCharacter.GetCurrentPropertySheet().GetMaxHealth();
        float currentHealth=mCharacter.GetHealth();
        float targetValue=currentHealth/maxHealth;
        Debug.Log(mCharacter.name+":最大生命值"+maxHealth+"当前生命值:"+currentHealth+",Value:"+targetValue);
        mHealthSlider.value=targetValue;
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
