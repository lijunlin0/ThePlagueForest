using System;
using LitJson;
using UnityEngine;

public class CharacterPropertySheet
{
    private static JsonData Config;
    public static void Init()
    {
        if(Config == null)
        {
            TextAsset configText=Resources.Load<TextAsset>("Config/CharacterConfig");
            Config=JsonMapper.ToObject(configText.text);
        }
    }
    //根据角色名字生成属性配置表
    public static PropertySheet GetBasePropertySheet(String characterName,int level)
    {
        JsonData characterData=Config[characterName];
        PropertySheet sheet=new PropertySheet();
        sheet.SetRawValue(Property.BaseHealth,(int)characterData["Health"][0]+(int)characterData["Health"][1]*level-1);
        sheet.SetRawValue(Property.HealthRecoveryRate,(float)characterData["HealthRecovery"][0]+(float)characterData["HealthRecovery"][1]*level-1);
        sheet.SetRawValue(Property.BaseMoveSpeed,(int)characterData["MoveSpeed"]);
        return sheet;
    }
}