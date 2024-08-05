using System.Buffers.Text;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//火焰魔杖
public class FireWand:Weapon
{
    public FireWand():base(EquipmentType.Active,EquipmentId.FireWand)
    {
        mStatusEffectId=StatusEffectId.Equipment_FireWand;
    }
    public override void OnGet(StatusEffect statusEffect,int layer)
    {
        mAttack=100;
        mShootTime=1.8f;
         BulletShooter shooter = new BulletShooter(()=>
         {
            Enemy nearEnemy=FightUtility.GetNearEnemy(Player.GetCurrent());
            if(nearEnemy==null)
            {
                mIsShoot=false;
                return;
            }
            mIsShoot=true;
            //创建子弹
            BulletFireWand bulletFireWand = BulletFireWand.Create(Player.GetCurrent(),mAttack);
            bulletFireWand.transform.position=Player.GetCurrent().transform.position;

            //方向朝着最近的敌人
            Vector3 direction=(nearEnemy.transform.position-bulletFireWand.transform.position).normalized;
            bulletFireWand.transform.rotation=FightUtility.DirectionToRotation(direction);
            FightModel.GetCurrent().AddPlayerBullet(bulletFireWand);
        },mShootTime);
        mBulletShooter=shooter;
    }

}