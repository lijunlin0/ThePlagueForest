using System.Buffers.Text;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Dagger:Weapon
{
    public override void Init()
    {
         BulletShooter shooter = new BulletShooter();
        shooter.SetShootCallback(()=>{
            //创建子弹
            BulletDagger bulletDagger = BulletDagger.Create();
            bulletDagger.transform.position=FightModel.GetCurrent().GetPlayer().transform.position;
            //方向朝着最近的敌人
            Enemy nearEnemy=FightUtility.GetNearEnemy();
            if(nearEnemy==null)
            {
                return;
            }
            Vector3 direction=(nearEnemy.transform.position-bulletDagger.transform.position).normalized;
            bulletDagger.transform.rotation=FightUtility.DirectionToRotation(direction);
            FightModel.GetCurrent().AddPlayerBullet(bulletDagger);
        },mShootTime);
        mBulletShooter=shooter;
    }

    public override void OnUpdate()
    {
        mBulletShooter.OnUpdate();
    }

}