using System.Buffers.Text;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//王冠显示
public class CrownObject : MonoBehaviour
{
    public static CrownObject Create()
    {
        GameObject Prefab=Resources.Load<GameObject>("Other/CrownObject");
        GameObject gameObject=Instantiate(Prefab,Player.GetCurrent().transform);
        gameObject.SetActive(false);
        CrownObject crown=gameObject.AddComponent<CrownObject>();
        return crown;
    }
}