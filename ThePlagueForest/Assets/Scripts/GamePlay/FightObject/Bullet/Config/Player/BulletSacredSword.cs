using System.Buffers.Text;
using System.Collections;
using System.Collections.Generic;
using Unity.Burst.Intrinsics;
using UnityEngine;

//圣剑子弹
public class BulletSacredSword:Bullet
{ 
    public static BulletSacredSword Create(Character character,int points,bool isFinal,float finalScale)
    {
        GameObject bulletObject=FightManager.GetCurrent().GetPoolManager().GetGameObject("Bullet/BulletSacredSword");
        bulletObject.SetActive(true);
        BulletSacredSword bullet=bulletObject.AddComponent<BulletSacredSword>();
        bullet.Init(character,points,isFinal,finalScale);
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
    protected void Init(Character source,int points,bool isFinal,float finalScale)
    {
        base.Init(source,points);
        if(isFinal)
        {
            transform.localScale=new Vector3(finalScale,finalScale,1);
            SpriteRenderer spriteRenderer=mDisplay.GetComponent<SpriteRenderer>();
            spriteRenderer.color=new Color(0.15f,0.15f,0.15f,1);
        }
        mIsPenetrate=true;
        mMoveSpeed=500;
        mMaxLifeTime=2f;
    }
}
