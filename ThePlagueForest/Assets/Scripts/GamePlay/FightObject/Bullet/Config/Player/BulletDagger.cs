using System.Buffers.Text;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//匕首子弹
public class BulletDagger: Bullet
{ 
    public static BulletDagger Create(Character character,int points,int penetrateCount)
    {
        GameObject bulletPrefab=Resources.Load<GameObject>("FightObject/Bullet/BulletDagger");
        GameObject bulletObject=GameObject.Instantiate(bulletPrefab);
        BulletDagger bullet=bulletObject.AddComponent<BulletDagger>();
        bullet.Init(character,points,penetrateCount);
        return bullet;
    }
    protected void Init(Character character,int points,int penetrateCount)
    {
        base.Init(character,points);
        mIsPenetrate=true;
        mPenetrateCount=penetrateCount;
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
