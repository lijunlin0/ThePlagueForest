
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
        mPause=false;
        sCurrent=this;
        mFightModel=new FightModel();
        mPoolManager=new PoolManager();
    }
    public void OnUpdate()
    {
        if(Player.GetCurrent().IsDead()&&!EndWindow.IsOpen())
        {
            mPause=true;
            EndWindow.SetOpen(true);
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

    public PoolManager GetPoolManager(){return mPoolManager;}

}
