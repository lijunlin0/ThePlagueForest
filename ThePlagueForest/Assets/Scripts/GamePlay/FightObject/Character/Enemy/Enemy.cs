using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class Enemy : Character
{
    protected bool CanShootFlag=false;
    protected override void Init()
    {
        base.Init();
    }
    public virtual void Move()
    {
        
    }
    protected virtual void Shoot(){}
    protected virtual bool CanShoot()
    {
        return CanShootFlag;
    }
    public virtual void OnUpdate()
    {
       
    }
    public  void HealthChange(int health)
    {
        mHealth+=health;
    }
    public int GetHealth(){return mHealth;}
}
