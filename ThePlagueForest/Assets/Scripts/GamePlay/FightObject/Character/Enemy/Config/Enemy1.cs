using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using Unity.Mathematics;
using UnityEngine;
using UnityEngine.UIElements;

//丧尸
public class Enemy1 : Enemy
{
    public static Enemy1 Create(Vector3 position)
    {
        GameObject enemyPrefab=Resources.Load<GameObject>("FightObject/Character/Enemy1");
        GameObject enemyObject=GameObject.Instantiate(enemyPrefab);
        enemyObject.transform.position=position;
        Enemy1 enemy=enemyObject.AddComponent<Enemy1>();
        PropertySheet propertySheet=CharacterUtility.GetBasePropertySheet("Enemy1",1);
        enemy.Init(propertySheet);
        return enemy;
    }
    protected void Init(PropertySheet basePropertySheet)
    {
        base.Init(CharacterId.Enemy1,basePropertySheet);
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
    public override void PlayDestroyAnimation()
    {
        mCollider.GetCollider().enabled=false;
        mAnimator.Play("Enemy1Death");
        ExpBall.Create(this.transform.position,PlayerLevelController.EnemyTypeToExp(enemyType));
        Destroy(mHealthBar.gameObject);
        SpriteRenderer spriteRenderer=mDisplay.GetComponent<SpriteRenderer>();
        spriteRenderer.DOFade(0,1).OnComplete(()=>
        {
            ExpBall.Create(this.transform.position,PlayerLevelController.EnemyTypeToExp(enemyType));
            Destroy(gameObject);
        });

    }
}
