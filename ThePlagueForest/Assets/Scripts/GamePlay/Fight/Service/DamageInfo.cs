using System.Collections.Generic;
using UnityEngine;

public class DamageInfo
{
    //攻击者
    private Character mSource;

    //受击者
    private Character mTarget;

    //伤害数值
    private int mPoints;

    //造成伤害的子弹
    private Bullet mBullet;

    //造成伤害的状态效果
    private StatusEffect mStatusEffect;

    public DamageInfo(Character source, Character target,int points,Bullet bullet,StatusEffect statusEffect)
    {
        mSource = source;
        mTarget = target;
        
        //初始至少一点伤害
        mPoints = Mathf.Max(1,points);
        mBullet = bullet;
        mStatusEffect = statusEffect;
    }

    public Character GetSource(){return mSource;}
    public Character GetTarget(){return mTarget;}
    public int GetPoints(){return mPoints;}
    public Bullet GetBullet(){return mBullet;}
    public StatusEffect GetStatusEffect(){return mStatusEffect;}

}