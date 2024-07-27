using UnityEngine;
using UnityEngine.UI;

public class EquipmentSelectCell : MonoBehaviour
{
    private Callback mCallback;
    private Equipment mEquipment;
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
        Button button=GetComponent<Button>();
        button.onClick.AddListener(()=>
        {
            callback();
        });
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