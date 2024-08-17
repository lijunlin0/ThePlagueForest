using System.Buffers.Text;
using System.Collections;
using System.Collections.Generic;
using Unity.Burst.Intrinsics;
using UnityEngine;

//圣剑子弹
public class BulletSacredSword:Bullet
{ 
    public static BulletSacredSword Create(Character character,int points)
    {
        GameObject bulletObject=FightManager.GetCurrent().GetPoolManager().GetGameObject("Bullet/BulletSacredSword");
        bulletObject.SetActive(true);
        BulletSacredSword bullet=bulletObject.AddComponent<BulletSacredSword>();
        bullet.Init(character,points);
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
    protected override void Init(Character source,int points)
    {
        base.Init(source,points);
        mIsPenetrate=true;
        mMoveSpeed=500;
        mMaxLifeTime=2.5f;
    }
}
