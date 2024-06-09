using System.Collections;
using System.Collections.Generic;
using Unity.Mathematics;
using UnityEngine;
using UnityEngine.UIElements;

//巫师
public class Enemy2 : Enemy
{
    float mShootTime=2f;
    float defaultTime=0;
    public static Enemy2 Create()
    {
        GameObject enemyPrefab=Resources.Load<GameObject>("FightObject/Character/Enemy2");
        GameObject enemyObject=GameObject.Instantiate(enemyPrefab);
        enemyObject.transform.position=new Vector3(-400,400,-1);
        Enemy2 enemy=enemyObject.AddComponent<Enemy2>();
        enemy.Init();
        return enemy;
    }
    protected override void Init()
    {
        base.Init();
        defaultTime=mShootTime;
        CanShootFlag=true;
    }
    
    protected override void Shoot()
    {
        //创建子弹
        Enemy2Bullet bullet=Enemy2Bullet.Create(this);
        bullet.transform.position=transform.position;

        //方向朝着玩家
        Vector3 direction=(FightModel.GetCurrent().GetPlayer().transform.position-bullet.transform.position).normalized;
        bullet.transform.localRotation=FightUtility.DirectionToRotation(direction);
        
        FightModel.GetCurrent().GetEnemyBullets().Add(bullet);
    }

    public override void OnUpdate()
    {
        if(defaultTime>=mShootTime)
        {
            Shoot();
            defaultTime=0;
        }
        defaultTime+=Time.deltaTime;
    }

}
