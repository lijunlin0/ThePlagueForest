using System.Collections.Generic;
using TMPro;
using UnityEngine;

//执行战斗逻辑
public static class FightSystem
{

    //造成伤害
    //return: 是否成功造成伤害
    public static bool Damage(DamageInfo damageInfo)
    {
        Character source=damageInfo.GetSource();
        Character target=damageInfo.GetTarget();
        Bullet bullet=damageInfo.GetBullet();
        if(target.GetStatusEffectList().IsInvincible())
        {
            Debug.Log("无敌状态抵挡伤害");
            return false;
        }
        int points=damageInfo.GetPoints();
        //伤害提升
        PropertySheet sourceProcertySheet=source.GetCurrentPropertySheet();
        points=(int)(points*sourceProcertySheet.GetDamageFactor());
        if(points==0)
        {
            return false;
        }
        SetHealth(target, target.GetHealth() - points,-points);
        FightEventDataDamage damageEventData=new FightEventDataDamage(source,target,damageInfo);
        FightEventDispatcher.Dispatch(damageEventData);
        return true;
    }

    //恢复
    //return: 是否成功恢复
    public static bool Recovery(RecoveryInfo recoveryInfo)
    {
        Character target=recoveryInfo.GetTarget();
        int points=recoveryInfo.GetPoints();
        if(points>0)
        {
            SetHealth(target,target.GetHealth()+points,points);
        }
        return true;
    }

    //设置生命值
    //return:生命值是否设置成功
    public static bool SetHealth(Character character,int health,int theoryChangePoints)
    {
        if(character.IsDead())
        {
            return false;
        }
        character.SetHealth(health,theoryChangePoints);
        //如果血量为 0 设置目标死亡
        if(character.GetHealth()<=0)
        {
            character.SetDead();
            FightEventDataDeath dyingEndedEventData=new FightEventDataDeath(character);
            FightEventDispatcher.Dispatch(dyingEndedEventData);
        }
        return true;
    }


    //玩家得到装备
    public static void GetEquipment(Equipment equipment)
    {
        //根据装备获得状态效果Id
        StatusEffectId statusEffectId= equipment.GetStatusEffectsId();
        Player player=Player.GetCurrent();
        StatusEffectList statusEffectList=player.GetStatusEffectList();
        List<StatusEffect> statusEffects=statusEffectList.GetStatusEffectsWithId(statusEffectId);
        Debug.Log("层数:"+(statusEffects.Count+1));
        //给玩家待修改的状态效果
        StatusEffect effect=null;
        if(statusEffects.Count>0)
        {
            effect=statusEffects[0];
        }
        else
        {
            effect=new StatusEffect(statusEffectId,player);
        }
        
        //添加装备
        FightModel.GetCurrent().AddEquipment(equipment);
        //交给装备修改状态效果
        int layer=FightModel.GetCurrent().GetEquipmentLayer(equipment);
        equipment.OnGet(effect,layer);
        StatusEffectChangeInfo changeInfo=new StatusEffectChangeInfo(effect,StatusEffectChangeReason.System,player);
        //为玩家添加状态效果
        AddStatusEffect(changeInfo);

    }

    public static bool AddStatusEffect(StatusEffectChangeInfo changeInfo)
    {
        StatusEffect statusEffect=changeInfo.GetStatusEffect();
        StatusEffectList statusEffectList=statusEffect.GetTarget().GetStatusEffectList();
        List<StatusEffect> statusEffects=statusEffectList.GetStatusEffectsWithId(statusEffect.GetId());
        if(statusEffects.Count>=statusEffect.GetMaxLayer())
        {
            statusEffect.SetElapsedTickDuration(statusEffects[0].GetElapsedTickDuration());
            statusEffectList.RemoveStatusEffect(statusEffects[0]);
        }
        statusEffectList.AddStatusEffect(statusEffect);
        return true;
    }
}