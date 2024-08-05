using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StatusEffectList
{
    private Character mCharacter;
    private List<StatusEffect> mStatucEffects=new List<StatusEffect>();
    private Callback mOnStatusEffectChanged;
    private bool mInvincible;
    public StatusEffectList(Character character,Callback onStatusEffectChanged)
    {
        mCharacter = character;
        mOnStatusEffectChanged=onStatusEffectChanged;
        mInvincible=false;
    }

    public List<StatusEffect> GetStatusEffects()
    {
        List<StatusEffect> effects=new List<StatusEffect>();
        foreach(StatusEffect effect in mStatucEffects)
        {
            if(!effect.IsDead())
            {
                effects.Add(effect);
            }
        }
        return effects;
    }

    public List<StatusEffect> GetStatusEffectsWithId(StatusEffectId id)
    {
        List<StatusEffect> statusEffects=new List<StatusEffect>();
        foreach(StatusEffect effect in mStatucEffects)
        {
            if(effect.GetId() == id&&!effect.IsDead())
            {
                statusEffects.Add(effect);
            }
        }
        return statusEffects;
    } 

    //添加新的状态效果
    public void AddStatusEffect(StatusEffect effect)
    {
        Debug.Log("加");
        mStatucEffects.Add(effect);
        if(effect.GetId()==StatusEffectId.Invincible)
        {
            mInvincible=true;
        }
        mOnStatusEffectChanged();
    }

    public void RemoveStatusEffect(StatusEffect effect)
    {
        if(mStatucEffects.Contains(effect))
        {
            mStatucEffects.Remove(effect);
            mInvincible=false;
            foreach(StatusEffect statusEffect in mStatucEffects)
            {
                if(statusEffect.GetId()==StatusEffectId.Invincible)
                {
                    mInvincible=true;
                }
            }
            mOnStatusEffectChanged();
        }
       
    }

    public bool IsInvincible(){return mInvincible;}

    public void OnUpdate()
    {
        for(int i=0;i<mStatucEffects.Count;)
        {
            StatusEffect effect=mStatucEffects[i];
            if(effect.IsDead())
            {
                RemoveStatusEffect(mStatucEffects[i]);
            }
            else
            {
                effect.OnUpdate();
                i++;
            }
        }
    }
}