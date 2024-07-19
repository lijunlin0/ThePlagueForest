using System.Collections;
using System.Collections.Generic;
using System.ComponentModel.Design;
using UnityEngine;

public class Weapon
{
    protected BulletShooter mBulletShooter;
    //攻击间隔
    protected float mShootTime=1f;
    public static float mAttackRange=400;
    //攻击力
    protected int mAttack=10;
    protected bool mIsShoot=true;
    public virtual void Init()
    {
       
    } 
    public virtual void OnUpdate()
    {

    }

    public int GetWeaponAttack()
    {
        return mAttack;
    }

    public bool IsShoot(){return mIsShoot;}
}