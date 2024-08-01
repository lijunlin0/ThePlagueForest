using System.Collections.Generic;
using DG.Tweening;
using UnityEngine;

public class PlayerLevelController
{
    public const int PlayerBaseExp=100;
    public const int PlayerExpAdd=20;
    public const float LevelExpFactor=1.05f;
    public const int NormalExp=20;
    public const int EliteExp=60;
    public const int BossExp=500;

    private Player mPlayer;
    private int mLevel;
    private float mExp;
    private Dictionary<EnemyType,int>mEnemyExp;
    Callback mExpChangedCallback;

    public PlayerLevelController()
    {
        Init();
        mPlayer = Player.GetCurrent();
        mLevel=1;
        mExp=0;
    }

    public void Init()
    {
        mEnemyExp=new Dictionary<EnemyType, int>();
        mEnemyExp.Add(EnemyType.Normal,NormalExp);
        mEnemyExp.Add(EnemyType.Elite,EliteExp);
        mEnemyExp.Add(EnemyType.Boss,BossExp);
    }

    public void AddExp(int exp)
    {
        mExp+=exp;
        Debug.Log(mExp);
        float levelUpExp=(PlayerBaseExp+(mLevel-1)*PlayerExpAdd)*Mathf.Pow(LevelExpFactor,mLevel-1);
        if(mExp>=levelUpExp)
        {
            mExp-=levelUpExp;
            OnLevelUp();
        }
    }

    public void OnLevelUp()
    {
        LevelUpRecovery();
        GetEquipment();
    }

    public void GetEquipment()
    {
        EquipmentUtility.GetAvailableEquipments();
        DOVirtual.DelayedCall(1,()=>
        {
            EquipmentSelectWindow.Open(EquipmentUtility.GetAvailableEquipments());
        });
    }

    public void LevelUpRecovery()
    {
        RecoveryInfo recoveryInfo=new RecoveryInfo(mPlayer,mPlayer,mPlayer.GetCurrentPropertySheet().GetMaxHealth());
        FightSystem.Recovery(recoveryInfo);
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
}