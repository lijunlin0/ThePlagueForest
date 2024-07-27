using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class EquipmentSelectWindow : MonoBehaviour 
{
    private List<EquipmentSelectCell> mCells;
    private EquipmentSelectCell mSelectCell;
    private List<Equipment> mEquipments;
    public static EquipmentSelectWindow Open(List<Equipment> equipments)
    {
        Canvas canvas=GameObject.Find("Main Camera/WindowCanvas").GetComponent<Canvas>();
        GameObject prefab=Resources.Load<GameObject>("UI/Equipment Select Window");
        GameObject gameObject = GameObject.Instantiate(prefab,canvas.transform);
        EquipmentSelectWindow window=gameObject.AddComponent<EquipmentSelectWindow>();
        window.Init(equipments);
        return window;
    }

    public void Init(List<Equipment> equipments)
    {  
        Time.timeScale = 0;
        mCells=new List<EquipmentSelectCell>();
        mEquipments = equipments;
        Button button=transform.Find("Button").GetComponent<Button>();
        button.onClick.AddListener(OnButtonClicked);

        GameObject content=transform.Find("Content").gameObject;
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
        GameObject.Destroy(gameObject);
        Time.timeScale=1;
    }

    private void UpdateContent()
    {
        foreach(EquipmentSelectCell cell in mCells)
        {
            cell.SetSelect(cell==mSelectCell);
        }
    }
}