using System.Buffers.Text;
using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using UnityEngine;

//Boss2子弹
public class Boss2Bullet : Bullet
{
    public static Boss2Bullet Create(Enemy enemy,int points,float rotation)
    {
        GameObject bulletPrefab=Resources.Load<GameObject>("FightObject/Bullet/BulletBoss2");
        GameObject bulletObject=Instantiate(bulletPrefab,enemy.transform.position,Quaternion.Euler(0,0,rotation));
        Boss2Bullet bullet=bulletObject.AddComponent<Boss2Bullet>();
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