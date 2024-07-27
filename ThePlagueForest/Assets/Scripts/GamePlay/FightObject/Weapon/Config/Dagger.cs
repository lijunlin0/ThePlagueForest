using System.Buffers.Text;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//匕首
public class Dagger:Weapon
{
    public Dagger():base(EquipmentType.Active,EquipmentId.Dagger)
    {
        
    }
    public override void OnGet(StatusEffect statusEffect,int layer)
    {
        mAttack=20;
         BulletShooter shooter = new BulletShooter(()=>
         {
            //方向朝着最近的敌人
            Enemy nearEnemy=FightUtility.GetNearEnemy(Player.GetCurrent());
            if(nearEnemy==null)
            {
                mIsShoot=false;
                return;
            }
            mIsShoot=true;
            //创建子弹
            BulletDagger bulletDagger = BulletDagger.Create(Player.GetCurrent(),mAttack);
            bulletDagger.transform.position=Player.GetCurrent().transform.position;
           
            Vector3 direction=(nearEnemy.transform.position-bulletDagger.transform.position).normalized;
            bulletDagger.transform.rotation=FightUtility.DirectionToRotation(direction);
            FightModel.GetCurrent().AddPlayerBullet(bulletDagger);
        },mShootTime);
        mBulletShooter=shooter;
    }
}