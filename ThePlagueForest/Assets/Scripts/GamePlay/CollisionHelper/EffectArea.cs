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
    private string mPrefabName;
    private Vector2[] mPoints;
    private Vector2[] points;
    private Vector2[] vertex;
    private Tween mAddTween;
    private Tween mAddEndTween;
    private bool mIsAddOver=false;
    private bool mIsAddEndOver=false;
    private bool mIsPoolObject=false;
    private float mBaseScaleX=1;
    private float mBaseScaleY=1;

    
    public static EffectArea Create(string prefabName,Callback<Character> collideCallback,bool isCarry=true)
    {
        GameObject effectAreaPrefab=Resources.Load<GameObject>("FightObject/Area/"+prefabName);
        GameObject effectAreaObject=Instantiate(effectAreaPrefab,Player.GetCurrent().transform);
        EffectArea effectArea=effectAreaObject.AddComponent<EffectArea>();
        effectArea.Init(collideCallback);
        return effectArea; 
    }
    public static EffectArea CircleWithPositonCreate(string prefabName,Vector3 position,Callback<Character> collideCallback,float scaleFactor=1)
    {
        GameObject gameObject=FightManager.GetCurrent().GetPoolManager().GetGameObject("Area/"+prefabName);
        gameObject.SetActive(true);
        gameObject.transform.position=position;
        EffectArea effectArea=gameObject.AddComponent<EffectArea>();
        effectArea.CircleWithPositonInit(collideCallback,prefabName,scaleFactor);
        return effectArea; 
    }


    private void CircleWithPositonInit(Callback<Character> collideCallback,string prefabName,float scaleFactor=1)
    {
        mIsPoolObject=true;
        mBaseScaleX=transform.localScale.x;
        mBaseScaleY=transform.localScale.y;
        if(scaleFactor!=1)
        {
            transform.localScale=new Vector3(mBaseScaleX*scaleFactor,mBaseScaleY*scaleFactor,1);
        }
        if(mAnimator==null)
        {
            mAnimator=GetComponent<Animator>();
            //mAnimator.Rebind();  // 重置Animator
            mAnimator.Update(0);
            //mAnimator.speed=1;
            //mAnimator.Play(prefabName,0,0);
        }
        if(mCollider==null)
        {
            mCollider=new MyCollider(GetComponent<PolygonCollider2D>());
            mCollider.GetCollider().enabled=true;
        }
        mCollideCallback=collideCallback;
    }
    private void Init(Callback<Character> collideCallback)
    {
        mCollideCallback=collideCallback;
        mAnimator=GetComponent<Animator>();
        mCollider=new MyCollider(GetComponent<PolygonCollider2D>());
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

    public void PlayDestroyAnimation(float fadeOutDuration)
    {
        SpriteRenderer spriteRenderer=GetComponent<SpriteRenderer>();
        spriteRenderer.DOFade(0,fadeOutDuration).OnComplete(()=>
        {
            DOTween.Kill(transform);
            if(mIsPoolObject)
            {
                mCollider=null;
                mAnimator=null;
                transform.localScale=new Vector3(mBaseScaleX,mBaseScaleY,1);
                Color color=spriteRenderer.color;
                color.a=1;
                spriteRenderer.color = color;
                FightManager.GetCurrent().GetPoolManager().PutGameObject(gameObject);
                Destroy(this);
            }
            else
            {
                gameObject.SetActive(false);
                Destroy(gameObject);

            }
        });
        
    }

    public void OnAdd()
    {
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