
using System.Collections.Generic; 
using UnityEngine;

public class FightModel
{
    private const int EnemyCreateDistanceWithPlayer=400;
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
    private float EnemyCreateStartTime=0;
    private float EnemyCreateEndTime=1;
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
        //初始武器
        FightSystem.GetEquipment(EquipmentUtility.GetEquipment(EquipmentId.SacredSword));
        FightSystem.GetEquipment(EquipmentUtility.GetEquipment(EquipmentId.SacredSword));
        FightSystem.GetEquipment(EquipmentUtility.GetEquipment(EquipmentId.SacredSword));
        FightSystem.GetEquipment(EquipmentUtility.GetEquipment(EquipmentId.SacredSword));

    }
    public void EnemyCreate()
    {
        //随机一个敌人id
        List<CharacterId> idList=new List<CharacterId>();
        for(int i=(int)CharacterId.Enemy1;i<=(int)CharacterId.Enemy2;i++)
        {
            idList.Add((CharacterId)i);
        }
        int index=RandomHelper.RandomInt(0,idList.Count);
        //随机一个位置
        float distance=0;
        float x=0;
        float y=0;
        while(distance<=EnemyCreateDistanceWithPlayer)
        {
            x=RandomHelper.RandomInt(-960,960)+mPlayer.transform.position.x;
            y=RandomHelper.RandomInt(-540,540)+mPlayer.transform.position.y;
            distance=Vector3.Distance(new Vector3(x,y,-1),mPlayer.transform.position);
        }
        //生成敌人
        Enemy enemy = EnemyIdToEnemy(idList[index],new Vector3(x,y,-1));
        mEnemyList.Add(enemy);

    }

    private Enemy EnemyIdToEnemy(CharacterId id,Vector3 position)
    {
        switch(id)
        {
            case CharacterId.Enemy1:return Enemy1.Create(position);
            case CharacterId.Enemy2:return Enemy2.Create(position);
            default:return null;
        }
    }


    public void OnUpdate()
    {
        EnemyCreateStartTime+=Time.deltaTime;
        if(EnemyCreateStartTime>=EnemyCreateEndTime)
        {
            EnemyCreateStartTime=0;
            EnemyCreate();
        }
       UpdateObjects();
       CollisionHelper.Collide();
       RemoveInvalidObjects();
    }

    public void AddEquipment(Equipment equipment)
    {
        if(!mEquipments.ContainsKey(equipment))
        {
            mEquipments.Add(equipment, 0);
        }
        mEquipments[equipment]+=1;
        if(equipment is Weapon)
        {
            mPlayer.AddWeapon(equipment as Weapon);
        }
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