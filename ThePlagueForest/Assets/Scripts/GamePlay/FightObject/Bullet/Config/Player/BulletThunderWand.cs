using System.Buffers.Text;
using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using UnityEngine;

//闪电魔杖子弹
public class BulletThunderWand: Bullet
{ 
    public static BulletThunderWand Create(Character character,int points)
    {
        GameObject bulletObject=FightManager.GetCurrent().GetPoolManager().GetGameObject("Bullet/BulletThunderWand");
        bulletObject.SetActive(true);
        BulletThunderWand bullet=bulletObject.AddComponent<BulletThunderWand>();
        bullet.Init(character,points);
        return bullet;
    }

    protected override void Init(Character source,int points)
    {
        base.Init(source,points);
        mIsPenetrate=true;
        mMoveSpeed=0;
        mMaxLifeTime=0.8;
    }
}
