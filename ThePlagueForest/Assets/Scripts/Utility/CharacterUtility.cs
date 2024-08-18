using System;
using LitJson;
using Unity.Mathematics;
using UnityEngine;

public class CharacterUtility
{
    private static JsonData Config;
    public static void Init()
    { 
        TextAsset configText=Resources.Load<TextAsset>("Config/CharacterConfig");
        Config=JsonMapper.ToObject(configText.text);
    }
    //根据角色名字生成属性配置表
    public static PropertySheet GetBasePropertySheet(string characterName,int level,bool IsEnemy)
    {
        JsonData characterData=Config[characterName];
        PropertySheet sheet=new PropertySheet();

        int health=(int)characterData["Health"][0];
        int healthAdd=(int)characterData["Health"][1];
        int moveSpeed=(int)characterData["MoveSpeed"][0];

        if(IsEnemy)
        {
            float healthAddition=(float)(double)characterData["Health"][2];
            float moveSpeedAddition=(float)(double)characterData["MoveSpeed"][1];
            float attack=(float)(double)characterData["Attack"][0];
            sheet.SetRawValue(Property.BaseHealth,health+healthAdd*(level-1)+health*healthAddition*(level-1));
            float totalMoveSpeed=Mathf.Clamp(moveSpeed+moveSpeedAddition*(level-1)*moveSpeed,moveSpeed,(float)1.5*moveSpeed);
            sheet.SetRawValue(Property.BaseMoveSpeed,totalMoveSpeed);
            sheet.SetRawValue(Property.Attack,attack);
            if(characterName!="Boss"&&characterName!="Boss2")
            {
                float attackAddition=(float)(double)characterData["Attack"][1];
                sheet.SetRawValue(Property.DamageAddition,attackAddition*attack*(level-1));
            }
            
        }
        else
        {
            sheet.SetRawValue(Property.BaseHealth,health + healthAdd*(level-1));
            sheet.SetRawValue(Property.BaseMoveSpeed,moveSpeed);
        }
        
        float healthRecovery=(float)(double)characterData["HealthRecovery"][0];
        float healthRecoveryAdd=(float)(double)characterData["HealthRecovery"][1];
        sheet.SetRawValue(Property.HealthRecoveryRate,healthRecovery+healthRecoveryAdd*(level-1));

        
        return sheet;
    }
    public static int GetLevelUpMaxHealthAdd(string characterName)
    {
        JsonData characterData=Config[characterName];
        PropertySheet sheet=new PropertySheet();

        return (int)characterData["Health"][1];
    }
}