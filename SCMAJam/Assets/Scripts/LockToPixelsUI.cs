using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[ExecuteInEditMode]
public class LockToPixelsUI : MonoBehaviour {

    [SerializeField]
    private int pixelsPerUnit = 16;

    private void LateUpdate()
    {
        RectTransform rectTransform = GetComponent<RectTransform>();
        Vector3 position = rectTransform.anchoredPosition3D;
        position.x = (Mathf.Round(position.x * pixelsPerUnit) / pixelsPerUnit);
        position.y = (Mathf.Round(position.y * pixelsPerUnit) / pixelsPerUnit);

        rectTransform.anchoredPosition3D = position;
    }
}
