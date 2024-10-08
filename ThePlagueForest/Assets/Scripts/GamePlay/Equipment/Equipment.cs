using System.Collections.Generic;
using UnityEngine;

public enum EquipmentType
{
    Active=1,   //主动装备
    Passive=2   //被动装备
}

public class Equipment
{
    public const int infiniteLayer=9999;
    protected EquipmentType mEquipmentType;
    protected EquipmentId mEquipmentId;
    protected StatusEffectId mStatusEffectId;
    protected int mMaxlayer;
    public Equipment(EquipmentType equipmentType, EquipmentId equipmentId)
    {
        mEquipmentType = equipmentType;
        mEquipmentId = equipmentId;
        mStatusEffectId=StatusEffectId.None;
        mMaxlayer=infiniteLayer;
    }
    //玩家获得装备
    public virtual void OnGet(StatusEffect statusEffect,int layer){}
    public StatusEffectId GetStatusEffectsId(){return mStatusEffectId;}
    public EquipmentId GetEquipmentId(){return mEquipmentId;}
    public int GetMaxLayer(){return mMaxlayer;}
}