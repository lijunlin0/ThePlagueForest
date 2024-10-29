using UnityEngine;

//丧尸
public class Enemy1 : Enemy
{
    public static Enemy1 Create(Vector3 position,int level)
    {
        GameObject enemyObject=FightManager.GetCurrent().GetPoolManager().GetGameObject("Character/Enemy1");
        enemyObject.SetActive(true);
        enemyObject.transform.position=position;
        Enemy1 enemy=enemyObject.AddComponent<Enemy1>();
        PropertySheet propertySheet=CharacterUtility.GetBasePropertySheet("Enemy1",level,true);
        enemy.Init(propertySheet);
        return enemy;
    }
    protected void Init(PropertySheet basePropertySheet)
    {
        base.Init(CharacterId.Enemy1,basePropertySheet);
        mName="Enemy1";
        mIsPoolObject=true;
        mdeathAnimationTime=1;
    }

    public override void OnUpdate()
    {
        base.OnUpdate();
        if(mIsOnCollidePlayer)
        {
            mIsOnShoot=true;
            mAnimator.Play("Enemy1Attack");
        }
        AnimatorStateInfo stateInfo = mAnimator.GetCurrentAnimatorStateInfo(0);
        if(stateInfo.IsName("Enemy1Attack")&&stateInfo.normalizedTime>=1)
        {
            mIsOnShoot=false;
            mIsOnCollidePlayer=false;
            mAnimator.Play("Enemy1Walk");
        }
    }
    
}
