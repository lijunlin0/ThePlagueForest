
using UnityEngine;
using UnityEngine.UI;

public class Joystick : MonoBehaviour
{
    public  static Joystick Create()
    {
        Canvas canvas=GameObject.Find("UICanvas").GetComponent<Canvas>();
        GameObject prefab=Resources.Load<GameObject>("UI/Joystick");
        GameObject gameObject=GameObject.Instantiate(prefab,canvas.transform);
        Joystick joystick=gameObject.AddComponent<Joystick>();
        return joystick;
    }
}