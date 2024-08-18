using System;
using System.Collections.Generic;
using DG.Tweening;
using UnityEngine;

public class PlayerLevelController
{
    public const int PlayerBaseExp=100;
    public const int PlayerExpAdd=20;
    public const float LevelExpFactor=1.06f;
    public const int NormalExp=6;
    public const int EliteExp=20;
    public const int BossExp=300;

    private Player mPlayer;
    private int mLevel;
    private float mExp;
    private Dictionary<EnemyType,int>mEnemyExp;
    private ExpBar mExpBar;
    private int mMaxHealthAdditionPoint;
    //连续升级时挨个处理
    private List<int> mLevelUpList=new List<int>();
    public PlayerLevelController()
    {
        Init();
    }

    public void Init()
    {
        mPlayer = Player.GetCurrent();
        mLevel=1;
        mExp=0;
        mEnemyExp=new Dictionary<EnemyType, int>();
        mEnemyExp.Add(EnemyType.Normal,NormalExp);
        mEnemyExp.Add(EnemyType.Elite,EliteExp);
        mEnemyExp.Add(EnemyType.Boss,BossExp);
        mMaxHealthAdditionPoint=CharacterUtility.GetLevelUpMaxHealthAdd("Player1");
        mExpBar=ExpBar.Create(GetlevelUPExp());
    }

    public void AddExp(int exp)
    {
        mExp+=exp;
        float levelUpExp=(PlayerBaseExp+(mLevel-1)*PlayerExpAdd)*Mathf.Pow(LevelExpFactor,mLevel-1);
        mExpBar.OnExpChanged(mExp);
        if(mExp>=levelUpExp)
        {
            mLevel+=1;
            mExp-=levelUpExp;
            OnLevelUp();
            mExpBar.OnLevelUp(GetlevelUPExp());
            mExpBar.OnExpChanged(mExp);
        }
    }

    public float GetlevelUPExp()
    {
        float levelUpExp=(PlayerBaseExp+(mLevel-1)*PlayerExpAdd)*Mathf.Pow(LevelExpFactor,mLevel-1);
        return levelUpExp;
    }

    public void OnLevelUp()
    {
        if(EndWindow.IsOpen())
        {
            return;
        }
        //推入
        mLevelUpList.Add(mLevel);
        HealthChangeText.TextCreate("Level Up!!!",Player.GetCurrent());
        DOVirtual.DelayedCall(0.5f,()=>
        {
            AddMaxHealth();
            LevelUpRecovery();
            GetEquipment();
        });
    }

    public void GetEquipment()
    {
        if(EquipmentSelectWindow.IsOpen())
        {
            return;
        }
        EquipmentUtility.GetAvailableEquipments();
        Callback windowCloseCallback=mLevelUpList.Count>1?()=>
        {
            GetEquipment();
            mLevelUpList.RemoveAt(0);
        }:()=>
        {
            mLevelUpList.Clear();
        };
        EquipmentSelectWindow window = EquipmentSelectWindow.Open(EquipmentUtility.GetAvailableEquipments(),"升级!",windowCloseCallback);
        window.gameObject.SetActive(false);
        DOVirtual.DelayedCall(1,()=>
        {
            window.gameObject.SetActive(true);
        });
    }

    public void LevelUpRecovery()
    {
        //恢复20%血量
        RecoveryInfo recoveryInfo=new RecoveryInfo(mPlayer,mPlayer,mPlayer.GetCurrentPropertySheet().GetMaxHealth()*20/100);
        FightSystem.Recovery(recoveryInfo);
    }

    private void AddMaxHealth()
    {
        StatusEffect statusEffect= new StatusEffect(StatusEffectId.LevelUp,mPlayer);
        Dictionary<Property,float> corrections=new Dictionary<Property,float>();
        corrections.Add(Property.HealthAdditionPoint,mMaxHealthAdditionPoint);
        statusEffect.SetPropertyCorrections(corrections);
        StatusEffectChangeInfo info= new StatusEffectChangeInfo(statusEffect,StatusEffectChangeReason.System,mPlayer);
        FightSystem.AddStatusEffect(info);
        //Debug.Log("玩家最大生命值:"+mPlayer.GetCurrentPropertySheet().GetMaxHealth());
    }

    public static int EnemyTypeToExp(EnemyType enemyType)
    {
        switch(enemyType)
        {
            case EnemyType.Normal:return NormalExp;
            case EnemyType.Elite:return EliteExp;
            case EnemyType.Boss:return BossExp;
            default:return NormalExp;
        }
    }

    public static string EnemyTypeToExpBallName(EnemyType enemyType)
    {
        switch(enemyType)
        {
            case EnemyType.Normal:return "ExpBallNormal";
            case EnemyType.Elite:return "ExpBallElite";
            case EnemyType.Boss:return "ExpBallBoss";
            default:return ""; 
        }
    }
}