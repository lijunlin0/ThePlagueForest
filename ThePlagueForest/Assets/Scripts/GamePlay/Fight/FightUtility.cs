using System;
using System.Collections;
using System.Collections.Generic;
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

    public static Enemy GetNearEnemy()
    {
        float minDistance = float.MaxValue;
        Enemy res=null;
        List<Enemy> enemies=FightModel.GetCurrent().GetEnemies();
        foreach(Enemy enemy in enemies)
        {
            float distance=Vector3.Distance(FightModel.GetCurrent().GetPlayer().transform.position,enemy.transform.position);
            if(Vector3.Distance(FightModel.GetCurrent().GetPlayer().transform.position,enemy.transform.position)<minDistance)
            {
                res=enemy;
                minDistance=distance;
                Debug.Log(minDistance);
            }
        }
        return res;
    }

    public static void Move(GameObject gameObject,float moveSpeed)
    {
        Vector3 moveDirection=FightUtility.RotationToDirection(gameObject.transform.rotation);
        gameObject.transform.localPosition+=moveDirection*moveSpeed*Time.deltaTime;
    }


}
