using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using UnityEngine;

public class FightObject : MonoBehaviour
{    
    protected MyCollider mCollider;
    protected GameObject mDisplay;
    protected bool mIsPoolObject;
    protected bool mIsDead;

    protected virtual void Init()
    {
        mIsPoolObject=false;
        mDisplay=transform.Find("Display").gameObject;
        mCollider=new MyCollider(GetComponent<PolygonCollider2D>());
        mIsDead=false;
    }

    public virtual void OnUpdate()
    {
        mCollider.OnUpdate();
    }

    public virtual void PlayDestroyAnimation()
    {
        gameObject.SetActive(false);
        DOVirtual.DelayedCall(3,()=>
        {
            if (mIsPoolObject)
            {
                DOTween.Kill(gameObject);
                FightManager.GetCurrent().GetPoolManager().PutGameObject(gameObject);
                Destroy(this);
            }
            else
            {
                DOTween.Kill(gameObject);
                Destroy(gameObject);
            }
        });
    }
    public MyCollider GetCollider(){return mCollider;}
    public bool IsDead() {return mIsDead;}
}
