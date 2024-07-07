using System.Collections;
using System.Collections.Generic;
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
        Destroy(gameObject);
    }
    public MyCollider GetCollider(){return mCollider;}
    public bool IsDead() {return mIsDead;}
}
