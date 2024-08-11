using System.Buffers.Text;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//回旋镖
public class Boomerang:Weapon
{
    private int mAttackAddition=10;
    private float mShootTimeReduce=0.1f;
    public Boomerang():base(EquipmentType.Active,EquipmentId.Boomerang)
    {
        mBaseAttack=20;
        mBaseShootTime=2.5f;
        mShootTime=mBaseShootTime;
        mAttack=mBaseAttack;
        mStatusEffectId=StatusEffectId.Equipment_Boomerang;
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
            BulletBoomerang bulletBoomerang = BulletBoomerang.Create(Player.GetCurrent(),mAttack);
            bulletBoomerang.transform.position=Player.GetCurrent().transform.position;
           
            Vector3 direction=(nearEnemy.transform.position-bulletBoomerang.transform.position).normalized;
            bulletBoomerang.transform.rotation=FightUtility.DirectionToRotation(direction);
            FightModel.GetCurrent().AddPlayerBullet(bulletBoomerang);
        },mShootTime);
        mBulletShooter=shooter;
    }

}