using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[ExecuteInEditMode]
public class LockToPixels : MonoBehaviour {

    [SerializeField]
    private int pixelsPerUnit = 16;

    private void LateUpdate()
    {
        Vector3 position = transform.position;
        position.x = (Mathf.Round(position.x * pixelsPerUnit) / pixelsPerUnit);
        position.y = (Mathf.Round(position.y * pixelsPerUnit) / pixelsPerUnit);

        transform.position = position;
    }
}
