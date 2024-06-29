using System.Collections.Generic;
using UnityEngine;

public class RecoveryInfo
{
    //来源角色
    private Character mSource;

    //恢复角色
    private Character mTarget;

    //恢复数值
    private int mPoints;


    public RecoveryInfo(Character source, Character target,int points)
    {
        mSource = source;
        mTarget = target;
        
        //初始至少一点回血
        mPoints = Mathf.Max(1,points);
    }

    public Character GetSource(){return mSource;}
    public Character GetTarget(){return mTarget;}
    public int GetPoints(){return mPoints;}
}