using System.Buffers.Text;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//回旋镖
public class Boomerang:Weapon
{
    public Boomerang():base(EquipmentType.Active,EquipmentId.Boomerang)
    {
        mStatusEffectId=StatusEffectId.Equipment_Boomerang;
    }
    public override void OnGet(StatusEffect statusEffect,int layer)
    {
        mAttack=20;
        mAttackRange=520;
        mShootTime=1.2f;
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
            BulletBoomerang bulletBoomerang = BulletBoomerang.Create(Player.GetCurrent(),mAttack);
            bulletBoomerang.transform.position=Player.GetCurrent().transform.position;
           
            Vector3 direction=(nearEnemy.transform.position-bulletBoomerang.transform.position).normalized;
            bulletBoomerang.transform.rotation=FightUtility.DirectionToRotation(direction);
            FightModel.GetCurrent().AddPlayerBullet(bulletBoomerang);
        },mShootTime);
        mBulletShooter=shooter;
    }

}