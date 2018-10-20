using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MapDoor : MonoBehaviour {

    public bool DEBUGTEST = false;

    public Side doorSide;
    public System.Action<Side> onAllPlayersEnter = (side) => { };
    public float radius = 1;

    public void SetUpDoorCallback(System.Action<Side> playersEnterAction)
    {
        onAllPlayersEnter += playersEnterAction;
    }

    public void Update()
    {
        int playerCount = 0;
        TestForPlayers(out playerCount);
    }

    public bool TestForPlayers(out int numberOfPlayersCurrently)
    {
        Collider2D[] colliders = Physics2D.OverlapCircleAll(transform.position, radius);
        numberOfPlayersCurrently = 0;
        for (int i = 0; i < colliders.Length; i++)
        {
            if (DEBUGTEST)
            {
                numberOfPlayersCurrently++;
            }
        }
        if(numberOfPlayersCurrently >= 4 || DEBUGTEST)
        {
            onAllPlayersEnter(doorSide);
            return true;
        }
        return false;
    }

    private void OnDrawGizmos()
    {
        Gizmos.DrawWireSphere(transform.position, radius);
    }

}
