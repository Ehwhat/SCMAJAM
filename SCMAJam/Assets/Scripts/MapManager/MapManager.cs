using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MapManager : MonoBehaviour {

    public MapDoor[] doors;

    public void SetDoorCallback(int i, System.Action<Side> callback)
    {
        doors[i].SetUpDoorCallback(callback);
    }

}
