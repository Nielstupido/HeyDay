using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MenuObjManager : MonoBehaviour
{
    public delegate void OnObjDestroyed(string parent);
    public static OnObjDestroyed onObjDestroyed;
}
