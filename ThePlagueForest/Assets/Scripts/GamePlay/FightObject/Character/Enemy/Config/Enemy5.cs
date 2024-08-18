using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using Unity.Mathematics;
using UnityEngine;
using UnityEngine.UIElements;

//蘑菇怪
public class Enemy5 : Enemy
{
    public static Enemy5 Create(Vector3 position,int level)
    {
        GameObject enemyObject=FightManager.GetCurrent().GetPoolManager().GetGameObject("Character/Enemy5");
        enemyObject.SetActive(true);
        enemyObject.transform.position=position;
        Enemy5 enemy=enemyObject.AddComponent<Enemy5>();
        PropertySheet propertySheet=CharacterUtility.GetBasePropertySheet("Enemy5",level,true);
        enemy.Init(propertySheet);
        return enemy;
    }
    protected void Init(PropertySheet basePropertySheet)
    {
        base.Init(CharacterId.Enemy5,basePropertySheet);
        mName="Enemy5";
        mEnemyType=EnemyType.Elite;
        mIsPoolObject=true;
        mdeathAnimationTime=0.75f;
    }

    public override void OnUpdate()
    {
        base.OnUpdate();
        if(mIsOnCollidePlayer)
        {
            mIsOnShoot=true;
            mAnimator.Play("Enemy5Attack");
        }
        AnimatorStateInfo stateInfo = mAnimator.GetCurrentAnimatorStateInfo(0);
        if(stateInfo.IsName("Enemy5Attack")&&stateInfo.normalizedTime>=1)
        {
            mIsOnShoot=false;
            mIsOnCollidePlayer=false;
            mAnimator.Play("Enemy5Walk");
        }
    }
    
}
