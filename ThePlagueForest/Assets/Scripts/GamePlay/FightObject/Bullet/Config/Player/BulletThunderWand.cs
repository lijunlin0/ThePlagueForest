using System.Buffers.Text;
using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using UnityEngine;

//闪电魔杖子弹
public class BulletThunderWand: Bullet
{ 
    public static BulletThunderWand Create(Character character,int points,bool isFinal)
    {
        GameObject bulletObject=FightManager.GetCurrent().GetPoolManager().GetGameObject("Bullet/BulletThunderWand");
        bulletObject.SetActive(true);
        BulletThunderWand bullet=bulletObject.AddComponent<BulletThunderWand>();
        bullet.Init(character,points,isFinal);
        return bullet;
    }

    protected void Init(Character source,int points,bool isFinal)
    {
        base.Init(source,points);
        if(isFinal)
        {
            SpriteRenderer spriteRenderer= mDisplay.GetComponent<SpriteRenderer>();
            spriteRenderer.color=new Color(1,0,1,1);
        }
        mIsPenetrate=true;
        mMoveSpeed=0;
        mMaxLifeTime=0.8;
    }
}
