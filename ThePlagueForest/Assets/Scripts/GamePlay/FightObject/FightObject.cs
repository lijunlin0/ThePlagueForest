using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using UnityEngine;

public class FightObject : MonoBehaviour
{    
    protected MyCollider mCollider;
    protected bool mIsDead;

    protected virtual void Init()
    {
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
        DOVirtual.DelayedCall(5,()=>
        {
            Destroy(gameObject);
        });
    }
    public MyCollider GetCollider(){return mCollider;}
    public bool IsDead() {return mIsDead;}
}
