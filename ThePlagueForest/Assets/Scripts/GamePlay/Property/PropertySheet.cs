using System.Collections;
using System.Collections.Generic;
using UnityEditor.Playables;
using System;
using UnityEngine;


public enum Property
{
    DamageAddition,             //攻击力提升
    AttackSpeedAddition,        //攻击速度提升
    BaseHealth,                 //基础生命值
    HealthAddition,             //生命值提升
    HealthRecoveryRate,         //生命值恢复速度
    BaseMoveSpeed,              //基础移动速度
    MoveSpeedAddition,          //移动速度提升
    LifeSteal,                  //生命偷取率
    AoeRange                    //范围攻击的范围   
}

public class PropertySheet
{
    public const int CollideDamage=10;
    private Character mCharacter;
    private Dictionary<Property,float> mProperties=new Dictionary<Property,float>();
    public PropertySheet(Character character,PropertySheet mBaseProperty,StatusEffectList statusEffectList)
    {
        mCharacter=character;
        Utility.EnumForeach((Property property)=>
        {
            mProperties[property]=mBaseProperty.GetRawValue(property);
        });
        foreach(StatusEffect statusEffect in statusEffectList.GetStatusEffects())
        {
            Dictionary<Property,float> propertyCorrections=statusEffect.GetPropertyCorrections();
            foreach(KeyValuePair<Property,float> correction in propertyCorrections)
            {
                AddRawValue(correction.Key,correction.Value);
            }
        }

    }
    public PropertySheet()
    {
       Utility.EnumForeach((Property property)=>
       {
        mProperties[property]=0;
       });
    }

    public float GetRawValue(Property property)
    {
        return mProperties[property];
    }
    public void SetRawValue(Property property,float value)
    {
        mProperties[property]=value;
    }
    public void AddRawValue(Property property,float value)
    {
        mProperties[property]+=value;  
    }
    //获取攻击力倍率
    public float GetDamageFactor()
    {
        return (mProperties[Property.DamageAddition]+100)/100;
    }
    //获取攻击速度倍率
    public float GetAttackSpeedFactor()
    {
        return (mProperties[Property.AttackSpeedAddition]+100)/100;
    }
    //获取移动速度
    public float GetMoveSpeed()
    {
        float baseMoveSpeed=mProperties[Property.BaseMoveSpeed];
        float moveSpeedAddition=mProperties[Property.MoveSpeedAddition];
        return baseMoveSpeed*(moveSpeedAddition+100)/100;
    }
    //获取最大生命值
    public int GetMaxHealth()
    {
        float baseHealth=mProperties[Property.BaseHealth];
        float healthAddition=mProperties[Property.HealthAddition];
        return (int)(baseHealth*(healthAddition+100)/100);
    }

    // 获取生命恢复速度
    public float GetHealthRecoveryRate()
    {
        return mProperties[Property.HealthRecoveryRate];
    }

    //获取生命偷取率
    public float GetLifeSteal()
    {
        return mProperties[Property.LifeSteal];
    }

    public float GetAoeRangeFacter()
    {
        return (mProperties[Property.AoeRange]+100)/100;
    }
}
