using System.Buffers.Text;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//匕首
public class BulletDagger: Bullet
{ 
    public static BulletDagger Create()
    {
        GameObject bulletPrefab=Resources.Load<GameObject>("FightObject/Bullet/DefaultBullet");
        GameObject bulletObject=GameObject.Instantiate(bulletPrefab);
        BulletDagger bullet=bulletObject.AddComponent<BulletDagger>();
        bullet.Init();
        return bullet;
    }
   public override void OnUpdate()
    {
        base.OnUpdate();
        if(mIsDead)
        {
            return;
        }
        FightUtility.Move(gameObject,mBulletMoveSpeed);
    }
}
