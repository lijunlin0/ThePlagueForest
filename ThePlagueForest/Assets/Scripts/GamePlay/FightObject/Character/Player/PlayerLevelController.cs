using System.Collections.Generic;
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
    private float mExpFactor;
    private Dictionary<EnemyType,int>mEnemyExp;
    Callback mExpChangedCallback;

    public PlayerLevelController()
    {
        Init();
        mPlayer = Player.GetCurrent();
        mLevel=1;
        mExp=0;
        mExpFactor=1;
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
        float levelUpExp=(PlayerBaseExp+mLevel-1*PlayerExpAdd)*Mathf.Pow(LevelExpFactor,mLevel-1);
        if(mExp>=levelUpExp)
        {
            mExp-=levelUpExp;
        }
    }

    public void OnLevelUp()
    {
        
    }

    public void GetEquipment()
    {
        
    }

    public void LevelUpRecovery()
    {
        RecoveryInfo recoveryInfo=new RecoveryInfo(mPlayer,mPlayer,mPlayer.GetCurrentPropertySheet().GetMaxHealth());
        FightSystem.Recovery(recoveryInfo);
    }
}