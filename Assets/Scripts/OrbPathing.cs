using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OrbPathing : MonoBehaviour
{
    public List<Transform> waypoints;
    [SerializeField] float moveSpeed = 2f;
    int waypointIndex = 0;
    bool readyToMove = false;
    Transform targetTransform;

    // Start is called before the first frame update
    void Start()
    {
        transform.position = waypoints[waypointIndex].transform.position;
        StartCoroutine(Path());
    }

    // Update is called once per frame
    void Update()
    {
		 if(!targetTransform) return;
        if (transform.position != targetTransform.position)
        {
            var targetPosition = targetTransform.position;
            var movementThisFrame = moveSpeed * Time.deltaTime;
            transform.position = Vector2.MoveTowards(transform.position, targetPosition, movementThisFrame);
        }
    }

    IEnumerator Path()
    {
        while (waypointIndex <= waypoints.Count - 1)
        {
            var targetPosition = waypoints[waypointIndex].transform.position;
            var movementThisFrame = moveSpeed * Time.deltaTime;
            transform.position = Vector2.MoveTowards(transform.position, targetPosition, movementThisFrame);
            yield return new WaitUntil(() => readyToMove);
            readyToMove = false;
            waypointIndex++;
        }
    }

    public void MakeOrbReady()
    {
        readyToMove = true;
    }

    public void MarkNextWaypoint(Transform position)
    {
        targetTransform = position;
    }
}
