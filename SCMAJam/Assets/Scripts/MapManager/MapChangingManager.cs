using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum Side
{
    Up,
    Down,
    Left,
    Right
}
public class MapChangingManager : MonoBehaviour {

    public Animator mapAnimator;

    public Transform nextMapHolder;
    public Transform mapHolder;
    public MapDefinitionSet mapSet;

    private bool isLoading = false;
    private MapManager nextToBeLoaded;
    private MapManager currentMap;

    private void Start()
    {
        LoadMap(0, Side.Up, true);
    }

    public void LoadMap(int index, Side side = Side.Up, bool instant = false)
    {
        if (!isLoading)
        {
            isLoading = true;
            nextToBeLoaded = Instantiate(mapSet.mapDefintions[index].mapManager, nextMapHolder);
            if (instant)
            {
                CreateMap();
            }
            else
            {
                mapAnimator.SetTrigger("OnNextMap");
            }
            
        }
    }
    
    public void CreateMap()
    {
        if(nextToBeLoaded == null)
        {
            return;
        }
        Trap[] traps = GameObject.FindObjectsOfType<Trap>();
        for (int i = 0; i < traps.Length; i++)
        {
            traps[i].OnMapStart();
        }
        if (currentMap)
        {
            Destroy(currentMap.gameObject);
        }
        nextToBeLoaded.transform.SetParent(mapHolder, false);
        isLoading = false;
        currentMap = nextToBeLoaded;
        nextToBeLoaded = null;

        PopulateDoors();
        traps = GameObject.FindObjectsOfType<Trap>();
        for (int i = 0; i < traps.Length; i++)
        {
            traps[i].OnMapStart(); 
        }

    }

    public void PopulateDoors()
    {
        for (int i = 0; i < currentMap.doors.Length; i++)
        {
            currentMap.SetDoorCallback(i, (side) => { LoadMap(GetRandomMapIndex(), side); });
        }
    }

    public int GetRandomMapIndex()
    {
        return Random.Range(0, mapSet.mapDefintions.Length - 1);
    }

}
