using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using Unity.Mathematics;
using UnityEngine;
using UnityEngine.UIElements;

//骷髅兵
public class Enemy6 : Enemy
{
    public static Enemy6 Create(Vector3 position,int level)
    {
        GameObject enemyObject=FightManager.GetCurrent().GetPoolManager().GetGameObject("Character/Enemy6");
        enemyObject.SetActive(true);
        enemyObject.transform.position=position;
        Enemy6 enemy=enemyObject.AddComponent<Enemy6>();
        PropertySheet propertySheet=CharacterUtility.GetBasePropertySheet("Enemy6",level,true);
        enemy.Init(propertySheet);
        return enemy;
    }
    protected void Init(PropertySheet basePropertySheet)
    {
        base.Init(CharacterId.Enemy6,basePropertySheet);
        mName="Enemy6";
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
            Debug.Log("播放");
            mAnimator.Play("Enemy6Attack");
        }
        AnimatorStateInfo stateInfo = mAnimator.GetCurrentAnimatorStateInfo(0);
        if(stateInfo.IsName("Enemy6Attack")&&stateInfo.normalizedTime>=1)
        {
            mIsOnShoot=false;
            mIsOnCollidePlayer=false;
            mAnimator.Play("Enemy6Walk");
        }
    }
    
}
