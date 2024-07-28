using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using DG.Tweening;

public class EffectArea:MonoBehaviour
{
    private MyCollider mCollider;
    private Callback<Character> mCollideCallback;
    private Callback mCollisionEnabledCallback;
    private GameObject mDisplay;

    
    public static EffectArea Create(string prefabName,Callback<Character> collideCallback)
    {
        GameObject effectAreaPrefab=Resources.Load<GameObject>("FightObject/Area/"+prefabName);
        GameObject effectAreaArea=Instantiate(effectAreaPrefab,Player.GetCurrent().transform);
        EffectArea effectArea=effectAreaArea.AddComponent<EffectArea>();
        effectArea.Init(collideCallback);
        return effectArea; 
    }
    public static EffectArea CircleWithPositonCreate(string prefabName,Vector3 position,Callback<Character> collideCallback)
    {
        GameObject effectAreaPrefab=Resources.Load<GameObject>("FightObject/Area/"+prefabName);
        GameObject effectAreaArea=Instantiate(effectAreaPrefab,position,Quaternion.identity);
        EffectArea effectArea=effectAreaArea.AddComponent<EffectArea>();
        effectArea.Init(collideCallback);
        return effectArea; 
    }
    private void Init(Callback<Character> collideCallback)
    {
        mDisplay=transform.Find("Display").gameObject;
        mCollideCallback=collideCallback;
        mCollider=new MyCollider(GetComponent<PolygonCollider2D>());
    }

    public void PlayDestroyAnimation(float fadeOutDuration)
    {
        SpriteRenderer spriteRenderer=mDisplay.GetComponent<SpriteRenderer>();
        spriteRenderer.DOFade(0f,fadeOutDuration).OnComplete(()=>
        {
            Destroy(gameObject);
        });
    }

    public void Update()
    {
        mCollider.OnUpdate();
        if(mCollider.IsEnable()&&mCollisionEnabledCallback!=null)
        {
           mCollisionEnabledCallback();
           mCollisionEnabledCallback=null;
        }
    }
    public void SetCollisionEnabledCallback(Callback callback)
    {
        mCollisionEnabledCallback=callback;
    }

    public void Collide()
    {
        List<Enemy> enemies=FightModel.GetCurrent().GetEnemies();
        foreach(Enemy enemy in enemies)
        {
            MyCollider collider2=enemy.GetCollider();
            if(CollisionHelper.IsColliding(mCollider,collider2))
            {
                mCollideCallback(enemy);
            }
        }
    }
}