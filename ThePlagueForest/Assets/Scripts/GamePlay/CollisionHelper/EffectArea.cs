using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
using System.Reflection;
using System.Linq;
using System;

public class EffectArea:MonoBehaviour
{
    private const float ColliderPointsScale=3.5f;
    private Animator mAnimator;
    private MyCollider mCollider;
    private Callback<Character> mCollideCallback;
    private Callback mCollisionEnabledCallback;
    private bool mIsCarry=false;
    private Vector2[] mPoints;
    private Vector2[] points;
    private Vector2[] vertex;
    private Tween mAddTween;
    private Tween mAddEndTween;
    private float mAnimationDuration=0;
    private bool mIsAddOver=false;
    private bool mIsAddEndOver=false;

    
    public static EffectArea Create(string prefabName,Callback<Character> collideCallback,bool isCarry=true)
    {
        GameObject effectAreaPrefab=Resources.Load<GameObject>("FightObject/Area/"+prefabName);
        GameObject effectAreaArea=Instantiate(effectAreaPrefab,Player.GetCurrent().transform);
        EffectArea effectArea=effectAreaArea.AddComponent<EffectArea>();
        effectArea.Init(collideCallback,isCarry);
        return effectArea; 
    }
    public static EffectArea CircleWithPositonCreate(string prefabName,Vector3 position,Callback<Character> collideCallback,float scaleFactor=1)
    {
        GameObject effectAreaPrefab=Resources.Load<GameObject>("FightObject/Area/"+prefabName);
        GameObject effectAreaArea=Instantiate(effectAreaPrefab,position,Quaternion.identity);
        if(scaleFactor!=1)
        {
            float xScale=effectAreaArea.transform.localScale.x;
            float yScale=effectAreaArea.transform.localScale.y;
            effectAreaArea.transform.localScale=new Vector3(xScale*scaleFactor,yScale*scaleFactor,1);

        }
        EffectArea effectArea=effectAreaArea.AddComponent<EffectArea>();
        effectArea.Init(collideCallback);
        return effectArea; 
    }


    private void Init(Callback<Character> collideCallback,bool isCarry=false)
    {
        mIsCarry=isCarry;
        mAnimator=new Animator();
        mCollideCallback=collideCallback;
        mCollider=new MyCollider(GetComponent<PolygonCollider2D>());
        if(mIsCarry)
        {
            mAnimator=GetComponent<Animator>();

            PolygonCollider2D collider=mCollider.GetCollider();
            mPoints=collider.GetPath(0);
            points=new Vector2[mPoints.Length];
            vertex=new Vector2[mPoints.Length];
            mAnimator.Play("BurnCircle");
            //添加事件
            AnimationEvent addEvent = new AnimationEvent();
            AnimationEvent addEndEvent = new AnimationEvent();
            addEvent.functionName = "OnAdd";
            addEndEvent.functionName = "OnAddEnd";
            addEvent.time = 0;
            addEndEvent.time = 1.5f;
            AnimationClip clip= mAnimator.runtimeAnimatorController.animationClips[0];
            clip.AddEvent(addEvent);
            clip.AddEvent(addEndEvent);
        }
    }

    public void OnAdd()
    {
        if(mCollider.GetCollider()==null)
        {
            Debug.Log("碰撞空");
        }
        mIsAddEndOver=false;
        if(mIsAddOver)
        {
            return;
        }
        mAddEndTween.Kill();
        mAddTween=DOVirtual.Float(1,ColliderPointsScale,1.5f,(float f)=>
        {
            for(int i=0;i<points.Length;i++)
            {
                points[i]=mPoints[i]*f;
            }
        mCollider.GetCollider().SetPath(0,points); 
        });
        mIsAddOver=true;
    }
    public void OnAddEnd()
    {
        mIsAddOver=false;
        if(mIsAddEndOver)
        {
            return;
        }
        Array.Copy(points,vertex,points.Length);
        mAddTween.Kill();
        mAddEndTween=DOVirtual.Float(1,ColliderPointsScale,1.5f,(float f)=>
        {
            for(int i=0;i<points.Length;i++)
            {
                points[i]=vertex[i]/f;
            }
            mCollider.GetCollider().SetPath(0,points);
        });
        mIsAddEndOver=true;
    }

    public void PlayDestroyAnimation(float fadeOutDuration)
    {
        SpriteRenderer spriteRenderer=GetComponent<SpriteRenderer>();
        spriteRenderer.DOFade(0,fadeOutDuration).OnComplete(()=>
        {
            gameObject.SetActive(false);
            DOTween.Kill(transform);
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