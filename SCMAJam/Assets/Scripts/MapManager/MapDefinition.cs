using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Levels/New Level Definition", fileName = "New Level Definition")]
public class MapDefinition : ScriptableObject {

    public string name;
    public MapManager mapManager;
    
}
