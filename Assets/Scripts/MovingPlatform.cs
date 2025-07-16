using UnityEngine;

public class MovingPlatform : MonoBehaviour
{
    public Transform pointA;
    public Transform pointB;
    public float speed = 2f;
    private bool activated = false;
    private Vector3 target;

    void Start()
    {
        target = pointB.position;
    }

    void Update()
    {
        if (!activated) return;

        transform.position = Vector3.MoveTowards(transform.position, target, speed * Time.deltaTime);

        if (Vector3.Distance(transform.position, target) < 0.1f)
        {
            target = target == pointA.position ? pointB.position : pointA.position;
        }
    }

    public void Activate()
    {
        activated = true;
    }
}
