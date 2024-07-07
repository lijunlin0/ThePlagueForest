using System.Collections.Generic;

//生命纹章--每 3 秒恢复 1% 最大生命值
public class LifeEmblem : Equipment
{
    private const int mHealthRecoveryTick=1;
    private const int mHealthRecoveryPercent=10;
    public LifeEmblem():base(EquipmentType.Passive,EquipmentId.LifeEmblem)
    {

    }
    public override void OnGet(StatusEffect effect,int layer)
    {
        Player player=Player.GetCurrent();
        int points=mHealthRecoveryPercent*player.GetCurrentPropertySheet().GetMaxHealth()/100;
        RecoveryInfo recoveryInfo= new RecoveryInfo(player,player,points);
        effect.SetTick(mHealthRecoveryTick,()=>
        {
            FightSystem.Recovery(recoveryInfo);
        });
    }
}