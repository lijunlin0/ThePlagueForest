using System.Collections.Generic;

//心石--击杀敌人后最大生命值 +1，最多 +150
public class Heartstone : Equipment
{
    private const int mHealthAddition=1;
    private const int mMaxHealthAddition=150;
    public Heartstone():base(EquipmentType.Passive,EquipmentId.Heartstone)
    {
        mStatusEffectId=StatusEffectId.Equipment_Heartstone;
    }
    public override void OnGet(StatusEffect effect,int layer)
    {
        effect.SetFightEventCallback((FightEventData eventData)=>
        {

            if(eventData.GetFightEvent()!=FightEvent.Death)
            {
                return;
            }
            string mCurrentHealthAdditionStr = effect.GetUserData("HeartStone");
            if(mCurrentHealthAdditionStr==string.Empty)
            {
                effect.SetUserData("HeartStone","0");
            }
            int mCurrentHealthAddition=Utility.StrToInt(mCurrentHealthAdditionStr);
            if(mCurrentHealthAddition<mMaxHealthAddition*layer)
            {
                Player player=Player.GetCurrent();
                Dictionary<Property,float> corrections=new Dictionary<Property,float>();
                corrections.Add(Property.HealthAdditionPoint,mHealthAddition*layer);
                effect.SetPropertyCorrections(corrections);
                effect.SetUserData("HeartStone",(mCurrentHealthAddition+1).ToString());
            }
                    
        });
    }
}