using System;
using System.IO;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class EquipmentSelectCell : MonoBehaviour
{
    private Callback mCallback;
    private Equipment mEquipment;
    private Image mIcon;
    public  static EquipmentSelectCell Create(Equipment equipment,GameObject parent,Callback callback)
    {
        GameObject prefab=Resources.Load<GameObject>("UI/Equipment Select Cell");
        GameObject gameObject=GameObject.Instantiate(prefab,parent.transform);
        EquipmentSelectCell cell=gameObject.AddComponent<EquipmentSelectCell>();
        cell.Init(equipment,callback);
        return cell;
    }
    private void Init(Equipment equipment,Callback callback)
    {
        mEquipment=equipment;
        Tuple<string,string,string,string,string,string> equipmentText=EquipmentUtility.GetEquipmentText(mEquipment.GetEquipmentId());
        //加载图标
        GameObject prefab=Resources.Load<GameObject>("UI/Icon/"+equipmentText.Item3);
        GameObject icon=GameObject.Instantiate(prefab,transform.Find("Icon"));
        //按钮点击事件
        Button button=GetComponent<Button>();
        button.onClick.AddListener(()=>
        {
            callback();
        });
        //加载装备名称和描述
        TMP_Text tittleText=transform.Find("TittleText").GetComponent<TMP_Text>();
        TMP_Text contentText=transform.Find("ContentText").GetComponent<TMP_Text>();
        TMP_Text levelUpText=transform.Find("LevelUpText").GetComponent<TMP_Text>();
        TMP_Text levelText=transform.Find("LevelText").GetComponent<TMP_Text>();
        //终极武器
        if(equipment is Weapon&&FightModel.GetCurrent().GetEquipmentLayer(equipment)==equipment.GetMaxLayer()-1)
        {
            tittleText.text=equipmentText.Item5;
            tittleText.color=new Color(1,0,0,1);
            contentText.text=equipmentText.Item6;
        }
        else
        {
            tittleText.text = equipmentText.Item1;
            contentText.text = equipmentText.Item2;
            levelUpText.text="叠加:"+equipmentText.Item4;
            levelText.text="层数:"+FightModel.GetCurrent().GetEquipmentLayer(equipment).ToString()+"/"+equipment.GetMaxLayer().ToString();
        }
        //Debug.Log(tittleText.text.ToString()+contentText.text.ToString());
    }
    public void SetSelect(bool isSelect)
    {
        Image image=transform.Find("Border").GetComponent<Image>();
        image.color=new Color(1,1,1,isSelect ? 1 : 0.15f);
    }
    public Equipment GetEquipment()
    {
        return mEquipment;
    }
}