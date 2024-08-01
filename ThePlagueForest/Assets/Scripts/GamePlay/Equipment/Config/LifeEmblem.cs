using System.Collections.Generic;

//生命纹章--每 3-(0.25*(layer-1)) 秒恢复 1% 最大生命值
public class LifeEmblem : Equipment
{
    private const int mHealthRecoveryTick=3;
    private const int mHealthRecoveryPercent=1;
    private const float mTickWithLayerReduce=0.25f;
    public LifeEmblem():base(EquipmentType.Passive,EquipmentId.LifeEmblem)
    {
        mMaxlayer=5;
    }
    public override void OnGet(StatusEffect effect,int layer)
    {
        Player player=Player.GetCurrent();
        int points=mHealthRecoveryPercent*player.GetCurrentPropertySheet().GetMaxHealth()/100;
        RecoveryInfo recoveryInfo= new RecoveryInfo(player,player,points);
        effect.SetTick(mHealthRecoveryTick-mTickWithLayerReduce*layer-1,()=>
        {
            FightSystem.Recovery(recoveryInfo);
        });
    }
}