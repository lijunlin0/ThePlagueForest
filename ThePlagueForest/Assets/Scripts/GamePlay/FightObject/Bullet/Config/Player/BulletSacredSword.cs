using System.Buffers.Text;
using System.Collections;
using System.Collections.Generic;
using Unity.Burst.Intrinsics;
using UnityEngine;

//圣剑子弹
public class BulletSacredSword:Bullet
{ 
    public static BulletSacredSword Create(Character character,int points,float scale)
    {
        GameObject bulletPrefab=Resources.Load<GameObject>("FightObject/Bullet/BulletSacredSword");
        GameObject bulletObject=GameObject.Instantiate(bulletPrefab);
        BulletSacredSword bullet=bulletObject.AddComponent<BulletSacredSword>();
        bullet.mIsPenetrate=true;
        bullet.Init(character,points,scale);
        return bullet;
    }
   public override void OnUpdate()
    {
        base.OnUpdate();
        if(mIsDead)
        {
            return;
        }
        FightUtility.Move(gameObject,mMoveSpeed);
    }
    protected void Init(Character source,int points,float scale)
    {
        base.Init(source,points);
        mMoveSpeed=500;
        if(scale==1)
        {
            return;
        }
        Vector2[] collidePoints=mCollider.GetCollider().GetPath(0);
        for(int i=0;i<collidePoints.Length;i++)
        {
            collidePoints[i]*=scale;
        }
        mCollider.GetCollider().SetPath(0,collidePoints);
        float xScale=mDisplay.transform.localScale.x;
        float yScale=mDisplay.transform.localScale.y;
        mDisplay.transform.localScale = new Vector3(xScale*scale,xScale*scale,1);
    }
}
