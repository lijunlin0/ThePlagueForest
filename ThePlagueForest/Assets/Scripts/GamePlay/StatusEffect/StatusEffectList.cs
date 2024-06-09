using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StatusEffectList
{
    private Character mCharacter;
    private List<StatusEffect> mStatucEffects=new List<StatusEffect>();
    private Callback mOnStatusEffectChanged;
    public StatusEffectList(Character character,Callback onStatusEffectChanged)
    {
        mCharacter = character;
        mOnStatusEffectChanged=onStatusEffectChanged;
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
        mStatucEffects.Add(effect);
        mOnStatusEffectChanged();
    }

}