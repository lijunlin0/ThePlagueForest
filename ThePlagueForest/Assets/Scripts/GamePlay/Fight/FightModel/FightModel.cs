using System.Collections;
using System.Collections.Generic; 
using UnityEngine;
using DG.Tweening;
using Unity.VisualScripting;

public class FightModel
{
    private static FightModel sCurrent;
    //敌人
    private List<Enemy> mEnemyList;
    //玩家
    private Player mPlayer;
    //敌人子弹
    private List<Bullet> mEnemyBulletList;
    //玩家子弹
    private List<Bullet> mPlayerBulletList;
    //芯片以及层数
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
        EnemyCreate();
        Equipment equipment=new Sickle();
        FightSystem.GetEquipment(equipment);
    }
    public void EnemyCreate()
    {
       //Enemy2 enemy1= Enemy2.Create(new Vector3(900,500,-1));
       //Enemy2 enemy2= Enemy2.Create(new Vector3(900,300,-1));
       //Enemy2 enemy3= Enemy2.Create(new Vector3(700,400,-1));
       //Enemy2 enemy4= Enemy2.Create(new Vector3(-300,100,-1));
       //Enemy2 enemy5= Enemy2.Create(new Vector3(-900,200,-1));
       //Enemy2 enemy6= Enemy2.Create(new Vector3(-300,300,-1));
       //Enemy2 enemy7= Enemy2.Create(new Vector3(700,0,-1));
       //Enemy2 enemy8= Enemy2.Create(new Vector3(-300,100,-1));
       //Enemy1 enemy100= Enemy1.Create(new Vector3(200,460,-1));
       //Enemy1 enemy101= Enemy1.Create(new Vector3(-900,200,-1));
       //Enemy1 enemy102= Enemy1.Create(new Vector3(-400,100,-1));
       //Enemy1 enemy103= Enemy1.Create(new Vector3(-200,200,-1));
       //Enemy1 enemy104= Enemy1.Create(new Vector3(100,450,-1));
       //Enemy1 enemy105= Enemy1.Create(new Vector3(-30,900,-1));
        var sequence = DOTween.Sequence();
        sequence.AppendInterval(0.2f).AppendCallback(()=>{mEnemyList.Add(Enemy2.Create(new Vector3(900,500,-1)));});
                sequence.AppendInterval(0.2f).AppendCallback(()=>{mEnemyList.Add(Enemy1.Create(new Vector3(200,460,-1)));});
        sequence.AppendInterval(0.2f).AppendCallback(()=>{mEnemyList.Add(Enemy2.Create(new Vector3(900,300,-1)));});
                sequence.AppendInterval(0.2f).AppendCallback(()=>{mEnemyList.Add(Enemy1.Create(new Vector3(-900,200,-1)));});
        sequence.AppendInterval(0.5f).AppendCallback(()=>{mEnemyList.Add(Enemy2.Create(new Vector3(700,400,-1)));});
                sequence.AppendInterval(0.5f).AppendCallback(()=>{mEnemyList.Add(Enemy1.Create(new Vector3(-400,100,-1)));});
        sequence.AppendInterval(0.5f).AppendCallback(()=>{mEnemyList.Add(Enemy2.Create(new Vector3(-300,100,-1)));});
                sequence.AppendInterval(0.5f).AppendCallback(()=>{mEnemyList.Add( Enemy1.Create(new Vector3(-200,200,-1)));});
        sequence.AppendInterval(0.5f).AppendCallback(()=>{mEnemyList.Add(Enemy2.Create(new Vector3(-900,200,-1)));});
                sequence.AppendInterval(0.5f).AppendCallback(()=>{mEnemyList.Add(Enemy1.Create(new Vector3(100,450,-1)));});
        sequence.AppendInterval(0.5f).AppendCallback(()=>{mEnemyList.Add(Enemy2.Create(new Vector3(-300,300,-1)));});
                sequence.AppendInterval(0.5f).AppendCallback(()=>{mEnemyList.Add(Enemy1.Create(new Vector3(-30,900,-1)));});
        sequence.AppendInterval(0.5f).AppendCallback(()=>{mEnemyList.Add(Enemy2.Create(new Vector3(700,0,-1)));});
        sequence.AppendInterval(0.5f).AppendCallback(()=>{mEnemyList.Add(Enemy2.Create(new Vector3(-300,100,-1)));});






    }

    public void OnUpdate()
    {
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