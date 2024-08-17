using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using Unity.Mathematics;
using UnityEngine;
using UnityEngine.UIElements;

//狼
public class Enemy3 : Enemy
{
    public static Enemy3 Create(Vector3 position,int level)
    {
        GameObject enemyObject=FightManager.GetCurrent().GetPoolManager().GetGameObject("Character/Enemy3");
        enemyObject.SetActive(true);
        enemyObject.transform.position=position;
        Enemy3 enemy=enemyObject.AddComponent<Enemy3>();
        PropertySheet propertySheet=CharacterUtility.GetBasePropertySheet("Enemy3",level,true);
        enemy.Init(propertySheet);
        return enemy;
    }
    protected void Init(PropertySheet basePropertySheet)
    {
        base.Init(CharacterId.Enemy3,basePropertySheet);
        mName="Enemy3";
        mIsPoolObject=true;
        mdeathAnimationTime=1;
    }

    public override void OnUpdate()
    {
        base.OnUpdate();
    }
    
}
