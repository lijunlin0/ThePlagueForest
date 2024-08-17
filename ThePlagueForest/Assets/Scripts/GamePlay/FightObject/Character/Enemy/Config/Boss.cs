using System.Collections.Generic;
using DG.Tweening;
using UnityEngine;

//火蠕虫
public class Boss : Enemy
{
    public static int sBossLevel=1;
    public static float sCreateTime=70;
    protected static float mTotalAngle=140;
    protected static int mBulletCount=8;
    protected bool mIsdestination=false;
    protected const int ShootRange=1000;
    protected bool mLookDirection=true;
    public static Boss Create(Vector3 position,int level)
    {
        GameObject enemyPrefab=Resources.Load<GameObject>("FightObject/Character/Boss");
        GameObject enemyObject=GameObject.Instantiate(enemyPrefab);
        enemyObject.transform.position=position;
        Boss enemy=enemyObject.AddComponent<Boss>();
        PropertySheet propertySheet=CharacterUtility.GetBasePropertySheet("Boss",level,true);
        enemy.Init(propertySheet);
        return enemy;
    }

    protected void Init(PropertySheet basePropertySheet)
    {
        base.Init(CharacterId.Boss,basePropertySheet);
        mName="Boss";
        mdeathAnimationTime=1.5f;
        mEnemyType=EnemyType.Boss;
        //添加子弹发射器
        mBulletShooter = new BulletShooter(EquipmentId.None,this,()=>
        {
            Shoot();
        },5,ShootRange,4f,()=>{mAnimator.Play("BossAttack");});
    }
    protected override void Shoot()
    {
        float startAngle=-mTotalAngle/2;
        float stepAngle=mTotalAngle/(mBulletCount-1);
        float rotation;
        //创建子弹
        for(int i=0; i<mBulletCount; i++)
        {
            rotation=startAngle+i*stepAngle;
            rotation=mLookDirection?rotation:rotation+180;
            BossBullet bullet=BossBullet.Create(this,mCurrentPropertySheet.GetAttack(),rotation);
            FightModel.GetCurrent().AddEnemyBullets(bullet);
        }
        mIsOnShoot=true;
    }

    protected override void Move()
    {
        if(mIsOnShoot)
        {
            return;
        }
        Player player=Player.GetCurrent();
        Vector3 playerPosition=Player.GetCurrent().transform.position;
        Vector3 enemyPosition=transform.position;
        Vector3 direction=playerPosition-enemyPosition;
        direction.z=0;
        float distance=Vector3.Distance(playerPosition,enemyPosition);
        Vector3 prePosition=transform.position;
        if(distance>ShootRange)
        {   
            transform.position+=direction.normalized*mCurrentPropertySheet.GetMoveSpeed()*Time.deltaTime;
            
        }
        else
        {
            float y=player.transform.position.y-transform.position.y;
            //超出一定距离后追赶
            if(Mathf.Abs(y)>=50)
            {
                Vector3 yDirection=new Vector3(0,y,0);
                transform.position+=yDirection.normalized*mCurrentPropertySheet.GetMoveSpeed()*Time.deltaTime;
            }
        }
        //根据位移方向转向
        if(transform.position.x>player.transform.position.x)
        {
            
            mSpriteRenderer.flipX=true;
            mLookDirection=false;
        }
        else
        {
            mSpriteRenderer.flipX=false;
            mLookDirection=true;
        }
        //没有实际位移,播放待机动画
        if(prePosition==transform.transform.position)
        {
            AnimatorStateInfo stateInfo = mAnimator.GetCurrentAnimatorStateInfo(0);
            if(!stateInfo.IsName("BossIdle"))
            {
                PlayLowAnimation("BossIdle");
            }
        }
    }

    protected override void OnDamage()
    {
        base.OnDamage();
        PlayLowAnimation("BossHurt");
    }

    public override void OnUpdate()
    {
        base.OnUpdate();
        if(mIsOnCollidePlayer)
        {
            DOVirtual.DelayedCall(1,()=>{
                mIsOnCollidePlayer=false;
            });
        }
        if(mIsOnShoot)
        {
            mAnimator.Play("BossAttack");
        }
        AnimatorStateInfo stateInfo = mAnimator.GetCurrentAnimatorStateInfo(0);
        if(stateInfo.IsName("BossAttack")&&stateInfo.normalizedTime>=1)
        {
            mIsOnShoot=false;
            mAnimator.Play("BossWalk");
        }
    }
    
    

    //播放低优先级动画
    private void PlayLowAnimation(string animationName)
    {
        AnimatorStateInfo info = mAnimator.GetCurrentAnimatorStateInfo(0);
        if(!info.IsName("BossAttack"))
        {
            mAnimator.Play(animationName);
            return;
        }
    }

}
