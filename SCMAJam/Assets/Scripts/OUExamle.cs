using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class OUExamle : MonoBehaviour {

    public Text text;
    public CharacterData data;
	
	// Update is called once per frame
	void Update () {
        text.text = data.points.ToString();
	}
}
