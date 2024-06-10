using System;
using LitJson;
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
    public static PropertySheet GetBasePropertySheet(string characterName,int level)
    {
        JsonData characterData=Config[characterName];
        PropertySheet sheet=new PropertySheet();

        int health=(int)characterData["Health"][0];
        int healthAdd=(int)characterData["Health"][1];
        sheet.SetRawValue(Property.BaseHealth,health + healthAdd*(level-1));
        
        float healthRecovery=(float)(double)characterData["HealthRecovery"][0];
        float healthRecoveryAdd=(float)(double)characterData["HealthRecovery"][1];
        sheet.SetRawValue(Property.HealthRecoveryRate,healthRecovery+healthRecoveryAdd*(level-1));

        int moveSpeed=(int)characterData["MoveSpeed"];
        sheet.SetRawValue(Property.BaseMoveSpeed,moveSpeed);
        return sheet;
    }
}