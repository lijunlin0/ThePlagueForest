using System;
using System.Collections;
using System.Collections.Generic;
using System.Runtime.InteropServices;
using DG.Tweening;
using UnityEngine;

public enum CharacterId
{
    Player,
    Enemy1,
    Enemy2,
    Boss,
}

public class Character : FightObject
{
    private CharacterId mCharacterId;
    protected Animator mAnimator;
    protected int mHealth;
    protected StatusEffectList mStatusEffectList;
    protected PropertySheet mBasePropertySheet;
    protected PropertySheet mCurrentPropertySheet;
    protected HealthBar mHealthBar;
    protected SpriteRenderer mSpriteRenderer;
    protected AudioSource mHurtSound;
    protected int mLevel;
    protected virtual void Init(CharacterId characterId,PropertySheet basePropertySheet)
    {

        base.Init();
        mSpriteRenderer=mDisplay.GetComponent<SpriteRenderer>();
        mHurtSound=GetComponent<AudioSource>();
        mCharacterId=characterId;
        mBasePropertySheet=basePropertySheet;
        mStatusEffectList=new StatusEffectList(this,OnStatusEffectChanged);
        mCurrentPropertySheet=new PropertySheet(this,mBasePropertySheet,mStatusEffectList);
        mHealth=mCurrentPropertySheet.GetMaxHealth();
        mAnimator=mDisplay.GetComponent<Animator>();
    }
    public StatusEffectList GetStatusEffectList(){return mStatusEffectList;}
    public PropertySheet GetCurrentPropertySheet(){return mCurrentPropertySheet;}

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
    protected virtual void OnHealthChanged(int theoryChangePoints)
    {
        OnDamage();
        mHealthBar.UpdateContent();
        HealthChangeText healthChangeText=HealthChangeText.Create(theoryChangePoints,this);
    }
    //被攻击
    protected virtual void OnDamage()
    {
        Color color=mSpriteRenderer.color;
        Sequence sequence = DOTween.Sequence();
        sequence.Append(mSpriteRenderer.material.DOColor(Color.red,0.1f));
        sequence.Append(mSpriteRenderer.material.DOColor(color,0.1f));
        sequence.Play();
    }
    public int GetHealth(){return mHealth;}
    public CharacterId GetCharacterId(){return mCharacterId;}
    public void SetDead()
    {
        mIsDead=true;
    }
    public void SetHealth(int health,int theoryChangePoints)
    {
        //Debug.Log("设置的血量: "+health);
        mHealth=Mathf.Clamp(health,0,mCurrentPropertySheet.GetMaxHealth());
        OnHealthChanged(theoryChangePoints);
        //Debug.Log(gameObject.name+":"+health);
    }
    public bool IsPlayer()
    {
        return mCharacterId==CharacterId.Player;
    }
    public bool IsBoss()
    {
        return mCharacterId>=CharacterId.Boss;
    }
    public bool IsEnemy()
    {
        return mCharacterId!=CharacterId.Player;
    }

    public override void OnUpdate()
    {
        base.OnUpdate();
        if(mIsDead)
        {
            return;
        }
        mStatusEffectList.OnUpdate();
    }
    public Animator GetAnimator(){return mAnimator;}
    public  void PlayHurtSound()
    {
        mHurtSound.Play();
    }
}