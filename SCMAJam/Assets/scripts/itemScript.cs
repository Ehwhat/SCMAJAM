using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class itemScript : MonoBehaviour {

    List<string> itemsRoster = new List<string>();
    public int whichItem;

	// Use this for initialization
	void Start () {
        itemsRoster.Add("bigDash");
        itemsRoster.Add("quickDash");

	}
	
	// Update is called once per frame
	void Update () {
		
	}

    public string equipThis()
    {
        return itemsRoster[whichItem];

    }
}
