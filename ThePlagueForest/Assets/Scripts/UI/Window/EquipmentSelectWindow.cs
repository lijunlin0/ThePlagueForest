using System.Collections.Generic;
using DG.Tweening;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class EquipmentSelectWindow : MonoBehaviour 
{
    private List<EquipmentSelectCell> mCells;
    private EquipmentSelectCell mSelectCell;
    private List<Equipment> mEquipments;
    private Callback mCloseCallback;
    private string mTittleTxt="";
    private static bool mIsOpen = false;
    public static EquipmentSelectWindow Open(List<Equipment> equipments,string tittleText="",Callback callback=null)
    {
        Canvas canvas=GameObject.Find("Main Camera/WindowCanvas").GetComponent<Canvas>();
        GameObject prefab=Resources.Load<GameObject>("UI/Equipment Select Window");
        GameObject gameObject = GameObject.Instantiate(prefab,canvas.transform);
        EquipmentSelectWindow window=gameObject.AddComponent<EquipmentSelectWindow>();
        window.Init(equipments,tittleText,callback);
        return window;
    }

    public void Init(List<Equipment> equipments,string tittleText,Callback callback)
    {  
        mCloseCallback=callback;
        mTittleTxt=tittleText;
        mIsOpen=true;
        if(mTittleTxt!="")
        {
            TextMeshProUGUI textUI=transform.Find("Window/Tittle/Text").gameObject.GetComponent<TextMeshProUGUI>();
            textUI.text=mTittleTxt;
        }  
        FightManager.GetCurrent().SetPause(true);
        mCells=new List<EquipmentSelectCell>();
        mEquipments = equipments;
        Button button=transform.Find("Window/Button").GetComponent<Button>();
        button.onClick.AddListener(OnButtonClicked);

        GameObject content=transform.Find("Window/Content").gameObject;
        foreach(Equipment equipment in mEquipments)
        {
            EquipmentSelectCell cell=null;

            cell=EquipmentSelectCell.Create(equipment,content,()=>
            {
                mSelectCell=cell;
                UpdateContent();
            });
            mCells.Add(cell);
        }
        Utility.ForceRebuildLayout(content.transform);
    }

    private void OnButtonClicked()
    {
        if(mSelectCell==null)
        {
            return;
        }
        //增加装备
        FightSystem.GetEquipment(mSelectCell.GetEquipment());
        FightManager.GetCurrent().SetPause(false);
        mIsOpen=false;
        DOTween.PlayAll();
        if(mCloseCallback!=null)
        {
            mCloseCallback();
        }
        Destroy(gameObject);
    }

    private void UpdateContent()
    {
        foreach(EquipmentSelectCell cell in mCells)
        {
            cell.SetSelect(cell==mSelectCell);
        }
    }
    public static bool IsOpen(){return mIsOpen;}
}