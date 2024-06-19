using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class Enemy : Character
{
    protected BulletShooter mBulletShooter;
    protected bool CanShootFlag=false;
    protected override void Init(PropertySheet basePropertySheet)
    {
        base.Init(basePropertySheet);
        mHealthBar=HealthBar.Create(this);
        //添加子弹发射器
        mBulletShooter = new BulletShooter(()=>
        {
            Shoot();
        },1);
    }
    public virtual void Move()
    {
        
    }
    protected virtual void Shoot(){}
    protected virtual bool CanShoot()
    {
        return CanShootFlag;
    }
    public override void OnUpdate()
    {
        base.OnUpdate();
       mBulletShooter.OnUpdate();
    }
    public override void PlayDestroyAnimation()
    {
        base.PlayDestroyAnimation();
        Destroy(mHealthBar.gameObject);
    }
    public  void HealthChange(int health)
    {
        mHealth+=health;
    }
}
