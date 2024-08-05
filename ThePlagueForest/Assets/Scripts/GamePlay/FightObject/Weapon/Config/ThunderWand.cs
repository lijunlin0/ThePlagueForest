using System.Buffers.Text;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//闪电魔杖
public class ThunderWand:Weapon
{
    public ThunderWand():base(EquipmentType.Active,EquipmentId.ThunderWand)
    {
        mStatusEffectId=StatusEffectId.Equipment_ThunderWand;
    }

    public override void OnGet(StatusEffect statusEffect,int layer)
    {
        mShootTime=3;
        mAttack=20;
         BulletShooter shooter = new BulletShooter(()=>
         {
            //随机选择一名敌人
            List<Enemy> enemyList = FightModel.GetCurrent().GetEnemies();
            if(enemyList.Count==0)
            {
                mIsShoot=false;
                return;
            }
            int randomIndex=RandomHelper.RandomInt(0,enemyList.Count-1);
            Enemy targetEnemy=enemyList[randomIndex];
            
            mIsShoot=true;
            //创建子弹
            BulletThunderWand bulletThunderWand = BulletThunderWand.Create(Player.GetCurrent(),mAttack);
            bulletThunderWand.transform.position=targetEnemy.transform.position;
            FightModel.GetCurrent().AddPlayerBullet(bulletThunderWand);
        },mShootTime);
        mBulletShooter=shooter;
    }
  

}