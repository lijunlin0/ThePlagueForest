using System;
using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
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


public enum EnemyCreateChance
{
    Enemy1=90,
    Enemy2=10,
}

public class Enemy : Character
{
    public static int sLevel=1;
    //敌人升级时间间隔
    public static float sEnemyLevelUpTime=30;
    protected EnemyType enemyType;
    protected BulletShooter mBulletShooter;
    protected bool mIsOnCollidePlayer;
    protected bool mIsOnShoot;

    protected override void Init(CharacterId characterId,PropertySheet basePropertySheet)
    {
        base.Init(characterId,basePropertySheet);
        enemyType=EnemyType.Elite;
        mIsOnShoot=false;
        mHealthBar=HealthBar.Create(this);
    }
    public virtual void Move()
    {
        if(mIsOnShoot)
        {
            return;
        }
        AnimatorStateInfo stateInfo = mAnimator.GetCurrentAnimatorStateInfo(0);
        if(!stateInfo.IsName("Walk"))
        {
            
        }
        Vector3 playerPosition=Player.GetCurrent().transform.position;
        Vector3 enemyPosition=transform.position;
        Vector3 direction=playerPosition-enemyPosition;
        direction.z=0;
        float distance=direction.sqrMagnitude;
        if(distance>1)
        {   
            Vector3 prePosition=transform.position;
            //transform.localRotation=Quaternion.LookRotation(Vector3.forward, direction);
            transform.position+=direction.normalized*mCurrentPropertySheet.GetMoveSpeed()*Time.deltaTime;
            //根据位移方向转向
            if(transform.position.x-prePosition.x>0)
            {
            
                mSpriteRenderer.flipX=false;
            }
            else
            {
                mSpriteRenderer.flipX=true;
            }
        }
        
    }
    protected virtual void Shoot(){}
    public virtual void OnCollidePlayer()
    {
        mIsOnCollidePlayer=true;
    }
    public override void OnUpdate()
    {
        base.OnUpdate();
        if(mBulletShooter!=null)
        {
            mBulletShooter.OnUpdate();
        }
        Move();
    }
    public override void PlayDestroyAnimation()
    {
        base.PlayDestroyAnimation();
        int expPoints=PlayerLevelController.EnemyTypeToExp(enemyType);
        string expBallName=PlayerLevelController.EnemyTypeToExpBallName(enemyType);
        ExpBall.Create(this.transform.position,expPoints,expBallName);
        Destroy(mHealthBar.gameObject);
    }
    public  void HealthChange(int health)
    {
        mHealth+=health;
    }
    public bool IsOnCollidePlayer()
    {
        return mIsOnCollidePlayer;
    }
}
