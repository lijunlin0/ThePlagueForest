using System.Collections.Generic;
using DG.Tweening;
using UnityEngine;
using UnityEngine.UI;

public class EquipmentSelectWindow : MonoBehaviour 
{
    public static int windowCount=0;
    private List<EquipmentSelectCell> mCells;
    private EquipmentSelectCell mSelectCell;
    private List<Equipment> mEquipments;
    public static EquipmentSelectWindow Open(List<Equipment> equipments)
    {
        Canvas canvas=GameObject.Find("Main Camera/WindowCanvas").GetComponent<Canvas>();
        GameObject prefab=Utility.IsPC?Resources.Load<GameObject>("UI/Equipment Select Window"):Resources.Load<GameObject>("UI/Equipment Select Window Phone");
        GameObject gameObject = GameObject.Instantiate(prefab,canvas.transform);
        EquipmentSelectWindow window=gameObject.AddComponent<EquipmentSelectWindow>();
        window.Init(equipments);
        return window;
    }

    public void Init(List<Equipment> equipments)
    {  
        windowCount++;
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
    }

    private void OnButtonClicked()
    {
        if(mSelectCell==null)
        {
            return;
        }
        //增加装备
        FightSystem.GetEquipment(mSelectCell.GetEquipment());
        Destroy(gameObject);
        //判断是否有多个装备选择窗口
        if(windowCount<=1)
        {
            FightManager.GetCurrent().SetPause(false);
            DOTween.PlayAll();
        }
        windowCount--;
    }

    private void UpdateContent()
    {
        foreach(EquipmentSelectCell cell in mCells)
        {
            cell.SetSelect(cell==mSelectCell);
        }
    }
}