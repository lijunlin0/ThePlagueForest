using System.Buffers.Text;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//Enemy1直线普通子弹
public class Enemy2Bullet : Bullet
{
    public static Enemy2Bullet Create(Enemy enemy)
    {
        GameObject bulletPrefab=Resources.Load<GameObject>("FightObject/Bullet/DefaultBullet");
        GameObject bulletObject=GameObject.Instantiate(bulletPrefab);
        Enemy2Bullet bullet=bulletObject.AddComponent<Enemy2Bullet>();
        bullet.Init();
        return bullet;
    }
    public override void OnUpdate()
    {
        if(Time.time-mLiveTime>mMaxLifeTime)
        {
            mIsDead=true;
            return;
        }
        FightUtility.Move(gameObject,mBulletMoveSpeed);
    }

    protected override void Init()
    {
        base.Init();
        mBulletMoveSpeed=400;
        mLiveTime = Time.time;
    }
}