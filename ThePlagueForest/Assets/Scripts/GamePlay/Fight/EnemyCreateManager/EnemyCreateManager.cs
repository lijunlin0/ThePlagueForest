using System.Collections.Generic;
using UnityEngine;

public enum EnemyCreateChance
{
    Enemy1=40,
    Enemy3=30,
    Enemy2=10,
}

public class EnemyCreateManager
{
    private int mNormalLevel=1;
    private int mBossLevel=1;
    private float mBossCreateDefaultTime=0;
    private float mBossCreateTime=70;
    
    private float mEnemyLevelUpDefaultTime=0;
    private float mEnemyLevelUpTime=30;

    private float mEnemyCreateDefaultTime=0;
    private float mEnemyCreateTime=3;
    private const int EnemyCreateDistanceWithPlayer=400;
    private Player mPlayer;
    private List<Enemy> mEnemies;
    //敌人生成概率表
    private Dictionary<CharacterId,int> mEnemyChance;
    public EnemyCreateManager()
    {
        mPlayer=Player.GetCurrent();
        mEnemies=FightModel.GetCurrent().GetEnemies();
        mEnemyChance=new Dictionary<CharacterId,int>()
        {
            {CharacterId.Enemy2,5},//巫师
            {CharacterId.Enemy6,7},//骷髅兵
            {CharacterId.Enemy5,15},//蘑菇怪
            {CharacterId.Enemy4,18},//哥布林
            {CharacterId.Enemy3,20},//狼
            {CharacterId.Enemy1,35},//丧尸
        };
    }

    private Vector3 GetEnemyValidPosition()
    {
        //随机一个位置
        float distance=0;
        float x=0;
        float y=0;
        while(distance<=EnemyCreateDistanceWithPlayer)
        {
            x=RandomHelper.RandomIntTwoRange(-Utility.WindowWidth/2,-Utility.WindowWidth/2,Utility.WindowWidth/2,Utility.WindowWidth/2)+mPlayer.transform.position.x;
            y=RandomHelper.RandomIntTwoRange(-Utility.WindowHeight/2,-Utility.WindowHeight/2,Utility.WindowHeight/2,Utility.WindowHeight/2)+mPlayer.transform.position.y;
            distance=Vector3.Distance(new Vector3(x,y,-1),mPlayer.transform.position);
        }
        return new Vector3(x,y,-1);
    }
    private void EnemyCreate(Vector3 position)
    {
        int randomInt=RandomHelper.RandomInt(0,100);
        int cumulativeChance = 0;
        CharacterId characterId=CharacterId.Enemy1;
        foreach(var entry in mEnemyChance)
        {
            cumulativeChance+=entry.Value;
            if(randomInt<entry.Value)
            {
                characterId=entry.Key;
                break;
            }
        }
        //生成敌人
        Enemy enemy = EnemyIdToEnemy(characterId,position,mNormalLevel);
        mEnemies.Add(enemy);

    }
    private Enemy EnemyIdToEnemy(CharacterId id,Vector3 position,int level)
    {
        switch(id)
        {
            case CharacterId.Enemy1:return Enemy1.Create(position,level);
            case CharacterId.Enemy2:return Enemy2.Create(position,level);
            case CharacterId.Enemy3:return Enemy3.Create(position,level);
            case CharacterId.Enemy4:return Enemy4.Create(position,level);
            case CharacterId.Enemy5:return Enemy5.Create(position,level);
            case CharacterId.Enemy6:return Enemy6.Create(position,level);
            default:return null;
        }
    }
    public void OnUpdate()
    {
        mEnemyCreateDefaultTime+=Time.deltaTime;
        mEnemyLevelUpDefaultTime+=Time.deltaTime;
        mBossCreateDefaultTime+=Time.deltaTime;
        
        if(mEnemyCreateDefaultTime>=mEnemyCreateTime)
        {
            mEnemyCreateDefaultTime=0;
            CreateEnemyCircle();
        }
        if(mEnemyLevelUpDefaultTime>=mEnemyLevelUpTime)
        {
            mEnemyLevelUpDefaultTime=0;
            mEnemyCreateTime=Mathf.Clamp(mEnemyCreateTime-0.1f,0.8f,mEnemyCreateTime);
            mNormalLevel++;
        }
         if(mBossCreateDefaultTime>=mBossCreateTime)
        {
            mBossCreateDefaultTime=0;
            int randomInt=RandomHelper.RandomInt(0,2);
            if(randomInt==0)
            {
                Boss boss=Boss.Create(GetEnemyValidPosition(),mBossLevel);
                mEnemies.Add(boss);
                mBossLevel++;
            }
            else
            {
                Boss2 boss=Boss2.Create(GetEnemyValidPosition(),mBossLevel);
                mEnemies.Add(boss);
                mBossLevel++;
            }
        }
    }

    private void CreateEnemyCircle()
    {
        float number=10;
        float angleStep = 360/number;
        int offsetAngle=RandomHelper.RandomInt(-30,30);
         for (int i = 0; i < number; i++)
        {
            // 计算当前敌人的角度
            float angle = i * angleStep;
            // 计算敌人的位置
            Vector3 enemyPosition = CalculatePosition(angle+offsetAngle);
            EnemyCreate(enemyPosition);
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
}