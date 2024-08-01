using System.Collections;
using System.Collections.Generic;
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
        CanShootFlag=false;
        CanShootFlag=true;
    }
}
