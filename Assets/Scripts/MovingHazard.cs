using UnityEngine;

// Optional: put on the KillObstacle to make it slide side-to-side across
// the platform so the player has to dodge it. Uses localPosition so it
// moves correctly even while parented to a spawned/despawning platform.
public class MovingHazard : MonoBehaviour
{
    public float range = 3f;   // how far it slides left/right
    public float speed = 2f;   // oscillation speed

    Vector3 startLocalPos;

    void Start()
    {
        startLocalPos = transform.localPosition;
    }

    void Update()
    {
        float offset = Mathf.Sin(Time.time * speed) * range;
        transform.localPosition = startLocalPos + new Vector3(offset, 0f, 0f);
    }
}
