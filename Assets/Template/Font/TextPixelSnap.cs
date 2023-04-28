using UnityEngine;
using UnityEditor;

[ExecuteInEditMode]
public class TextPixelSnap : MonoBehaviour
{
    public float pixelsPerUnit = 16f; // Your target Pixels Per Unit value
    void Update() {
        transform.position = new Vector3(
            Mathf.Round(transform.position.x * pixelsPerUnit) / pixelsPerUnit,
            Mathf.Round(transform.position.y * pixelsPerUnit) / pixelsPerUnit,
            transform.position.z
        );
    }
}