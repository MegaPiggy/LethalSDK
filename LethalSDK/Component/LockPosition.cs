using UnityEngine;

public class LockPosition : MonoBehaviour
{
    public Vector3 initialPosition;

    void Awake()
    {
        initialPosition = transform.position;
    }
    void Start()
    {
        Destroy(this);
    }
}
