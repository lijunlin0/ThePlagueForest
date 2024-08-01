using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UIElements;

public enum EnemyType
{
    None,
    Normal=1,
    Elite=2,
    Boss=3,
}

public class Enemy : Character
{
    protected EnemyType enemyType;
    protected BulletShooter mBulletShooter;
    protected bool CanShootFlag=false;
    protected override void Init(CharacterId characterId,PropertySheet basePropertySheet)
    {
        enemyType=EnemyType.Normal;
        base.Init(characterId,basePropertySheet);
        mHealthBar=HealthBar.Create(this);
        //添加子弹发射器
        mBulletShooter = new BulletShooter(()=>
        {
            Shoot();
        },3);
    }
    public virtual void Move()
    {
        Vector3 playerPosition=Player.GetCurrent().transform.position;
        Vector3 enemyPosition=transform.position;
        Vector3 direction=playerPosition-enemyPosition;
        direction.z=0;
        float distance=direction.sqrMagnitude;
        if(distance>1)
        {   
            transform.localRotation=Quaternion.LookRotation(Vector3.forward, direction);
            transform.position+=direction.normalized*mCurrentPropertySheet.GetMoveSpeed()*Time.deltaTime;
            //Debug.Log("移动速度:"+mCurrentPropertySheet.GetMoveSpeed());
        }
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
        Move();
    }
    public override void PlayDestroyAnimation()
    {
        base.PlayDestroyAnimation();
        ExpBall.Create(this.transform.position,PlayerLevelController.EnemyTypeToExp(enemyType));
        Destroy(mHealthBar.gameObject);
    }
    public  void HealthChange(int health)
    {
        mHealth+=health;
    }
}
