using System;
using System.Collections;
using System.Collections.Generic;
using System.Threading;
using UnityEngine;

public class FightManager
{
    private static FightManager sCurrent;
    protected FightModel mFightModel;
    public static FightManager GetCurrent()
    {
        return sCurrent;
    }
    public FightManager()
    {
        sCurrent=this;
        mFightModel=new FightModel();
    }
    ~FightManager()
    {
        sCurrent=null;
    }
    public void OnUpdate()
    {
        if(Player.GetCurrent().IsDead())
        {
            return;
        }
        mFightModel.OnUpdate();
    }
}
