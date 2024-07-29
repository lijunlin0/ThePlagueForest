using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Threading;
using DG.Tweening;
using UnityEngine;
using UnityEngine.SceneManagement;

public class FightManager
{
    private bool mOver;
    private static FightManager sCurrent;
    protected FightModel mFightModel;
    public static FightManager GetCurrent()
    {
        return sCurrent;
    }
    public FightManager()
    {
        DOTween.Init();
        mOver=false;
        sCurrent=this;
        mFightModel=new FightModel();
    }
    ~FightManager()
    {
        sCurrent=null;
    }
    public void OnUpdate()
    {
        if(Player.GetCurrent().IsDead()&&!EndWindow.IsOpen())
        {
            mOver=true;
            EndWindow.Open();
        }
        if(mOver)
        {
            return;
        }
        mFightModel.OnUpdate();
    }

}
