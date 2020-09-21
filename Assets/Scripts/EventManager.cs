using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EventManager : MonoBehaviour
{
    public delegate void PartCall(int ID);
    public delegate void PartFocus(float YROt);
    public static event PartCall onCall;
    public static event PartFocus onFocus;

    public void Call(int ID)
    {
        onCall?.Invoke(ID);
    }

    public void Focus(float YRot)
    {
        onFocus?.Invoke(YRot);
    }
}
