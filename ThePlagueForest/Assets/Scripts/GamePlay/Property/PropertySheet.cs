using System.Collections;
using System.Collections.Generic;
using UnityEditor.Playables;
using System;
using UnityEngine;

public enum Property
{
    AttackAddition,             //攻击力提升
    AttackSpeedAddition,        //攻击速度提升
    BaseHealth,                 //基础生命值
    HealthAddition,             //生命值提升
    HealthRecoveryRate,         //生命值恢复速度
    BaseMoveSpeed,              //基础移动速度
    MoveSpeedAddition,          //移动速度提升
    
    
}

public class PropertySheet
{
    private Character mCharacter;
    private Dictionary<Property,double> mProperties=new Dictionary<Property,double>();
    public PropertySheet(Character character,PropertySheet mBaseProperty,StatusEffectList statusEffectList)
    {
        mCharacter=character;
        Utility.EnumForeach((Property property)=>
        {
            mProperties[property]=mBaseProperty.GetRawValue(property);
        });
        foreach(StatusEffect statusEffect in statusEffectList.GetStatusEffects())
        {
            Dictionary<Property,double> propertyCorrections=statusEffect.GetPropertyCorrections();
            foreach(KeyValuePair<Property,double> correction in propertyCorrections)
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

    public double GetRawValue(Property property)
    {
        return mProperties[property];
    }
    public void SetRawValue(Property property,double value)
    {
        mProperties[property]=value;
    }
    public void AddRawValue(Property property,double value)
    {
        mProperties[property]+=value;  
    }
    //获取攻击力倍率
    public double GetAttackFactor()
    {
        return (mProperties[Property.AttackAddition]+100)/100;
    }
    //获取攻击速度倍率
    public double GetAttackSpeedFactor()
    {
        return (mProperties[Property.AttackSpeedAddition]+100)/100;
    }
    //获取移动速度
    public double GetMoveSpeed()
    {
        double baseMoveSpeed=mProperties[Property.BaseMoveSpeed];
        double moveSpeedAddition=mProperties[Property.MoveSpeedAddition];
        return baseMoveSpeed*(moveSpeedAddition+100)/100;
    }
    //获取最大生命值
    public int GetMaxHealth()
    {
        double baseHealth=mProperties[Property.BaseHealth];
        double healthAddition=mProperties[Property.HealthAddition];
        return (int)(baseHealth*(healthAddition+100)/100);
    }
    // 获取生命恢复速度
    public double GetHealthRecoveryRate()
    {
        return mProperties[Property.HealthRecoveryRate];
    }
}
