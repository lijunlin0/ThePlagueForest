using System.Buffers.Text;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//闪电魔杖
public class ThunderWand:Weapon
{
    private int mAttackAddition=10;
    private float mShootTimeReduce=0.1f;
    public ThunderWand():base(EquipmentType.Active,EquipmentId.ThunderWand)
    {
        mBaseAttack=20;
        mBaseShootTime=2f;
        mShootTime=mBaseShootTime;
        mAttack=mBaseAttack;
        mStatusEffectId=StatusEffectId.Equipment_ThunderWand;
    }

    public override void OnGet(StatusEffect statusEffect,int layer)
    {
        if(layer!=1)
        {
           mAttack+=mBaseAttack*mAttackAddition/100;
            mShootTime-=mShootTimeReduce;
            Debug.Log("攻击力:"+mAttack+"攻速间隔:"+mShootTime);
            Player.GetCurrent().RemoveWeaponWithStatusEffectId(mStatusEffectId);
        }
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