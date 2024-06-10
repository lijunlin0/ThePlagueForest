using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Character : FightObject
{
    protected int mHealth;
    protected StatusEffectList mStatusEffectList;
    protected PropertySheet mBasePropertySheet;
    protected PropertySheet mCurrentPropertySheet;
    protected virtual void Init(PropertySheet basePropertySheet)
    {
        base.Init();
        mBasePropertySheet=basePropertySheet;
        mStatusEffectList=new StatusEffectList(this,OnStatusEffectChanged);
        mCurrentPropertySheet=new PropertySheet(this,mBasePropertySheet,mStatusEffectList);
        mHealth=mCurrentPropertySheet.GetMaxHealth();
    }
    public StatusEffectList GetStatusEffectList(){return mStatusEffectList;}
    //状态效果改变
    private void OnStatusEffectChanged()
    {
        int preMaxHealth=mCurrentPropertySheet.GetMaxHealth();
        mCurrentPropertySheet=new PropertySheet(this,mBasePropertySheet,mStatusEffectList);
        int currMaxHealth=mCurrentPropertySheet.GetMaxHealth();
        int deltaMaxHealth=currMaxHealth-preMaxHealth;
        if(deltaMaxHealth>0)
        {
            mHealth+=deltaMaxHealth;
        }
        else if(mHealth>currMaxHealth)
        {
            mHealth=currMaxHealth;
        }
    }
}
