using System.Collections;
using System.Collections.Generic;
using System.ComponentModel.Design;
using UnityEngine;

public class Weapon : Equipment
{
    protected BulletShooter mBulletShooter;
    //攻击间隔
    protected float mBaseShootTime=1f;
    protected float mShootTime=1f;
    public static float mAttackRange=400;
    protected int mBaseAttack=10;
    //攻击力
    protected int mAttack=10;
    protected bool mIsShoot=true;
    public Weapon(EquipmentType equipmentType,EquipmentId equipmentId):base(equipmentType,equipmentId)
    {
        mMaxlayer=5;
    }

    public virtual void OnUpdate()
    {
        if(mBulletShooter != null)
        {
            mBulletShooter.OnUpdate();
        }
    }

    public int GetWeaponAttack()
    {
        return mAttack;
    }

    public bool IsShoot(){return mIsShoot;}

    public BulletShooter GetBulletShooter()
    {
        return mBulletShooter;
    }

    public float GetBaseShootTime()
    {
        return mBaseShootTime;
    }
}