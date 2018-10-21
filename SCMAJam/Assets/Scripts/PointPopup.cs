using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class PointPopup : MonoBehaviour {

    TextMeshProUGUI text;

    public void SetPoints(int points)
    {
        string pointText = "<color=#f4f189>Points</color> ";
        if(points >= 0)
        {
            pointText += "<color=#8cf489>";
        }
        else
        {
            pointText += "<color=#FE8F92>";
        }
        pointText += points.ToString();
    }
	
}
