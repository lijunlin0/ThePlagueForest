using System;
using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using Unity.Collections;
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
    protected EnemyType mEnemyType;
    protected BulletShooter mBulletShooter;
    protected bool mIsOnCollidePlayer;
    protected bool mIsOnShoot;
    protected String mName="";
    protected float mdeathAnimationTime;


    protected override void Init(CharacterId characterId,PropertySheet basePropertySheet)
    {
        base.Init(characterId,basePropertySheet);
        mEnemyType=EnemyType.Normal;
        mIsOnShoot=false;
        mHealthBar=HealthBar.Create(this);
    }
    protected virtual void Move()
    {
        if(mIsOnShoot)
        {
            return;
        }
        mPrePosition=transform.position;
        Vector3 playerPosition=Player.GetCurrent().transform.position;
        Vector3 enemyPosition=transform.position;
        Vector3 direction=playerPosition-enemyPosition;
        direction.z=0;
        float distance=direction.sqrMagnitude;
        if(distance>1)
        {   
            Vector3 prePosition=transform.position;
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
        if(mEnemyType==EnemyType.Boss)
        {
            List<Enemy> bossList=FightModel.GetCurrent().GetEnemyCreateManager().GetBossList();
            bossList.RemoveAt(0);
            if(bossList.Count>0)
            {
                bossList[0].GetHealthBar().gameObject.SetActive(true);
            }
        }
        mCollider.GetCollider().enabled=false;
        mAnimator.Play(mName+"Death");
        mSpriteRenderer.DOFade(0,mdeathAnimationTime).OnComplete(()=>
        {
            string expBallName=PlayerLevelController.EnemyTypeToExpBallName(mEnemyType);
            ExpBall.Create(this.transform.position,mEnemyType,expBallName);
            DOTween.Kill(gameObject);
            if(mIsPoolObject)
            {
                // 重置
                Color color = mSpriteRenderer.color;
                color.a = 1;
                mSpriteRenderer.color = color;
                mCollider.GetCollider().enabled=true;
                FightManager.GetCurrent().GetPoolManager().PutGameObject(gameObject);
                Destroy(this);
            }
            else
            {
                Destroy(gameObject,3);
            }
            
        });
    }
    public  void HealthChange(int health)
    {
        mHealth+=health;
    }
    public bool IsOnCollidePlayer()
    {
        return mIsOnCollidePlayer;
    }

    public HealthBar GetHealthBar(){return mHealthBar;}
    
}
