using DG.Tweening;
using UnityEngine;

//死神
public class Boss2 : Enemy
{
    protected static float mTotalAngle=140;
    protected static int mBulletCount=5;
    protected bool mIsdestination=false;
    protected const int ShootRange=1200;
    protected bool mLookDirection=true;
    public static Boss2 Create(Vector3 position,int level)
    {
        GameObject enemyPrefab=Resources.Load<GameObject>("FightObject/Character/Boss2");
        GameObject enemyObject=GameObject.Instantiate(enemyPrefab);
        enemyObject.transform.position=position;
        Boss2 enemy=enemyObject.AddComponent<Boss2>();
        PropertySheet propertySheet=CharacterUtility.GetBasePropertySheet("Boss2",level,true);
        enemy.Init(propertySheet);
        return enemy;
    }

    protected void Init(PropertySheet basePropertySheet)
    {
        base.Init(CharacterId.Boss2,basePropertySheet);
        mName="Boss2";
        mdeathAnimationTime=1.5f;
        mEnemyType=EnemyType.Boss;
        //添加子弹发射器
        mBulletShooter = new BulletShooter(EquipmentId.None,this,()=>
        {
            Shoot();
        },5,ShootRange,4.5f,()=>{mAnimator.Play("Boss2Attack");});
    }
    protected override void Shoot()
    {
        Vector3 direction=Player.GetCurrent().transform.position-transform.position;
        float baseAngle = FightUtility.DirectionToRadian(direction)*Mathf.Rad2Deg;
        float startAngle=-mTotalAngle/2;
        float stepAngle=mTotalAngle/(mBulletCount-1);
        float rotation;
        //创建子弹
        for(int i=0; i<mBulletCount; i++)
        {
            rotation=startAngle+i*stepAngle;
            Boss2Bullet bullet=Boss2Bullet.Create(this,mCurrentPropertySheet.GetAttack(),baseAngle+rotation);
            FightModel.GetCurrent().AddEnemyBullets(bullet);
        }
        mIsOnShoot=true;
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
            mAnimator.Play("Boss2Attack");
        }
        AnimatorStateInfo stateInfo = mAnimator.GetCurrentAnimatorStateInfo(0);
        if(stateInfo.IsName("Boss2Attack")&&stateInfo.normalizedTime>=1)
        {
            mIsOnShoot=false;
            mAnimator.Play("Boss2Walk");
        }
    }
}
