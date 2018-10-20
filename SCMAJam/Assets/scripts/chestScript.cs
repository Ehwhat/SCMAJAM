using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class chestScript : MonoBehaviour {

    List<string> buffs = new List<string>();
    public string selectedBuff;
    bool hasBuff = true;

	// Use this for initialization
	void Start () {
        buffs.Add("fast");
        buffs.Add("slow");
        buffs.Add("coins");

        selectedBuff = buffs[Random.Range(0, buffs.Count-1)];
    }
	
	// Update is called once per frame
	void Update () {
		
	}

    public string getBuff()
    {
        hasBuff = false;
        return selectedBuff;
    }
}
