using System.Buffers.Text;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//圣剑
public class SacredSword:Weapon
{
    private int mAttackAddition=10;
    private float mShootTimeReduce=0.1f;
    private float mBulletScale=1;
    public SacredSword():base(EquipmentType.Active,EquipmentId.SacredSword)
    {
        mBaseAttack=20;
        mShootTime=1.2f;
        mAttack=mBaseAttack;
        mStatusEffectId=StatusEffectId.Equipment_SacredSword;
    }
    public override void OnGet(StatusEffect statusEffect,int layer)
    {
        if(layer!=1)
        {
            if(layer==mMaxlayer)
            {
                mBulletScale*=1.5f;
            }
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
            BulletSacredSword bulletSacredSword = BulletSacredSword.Create(Player.GetCurrent(),mAttack,mBulletScale);
            bulletSacredSword.transform.position=Player.GetCurrent().transform.position;
           
            Vector3 direction=(nearEnemy.transform.position-bulletSacredSword.transform.position).normalized;
            bulletSacredSword.transform.rotation=FightUtility.DirectionToRotation(direction);
            FightModel.GetCurrent().AddPlayerBullet(bulletSacredSword);
        },mShootTime);
        mBulletShooter=shooter;
    }

}