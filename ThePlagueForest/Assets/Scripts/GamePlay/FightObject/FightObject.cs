using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FightObject : MonoBehaviour
{    
    protected PolygonCollider2D mCollider;
    protected bool mIsDead;

    protected virtual void Init()
    {
        mCollider=GetComponent<PolygonCollider2D>();
        // 检查是否成功获取到组件
        if (mCollider == null)
        {
            Debug.LogError("未能找到PolygonCollider2D组件！");
        }
        mIsDead=false;
    }

    public virtual void PlayDestroyAnimation()
    {
        Destroy(gameObject);
    }
    public PolygonCollider2D GetCollider(){return mCollider;}
    public bool IsDead() {return mIsDead;}
}
