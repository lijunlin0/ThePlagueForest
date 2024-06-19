using System.Collections;
using System.Collections.Generic;
using System.ComponentModel.Design;
using UnityEngine;

public class Weapon
{
    protected BulletShooter mBulletShooter;
    //攻击间隔
    protected static float mShootTime=0.5f;
    //攻击力
    protected int mAttack=10;
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
}