using System.Collections.Generic;

public class FightEventDispatcher
{
    public static void Dispatch(FightEventData eventData)
    {
        //玩家
        if(!Player.GetCurrent().IsDead())
        {
            List<StatusEffect> playerStatusEffects=Player.GetCurrent().GetStatusEffectList().GetStatusEffects();
            foreach(StatusEffect effect in playerStatusEffects)
            {
                effect.OnFightEvent(eventData);
            }
        }

        //敌人
        List<Enemy> enemies=FightModel.GetCurrent().GetEnemies();
        foreach(Enemy enemy in enemies)
        {
            if(!enemy.IsDead())
            {
                continue;
            }

            List<StatusEffect> enemyStatusEffects=enemy.GetStatusEffectList().GetStatusEffects();
            foreach(StatusEffect effect in enemyStatusEffects)
            {
                effect.OnFightEvent(eventData);
            }
        }
    }
}