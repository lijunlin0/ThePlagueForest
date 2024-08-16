using System.Buffers.Text;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//Enemy2直线普通子弹
public class Enemy2Bullet : Bullet
{
    public static Enemy2Bullet Create(Enemy enemy,int points)
    {
        GameObject bulletObject=FightManager.GetCurrent().GetPoolManager().GetGameObject("Bullet/MagicBall");
        bulletObject.SetActive(true);
        bulletObject.transform.position=enemy.transform.position;
        Enemy2Bullet bullet=bulletObject.AddComponent<Enemy2Bullet>();
        bullet.Init(enemy,points);
        return bullet;
    }
    protected override void Init(Character character,int points)
    {
        base.Init(character,points);
        mIsPoolObject=true;
        mMoveSpeed=450;
    }
    public override void OnUpdate()
    {
        base.OnUpdate();
        if(mIsDead)
        {
            return;
        }
        FightUtility.Move(gameObject,mMoveSpeed);
    }
}