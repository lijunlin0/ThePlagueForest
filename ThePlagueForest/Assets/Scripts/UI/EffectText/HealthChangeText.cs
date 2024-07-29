using DG.Tweening;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;

public class HealthChangeText : MonoBehaviour
{
    private TMP_Text mText;
    private Character mTarget;
    public static HealthChangeText Create(int points,Character target)
    {
        Canvas canvas=GameObject.Find("TextCanvas").GetComponent<Canvas>();
        GameObject prefab=Resources.Load<GameObject>("FightObject/Text/HealthChangeText");
        GameObject TextObject=Instantiate(prefab,canvas.transform);
        HealthChangeText healthChangeText=TextObject.AddComponent<HealthChangeText>();
        healthChangeText.Init(points,target);
        return healthChangeText;
    }

    public void Init(int points,Character target)
    {
        mTarget=target;
        
        mText=GetComponent<TMP_Text>();
        
        //恢复血量
        if(points>0)
        {
            mText.text="+"+points.ToString();
            mText.color=Color.green;
        }
        //扣除血量
        else
        {
            if(mTarget.IsPlayer())
            {
                mText.color=Color.red;
                mText.text="-"+(points*-1).ToString();
            }   
            else
            {
                mText.color=Color.white;
                mText.text=(points*-1).ToString();
            }
           
        }
    }

    public void Start()
    {
        Vector3 startPosition=mTarget.transform.position;
        if(mTarget.IsEnemy())
        {
            startPosition+=new Vector3(RandomHelper.RandomInt(-40,40),40+RandomHelper.RandomInt(-20,20),0);
        }
        Vector3 targetPosition=startPosition+new Vector3(0,80+RandomHelper.RandomInt(-20,20),0);
        transform.position=startPosition;
        transform.DOMove(targetPosition,1f).SetEase(Ease.OutQuart);
        DOVirtual.DelayedCall(0.5f,()=>
        {
            mText.DOFade(0,0.2f).OnComplete(()=>
            {
                 Destroy(gameObject,1);
            });
            
        });
    }
}