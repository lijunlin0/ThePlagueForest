using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using Unity.Mathematics;
using UnityEngine;
using UnityEngine.UIElements;

//哥布林
public class Enemy4 : Enemy
{
    public static Enemy4 Create(Vector3 position,int level)
    {
        GameObject enemyObject=FightManager.GetCurrent().GetPoolManager().GetGameObject("Character/Enemy4");
        enemyObject.SetActive(true);
        enemyObject.transform.position=position;
        Enemy4 enemy=enemyObject.AddComponent<Enemy4>();
        PropertySheet propertySheet=CharacterUtility.GetBasePropertySheet("Enemy4",level,true);
        enemy.Init(propertySheet);
        return enemy;
    }
    protected void Init(PropertySheet basePropertySheet)
    {
        base.Init(CharacterId.Enemy4,basePropertySheet);
        mName="Enemy4";
        mIsPoolObject=true;
        mdeathAnimationTime=0.75f;
    }

    public override void OnUpdate()
    {
        base.OnUpdate();
        if(mIsOnCollidePlayer)
        {
            mIsOnShoot=true;
            mAnimator.Play("Enemy4Attack");
        }
        AnimatorStateInfo stateInfo = mAnimator.GetCurrentAnimatorStateInfo(0);
        if(stateInfo.IsName("Enemy4Attack")&&stateInfo.normalizedTime>=1)
        {
            mIsOnShoot=false;
            mIsOnCollidePlayer=false;
            mAnimator.Play("Enemy4Walk");
        }
    }
    
}
