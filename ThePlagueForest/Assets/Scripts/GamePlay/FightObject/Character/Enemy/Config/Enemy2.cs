using System.Collections;
using System.Collections.Generic;
using Unity.Mathematics;
using UnityEngine;
using UnityEngine.UIElements;

//巫师
public class Enemy2 : Enemy
{
    public static Enemy2 Create()
    {
        GameObject enemyPrefab=Resources.Load<GameObject>("FightObject/Character/Enemy2");
        GameObject enemyObject=GameObject.Instantiate(enemyPrefab);
        enemyObject.transform.position=new Vector3(-400,400,-1);
        Enemy2 enemy=enemyObject.AddComponent<Enemy2>();
        PropertySheet propertySheet=CharacterUtility.GetBasePropertySheet("Enemy2",1);
        enemy.Init(CharacterId.Enemy2,propertySheet);
        return enemy;
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
    }
    

}
