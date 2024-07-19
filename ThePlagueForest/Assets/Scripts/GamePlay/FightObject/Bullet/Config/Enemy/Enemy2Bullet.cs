using System.Buffers.Text;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//Enemy2直线普通子弹
public class Enemy2Bullet : Bullet
{
    public static Enemy2Bullet Create(Enemy enemy,int points)
    {
        GameObject bulletPrefab=Resources.Load<GameObject>("FightObject/Bullet/BulletDagger");
        GameObject bulletObject=Instantiate(bulletPrefab);
        Enemy2Bullet bullet=bulletObject.AddComponent<Enemy2Bullet>();
        bullet.Init(enemy,points);
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
}