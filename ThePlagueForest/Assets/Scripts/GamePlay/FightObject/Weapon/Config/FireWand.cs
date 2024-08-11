using System.Buffers.Text;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//火焰魔杖
public class FireWand:Weapon
{
     private int mAttackAddition=10;
    private float mShootTimeReduce=0.1f;
    public FireWand():base(EquipmentType.Active,EquipmentId.FireWand)
    {
        mBaseAttack=20;
        mBaseShootTime=2f;
        mShootTime=mBaseShootTime;
        mAttack=mBaseAttack;
        mStatusEffectId=StatusEffectId.Equipment_FireWand;
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