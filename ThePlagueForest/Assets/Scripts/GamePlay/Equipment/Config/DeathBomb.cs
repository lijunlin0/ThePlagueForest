using UnityEngine;
using DG.Tweening;

//死亡炸弹--敌人阵亡时爆炸，对周围敌人造成 2% 最大生命值的伤害,boss造成 0.5%


public class DeathBomb : Equipment
{
    private const int mBombDamagePercent=2;
    private const int mRangeAdditionPercent=10;
    public DeathBomb():base(EquipmentType.Passive,EquipmentId.DeathBomb)
    {
        mMaxlayer=3;
        mStatusEffectId=StatusEffectId.Equipment_DeathBomb;
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
            float delayTime=RandomHelper.Randomfloat(0.01f,0.5f);
            DOVirtual.DelayedCall(delayTime,()=>
            {
                EffectArea area= EffectArea.CircleWithPositonCreate("BombCircle",enemy.gameObject.transform.position,(Character target)=>
                {
                    int points=target.GetCurrentPropertySheet().GetMaxHealth()*mBombDamagePercent/100;
                    if(target.IsBoss())
                    {
                        points/=4;
                    }
                    DamageInfo damageInfo=new DamageInfo(player,target,points,null,statusEffect);
                    FightSystem.Damage(damageInfo);
                },1+(layer-1)*mRangeAdditionPercent/100);
                area.SetCollisionEnabledCallback(()=>
                {
                    area.PlayDestroyAnimation(0.25f);
                    area.Collide();
                });
            });

        });
        
    }
}