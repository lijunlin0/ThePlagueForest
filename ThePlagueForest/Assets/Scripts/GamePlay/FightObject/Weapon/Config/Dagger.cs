using System.Buffers.Text;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//匕首
public class Dagger:Weapon
{
     private int mAttackAddition=10;
    private float mShootTimeReduce=0.1f;
    public Dagger():base(EquipmentType.Active,EquipmentId.Dagger)
    {
        mBaseAttack=20;
        mBaseShootTime=1f;
        mShootTime=mBaseShootTime;
        mAttack=mBaseAttack;
        mStatusEffectId=StatusEffectId.Equipment_Dagger;
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