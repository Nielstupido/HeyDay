using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Missions List")]
public class MissionsHolder : ScriptableObject
{
    public List<MissionsScriptableObj> missions = new List<MissionsScriptableObj>(); 
}
