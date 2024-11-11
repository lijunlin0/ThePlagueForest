
using System.Collections.Generic;
using System.IO.Enumeration;
using System.Net.NetworkInformation;
using DG.Tweening;
using Unity.Mathematics;
using UnityEngine;

public class FightModel
{
    private bool mIsStartTime=false;
    public static int mHealthChangeTextCount=0;
    private static FightModel sCurrent;
    //敌人
    private List<Enemy> mEnemyList;
    //玩家
    private Player mPlayer;
    //敌人子弹
    private List<Bullet> mEnemyBulletList;
    //玩家子弹
    private List<Bullet> mPlayerBulletList;
    //装备以及层数
    private Dictionary<Equipment,int> mEquipments;
    private EnemyCreateManager mEnemyCreateManager;
    public List<Bullet> GetEnemyBullets()
    {
        return mEnemyBulletList;
    }
    public List<Bullet> GetPlayerBullets()
    {
        return mPlayerBulletList;
    }
    public void AddPlayerBullet(Bullet bullet)
    {
        mPlayerBulletList.Add(bullet);
    }
    public void AddEnemyBullets(Bullet bullet)
    {
        mEnemyBulletList.Add(bullet);
    }
    public List<Enemy> GetEnemies()
    {
        return mEnemyList;
    }
    public Player GetPlayer()
    {
        return mPlayer;
    }
    public static FightModel GetCurrent()
    {
        return sCurrent;
    }
    public  FightModel()
    {
        sCurrent=this;
        mEnemyList=new List<Enemy>();
        mPlayer=Player1.Create();
        mEnemyBulletList=new List<Bullet>();
        mPlayerBulletList=new List<Bullet>();
        mEquipments=new Dictionary<Equipment,int>();
        mEnemyCreateManager=new EnemyCreateManager();
    }
    public void OnUpdate()
    {
       mEnemyCreateManager.OnUpdate();
        CollisionHelper.Collide();
       UpdateObjects();

       RemoveInvalidObjects();
    }
    public void AddEquipment(Equipment equipment)
    {
        if(!mEquipments.ContainsKey(equipment))
        {
            mEquipments.Add(equipment, 0);
        }
        mEquipments[equipment]+=1;
    }

    public int GetEquipmentLayer(Equipment equipment)
    {
        if(!mEquipments.ContainsKey(equipment))
        {
            return 0;
        }
        return mEquipments[equipment];
    }

    public EnemyCreateManager GetEnemyCreateManager(){return mEnemyCreateManager;}

    public bool IsStartTime(){return mIsStartTime;}
    public void TimeStart(){mIsStartTime=true;}


    private void UpdateObjects()
    {
         mPlayer.OnUpdate();
        foreach(Enemy enemy in mEnemyList)
        {
            enemy.OnUpdate();
        }
        foreach(Bullet enemyBullet in mEnemyBulletList )
        {
            enemyBullet.OnUpdate();
        }
        foreach(Bullet playerBullet in mPlayerBulletList)
        {
            playerBullet.OnUpdate();
        }
    }

    private void RemoveInvalidObjects()
    {
        //删除销毁的敌人子弹
        for(int i=0;i<mEnemyBulletList.Count;)
        {
            Bullet bullet=mEnemyBulletList[i];
            if(bullet.IsDead())
            {
                mEnemyBulletList.RemoveAt(i);
                bullet.PlayDestroyAnimation();
            }
            else
            {
                i++;
            }
        }
        //删除销毁的玩家子弹
        for(int i=0;i<mPlayerBulletList.Count;)
        {
            Bullet bullet=mPlayerBulletList[i];
            if(bullet.IsDead())
            {
                mPlayerBulletList.RemoveAt(i);
                bullet.PlayDestroyAnimation();
            }
            else
            {
                i++;
            }
        }
        //删除销毁的敌人
        for(int i=0;i<mEnemyList.Count;)
        {
            Enemy enemy=mEnemyList[i];
            if(enemy.IsDead())
            {
                mEnemyList.RemoveAt(i);
                enemy.PlayDestroyAnimation();
            }
            else
            {
                i++;
            }
        }
    } 
}