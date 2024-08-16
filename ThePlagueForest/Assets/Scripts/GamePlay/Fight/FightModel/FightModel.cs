
using System.Collections.Generic;
using System.IO.Enumeration;
using System.Net.NetworkInformation;
using DG.Tweening;
using Unity.Mathematics;
using UnityEngine;

public class FightModel
{
    private float mBossCreateDefaultTime=0;
    private float mBossCreateTime=0;
    
    private float mEnemyLevelUpDefaultTime=0;
    private float mEnemyLevelUpTime=0;

    private float mEnemyCreateDefaultTime=0;
    private float mEnemyCreateTime=2f;
    private const int EnemyCreateDistanceWithPlayer=400;
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
        mEnemyLevelUpTime=Enemy.sEnemyLevelUpTime;
        mBossCreateTime=Boss.sCreateTime;
        
        //初始武器
        EquipmentSelectWindow.Open(EquipmentUtility.GetStartWeapon(),"选择一把武器");
    }
    public void EnemyCreate()
    {
        List<CharacterId> idList=new List<CharacterId>();
        for(int i=(int)CharacterId.Enemy1;i<=(int)CharacterId.Enemy2;i++)
        {
            idList.Add((CharacterId)i);
        }
        //根据概率得到下标
        int randomNum=RandomHelper.RandomInt(0,100);
        int index=0;
        if(randomNum<(int)EnemyCreateChance.Enemy2)
        {
            index=idList.Count-1;
        }
        else if(randomNum<(int)EnemyCreateChance.Enemy1)
        {
            index=idList.Count-2;
        }
        //生成敌人
        Enemy enemy = EnemyIdToEnemy(idList[index],GetEnemyValidPosition(),Enemy.sLevel);
        //Debug.Log("敌人等级:"+Enemy.sLevel);

        mEnemyList.Add(enemy);

    }

    private Vector3 GetEnemyValidPosition()
    {
         //随机一个位置
        float distance=0;
        float x=0;
        float y=0;
        while(distance<=EnemyCreateDistanceWithPlayer)
        {
            x=RandomHelper.RandomIntTwoRange(-Utility.WindowWidth,-Utility.WindowWidth/2,Utility.WindowWidth,Utility.WindowWidth/2)+mPlayer.transform.position.x;
            y=RandomHelper.RandomIntTwoRange(-Utility.WindowHeight,-Utility.WindowHeight/2,Utility.WindowHeight,Utility.WindowHeight/2)+mPlayer.transform.position.y;
            distance=Vector3.Distance(new Vector3(x,y,-1),mPlayer.transform.position);
        }
        return new Vector3(x,y,-1);
    }

    private Enemy EnemyIdToEnemy(CharacterId id,Vector3 position,int level)
    {
        switch(id)
        {
            case CharacterId.Enemy1:return Enemy1.Create(position,level);
            case CharacterId.Enemy2:return Enemy2.Create(position,level);
            default:return null;
        }
    }


    public void OnUpdate()
    {
       EnemyCreateUpdate();
       UpdateObjects();
       CollisionHelper.Collide();
       RemoveInvalidObjects();
    }

    private void EnemyCreateUpdate()
    {
        mEnemyCreateDefaultTime+=Time.deltaTime;
        mEnemyLevelUpDefaultTime+=Time.deltaTime;
        mBossCreateDefaultTime+=Time.deltaTime;
        
        if(mEnemyCreateDefaultTime>=mEnemyCreateTime)
        {
            mEnemyCreateDefaultTime=0;
            Debug.Log("endTime"+mEnemyCreateTime);
            if(Enemy.sLevel%3==0)
            {
                CreateEnemyCircle();
            }
            else
            {
                EnemyCreate();
            }
            
        }
        if(mEnemyLevelUpDefaultTime>=mEnemyLevelUpTime)
        {
            mEnemyLevelUpDefaultTime=0;
            mEnemyCreateTime=Mathf.Clamp(mEnemyCreateTime-0.2f,0.01f,2);
            Enemy.sLevel++;
        }
         if(mBossCreateDefaultTime>=mBossCreateTime)
        {
            mBossCreateDefaultTime=0;
            Boss boss=Boss.Create(GetEnemyValidPosition(),Boss.sBossLevel);
            mEnemyList.Add(boss);
            Boss.sBossLevel+=1;
        }
    }

    private void CreateEnemyCircle()
    {
        float number=10;
        float angleStep = 360/number;
         for (int i = 0; i < number; i++)
        {
            // 计算当前敌人的角度
            float angle = i * angleStep;
            // 计算敌人的位置
            Vector3 enemyPosition = CalculatePosition(angle);
            EnemyCreate();
        }
    }
    private Vector3 CalculatePosition(float angle)
    {
        int r=2000;
        float radian=angle*Mathf.Rad2Deg;
        float x = mPlayer.transform.position.x + r * Mathf.Cos(radian);
        float y = mPlayer.transform.position.y + r * Mathf.Sin(radian);
        return new Vector3(x, y,mPlayer.transform.position.z);
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
                //Debug.Log("销毁子弹");
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