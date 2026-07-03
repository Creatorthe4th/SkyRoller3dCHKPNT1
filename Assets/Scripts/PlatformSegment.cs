using UnityEngine;

// Put this on the ROOT of every platform prefab.
// The generator chains segments end-to-start using these values.
public class PlatformSegment : MonoBehaviour
{
    [Tooltip("Z length of this platform. Used when End Point is not assigned.")]
    public float length = 20f;

    [Tooltip("Optional. Marks the near edge of this platform. Defaults to the root.")]
    public Transform startPoint;

    [Tooltip("Optional. Marks the far edge where the NEXT segment begins. " +
             "Assign this (an empty child) if your pivot isn't at the near edge.")]
    public Transform endPoint;

    Vector3 StartPos => startPoint != null ? startPoint.position : transform.position;

    // Shift this whole segment so its start sits at worldPos.
    public void PlaceStartAt(Vector3 worldPos)
    {
        transform.position += (worldPos - StartPos);
    }

    // Where the next segment should start.
    public Vector3 EndPosition()
    {
        if (endPoint != null) return endPoint.position;
        return StartPos + new Vector3(0f, 0f, length);
    }
}
