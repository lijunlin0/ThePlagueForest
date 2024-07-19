using UnityEngine;
using DG.Tweening;

//死亡炸弹--敌人阵亡时爆炸，对周围敌人造成 5% 最大生命值的伤害


public class DeathBomb : Equipment
{
    private const int mBombDamagePercent=5;
    public DeathBomb():base(EquipmentType.Passive,EquipmentId.DeathBomb)
    {

    }

    public override void OnGet(StatusEffect statusEffect, int layer)
    {
        statusEffect.SetFightEventCallback((FightEventData eventData)=>
        {
            if(eventData.GetFightEvent()!=FightEvent.Death)
            {
                return;
            }
            Player player = Player.GetCurrent();
            Character enemy=eventData.GetTarget();
            EffectArea area= EffectArea.CircleWithPositonCreate("BombCircle",enemy.gameObject.transform.position,(Character target)=>
            {
                int points=target.GetCurrentPropertySheet().GetMaxHealth()*mBombDamagePercent/100;
                DamageInfo damageInfo=new DamageInfo(player,target,points,null,statusEffect);
                FightSystem.Damage(damageInfo);
            });
            area.SetCollisionEnabledCallback(()=>
            {
                area.PlayDestroyAnimation(0.15f);
                area.Collide();
            });
        });
        
    }
}