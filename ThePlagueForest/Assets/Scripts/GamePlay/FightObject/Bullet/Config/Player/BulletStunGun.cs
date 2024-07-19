using System.Buffers.Text;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//电击枪子弹
public class BulletStunGun: Bullet
{ 
    public static BulletStunGun Create(Character character,int points)
    {
        GameObject bulletPrefab=Resources.Load<GameObject>("FightObject/Bullet/BulletStunGun");
        GameObject bulletObject=GameObject.Instantiate(bulletPrefab);
        BulletStunGun bullet=bulletObject.AddComponent<BulletStunGun>();
        bullet.Init(character,points);
        return bullet;
    }

    protected override void Init()
    {
        mMoveSpeed=0;
        mIsDead=true;
    }
}
