using DG.Tweening;
using UnityEngine;

//巫师
public class Enemy2 : Enemy
{
    public static Enemy2 Create(Vector3 position)
    {
        GameObject enemyPrefab=Resources.Load<GameObject>("FightObject/Character/Enemy2");
        GameObject enemyObject=GameObject.Instantiate(enemyPrefab);
        enemyObject.transform.position=position;
        Enemy2 enemy=enemyObject.AddComponent<Enemy2>();
        PropertySheet propertySheet=CharacterUtility.GetBasePropertySheet("Enemy2",1);
        enemy.Init(CharacterId.Enemy2,propertySheet);
        return enemy;
    }

    protected override void Init(CharacterId characterId,PropertySheet basePropertySheet)
    {
        base.Init(characterId,basePropertySheet);
        //添加子弹发射器
        mBulletShooter = new BulletShooter(()=>
        {
            Shoot();
        },3,2,()=>{mAnimator.Play("Enemy2Attack");});
    }
    protected override void Shoot()
    {
        //创建子弹
        Enemy2Bullet bullet=Enemy2Bullet.Create(this,10);
        bullet.transform.position=transform.position;
        //Debug.Log("敌人发射子弹,位置:"+bullet.transform.position);
        //方向朝着玩家
        Vector3 direction=(FightModel.GetCurrent().GetPlayer().transform.position-bullet.transform.position).normalized;
        bullet.transform.localRotation=FightUtility.DirectionToRotation(direction);
        FightModel.GetCurrent().AddEnemyBullets(bullet);
        mIsOnShoot=true;
    }

    public override void OnUpdate()
    {
        base.OnUpdate();
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
