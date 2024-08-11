
using System;
using System.Collections.Generic;
using DG.Tweening;
using UnityEngine;

public static class FightUtility
{  
    public static Vector3 RotationToDirection(Quaternion rotation)
    {
        float radian=rotation.eulerAngles.z*Mathf.Deg2Rad;
        return new Vector3(Mathf.Cos(radian),MathF.Sin(radian),0).normalized;
    }

    public static Quaternion DirectionToRotation(Vector3 direction)
    {
        return Quaternion.Euler(0,0,DirectionToRadian(direction)*Mathf.Rad2Deg);
    }

    public static float DirectionToRadian(Vector3 direction)
    {
        return Mathf.Atan2(direction.y,direction.x);
    }

    public static  Quaternion RadianToRotation(float radian)
    {
        return Quaternion.Euler(0,0,radian*Mathf.Rad2Deg);
    }

    public static void ChainEffect(List<Character> targets)
    {
        GameObject prefab=Resources.Load<GameObject>("FightObject/Effect/Bullet/StunChain");
        for(int i=1;i<targets.Count;i++)
        {
            Character startEnemy=targets[i-1];
            Character endEnemy=targets[i];
            GameObject effect=GameObject.Instantiate(prefab);
            Callback updatePosition=()=>
            {

                LineLink(effect,startEnemy.transform.position,endEnemy.transform.position);
            };
            updatePosition();

            DOVirtual.Float(0,1,0.5f,(float f)=>
            {
                updatePosition();
            }).OnComplete(()=>
            {
                GameObject.Destroy(effect);
            });
        }
    }

    public static Enemy GetNearEnemy(Character character,List<Character> ignoreList=null)
    {
        if(character.IsEnemy())
        {
            ignoreList.Add(character as Enemy);
        }
        float minDistance = float.MaxValue;
        Enemy res=null;
        List<Enemy> enemies=FightModel.GetCurrent().GetEnemies();
        foreach(Enemy enemy in enemies)
        {
            if(ignoreList!=null&&ignoreList.Contains(enemy))
            {
                continue;
            }
            float distance=Vector3.Distance(character.transform.position,enemy.transform.position);
            if(distance<Weapon.mAttackRange&&distance<minDistance)
            {
                res=enemy;
                minDistance=distance;
                //Debug.Log(minDistance);
            }
        }
        return res;
    }

    public static void LineLink(GameObject fightObject,Vector3 startPosition,Vector3 endPosition,float xFactor=0.01f)
    {
        Vector3 direction=endPosition-startPosition;
        fightObject.transform.localPosition=(startPosition+endPosition)/2;
        fightObject.transform.localRotation=DirectionToRotation(direction);
        float distance=Mathf.Sqrt(SqrDistance2D(startPosition,endPosition));
        float scaleY=fightObject.transform.localScale.y;

        fightObject.transform.localScale=new Vector3(distance*xFactor,scaleY,1);

    }
    
    public static float SqrDistance2D(Vector3 position1,Vector3 position2)
    {
        float dx=position1.x-position2.x;
        float dy=position1.y-position2.y;
        return dx*dx+dy*dy;
    }

    public static void Move(GameObject gameObject,float moveSpeed)
    {
        Vector3 moveDirection=FightUtility.RotationToDirection(gameObject.transform.rotation);
        gameObject.transform.localPosition+=moveDirection*moveSpeed*Time.deltaTime;
    }


}
