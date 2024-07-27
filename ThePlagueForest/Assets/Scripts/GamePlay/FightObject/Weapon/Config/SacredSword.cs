using System.Buffers.Text;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//圣剑
public class SacredSword:Weapon
{
    public SacredSword():base(EquipmentType.Active,EquipmentId.SacredSword)
    {
        
    }
    public override void OnGet(StatusEffect statusEffect,int layer)
    {
        mAttack=15;
        mShootTime=2.2f;
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
            BulletSacredSword bulletSacredSword = BulletSacredSword.Create(Player.GetCurrent(),mAttack);
            bulletSacredSword.transform.position=Player.GetCurrent().transform.position;
           
            Vector3 direction=(nearEnemy.transform.position-bulletSacredSword.transform.position).normalized;
            bulletSacredSword.transform.rotation=FightUtility.DirectionToRotation(direction);
            FightModel.GetCurrent().AddPlayerBullet(bulletSacredSword);
        },mShootTime);
        mBulletShooter=shooter;
    }

}