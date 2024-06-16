using System.Collections.Generic;
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
        
        int points=damageInfo.GetPoints();
        //伤害提升
        PropertySheet sourceProcertySheet=source.GetCurrentPropertySheet();
        points=(int)(points*sourceProcertySheet.GetDamageFactor());

        if(points>0)
        {
            SetHealth(target, target.GetHealth() - points);
        }
        return true;
    }



    //正常伤害流程
    private static void NormalDamage(DamageInfo damageInfo,int points,ref int healthRealPoints)
    {
        Character target=damageInfo.GetTarget();
        
    }

    //设置生命值
    //return:生命值是否设置成功
    public static bool SetHealth(Character character,int health)
    {
        if(character.IsDead())
        {
            return false;
        }
        character.SetHealth(health);
        //如果血量为 0 设置目标死亡
        if(character.GetHealth()<=0)
        {
            character.SetDead();
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

        //给玩家待修改的状态效果
        StatusEffect effect=null;
        if(statusEffects.Count>0)
        {
            effect=statusEffects[0];
        }
        else
        {
            new StatusEffect(statusEffectId,player);
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
        statusEffect.GetTarget().GetStatusEffectList().AddStatusEffect(statusEffect);
        return true;
    }
}