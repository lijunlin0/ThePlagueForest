using System.Collections;
using System.Collections.Generic;
using Unity.Mathematics;
using UnityEngine;
using UnityEngine.UIElements;

//丧尸
public class Enemy1 : Enemy
{

    public static Enemy1 Create()
    {
        GameObject enemyPrefab=Resources.Load<GameObject>("FightObject/Character/Enemy1");
        GameObject enemyObject=GameObject.Instantiate(enemyPrefab);
        enemyObject.transform.position=new Vector3(900,500,-1);
        Enemy1 enemy=enemyObject.AddComponent<Enemy1>();
        enemy.Init();
        return enemy;
    }
    public override void Move()
    {
        base.Move();
        Vector3 playerPosition=Player.GetCurrent().transform.position;
        Vector3 enemyPosition=transform.position;
        Vector3 direction=playerPosition-enemyPosition;
        direction.z=0;
        float distance=direction.sqrMagnitude;
        if(distance>1)
        {   
            transform.localRotation=Quaternion.LookRotation(Vector3.forward, direction);
            transform.position+=direction.normalized*mSpeed*Time.deltaTime;
        }
    }
    protected override void Init()
    {
        base.Init();
        CanShootFlag=false;
        CanShootFlag=true;
    }
    public override void OnUpdate()
    {
        Move();
    }
}