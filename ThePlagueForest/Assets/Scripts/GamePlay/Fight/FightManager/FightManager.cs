
using DG.Tweening;
using UnityEngine;

public class FightManager
{
    private static bool mPause;
    private static FightManager sCurrent;
    private PoolManager mPoolManager;
    protected FightModel mFightModel;
    public static FightManager GetCurrent()
    {
        return sCurrent;
    }
    public FightManager()
    {
        DOTween.Init();
        mPause=false;
        sCurrent=this;
        mFightModel=new FightModel();
        mPoolManager=new PoolManager();
    }
    public void OnUpdate()
    {
        if(Player.GetCurrent().IsDead()&&!EndWindow.IsOpen())
        {
            EnemyLevelRestart();
            mPause=true;
            EndWindow.SetOpen(mPause);
            DOVirtual.DelayedCall(2,()=>
            {
                EndWindow.Open();
            });
        }
        if(mPause)
        {
            return;
        }
        mFightModel.OnUpdate();
    }
    public void SetPause(bool pause)
    {
        mPause=pause;
    }
    public static bool IsPause(){return mPause;}
    public void EnemyLevelRestart()
    {
        Enemy.sLevel=1;
        Boss.sBossLevel=1;
        Enemy.sEnemyCreateTime=3;
    }
    public PoolManager GetPoolManager(){return mPoolManager;}

}
