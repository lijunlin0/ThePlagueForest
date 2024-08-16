using DG.Tweening;
using UnityEngine;

//巫师
public class Enemy2 : Enemy
{
    protected const int ShootRange=600;
    public static Enemy2 Create(Vector3 position,int level)
    {
        GameObject enemyObject=FightManager.GetCurrent().GetPoolManager().GetGameObject("Character/Enemy2");
        enemyObject.SetActive(true);
        enemyObject.transform.position=position;
        Enemy2 enemy=enemyObject.AddComponent<Enemy2>();
        PropertySheet propertySheet=CharacterUtility.GetBasePropertySheet("Enemy2",level,true);
        enemy.Init(CharacterId.Enemy2,propertySheet);
        return enemy;
    }

    protected override void Init(CharacterId characterId,PropertySheet basePropertySheet)
    {
        base.Init(characterId,basePropertySheet);
        mName="Enemy2";
        mIsPoolObject=true;
        mdeathAnimationTime=1.5f;
        mEnemyType=EnemyType.Elite;
        //添加子弹发射器
        mBulletShooter = new BulletShooter(EquipmentId.None,this,()=>
        {
            Shoot();
        },3,ShootRange,2,()=>{mAnimator.Play("Enemy2Attack");});
    }
    protected override void Shoot()
    {
        mAnimator.Play("Enemy2Attack");
        //创建子弹
        Enemy2Bullet bullet=Enemy2Bullet.Create(this,mCurrentPropertySheet.GetAttack());
        //方向朝着玩家
        Vector3 direction=(FightModel.GetCurrent().GetPlayer().transform.position-bullet.transform.position).normalized;
        bullet.transform.localRotation=FightUtility.DirectionToRotation(direction);
        FightModel.GetCurrent().AddEnemyBullets(bullet);
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
            mAnimator.Play("Enemy2Attack");
        }
        AnimatorStateInfo stateInfo = mAnimator.GetCurrentAnimatorStateInfo(0);
        if(stateInfo.IsName("Enemy2Attack")&&stateInfo.normalizedTime>=1)
        {
            mIsOnShoot=false;
            mAnimator.Play("Enemy2Walk");
        }
    }
}
