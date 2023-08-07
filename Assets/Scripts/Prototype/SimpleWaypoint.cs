using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SimpleWaypoint : MonoBehaviour
{
    [SerializeField] private float moveSpeed;
    [SerializeField] private GameObject[] waypoints;
    [SerializeField] private bool enabled = true;
    private int currentIndex = 0;
    private bool toStart = true;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (waypoints.Length == 0 || !enabled) return;

        float step = moveSpeed * Time.deltaTime;
        Vector3 targetPos = waypoints[currentIndex].transform.position;
        transform.position = Vector3.MoveTowards(transform.position, targetPos, step); 
        if(Vector3.Distance(transform.position, targetPos) < 0.01f) {
            if(toStart) {
                toStart = false;
            }
            if(currentIndex + 1 < waypoints.Length) {
                currentIndex += 1;
            }
            else
            {
                currentIndex = 0;
            }
        }
    }

    private void OnDrawGizmos() {
        if (waypoints.Length == 0 || !enabled) return;
        foreach(var waypoint in waypoints) {
            Gizmos.color = Color.green;
            Gizmos.DrawSphere(waypoint.transform.position, 0.5f);
        }
        for(int i = 0; i < waypoints.Length - 1; i++) {
            Gizmos.DrawLine(waypoints[i].transform.position, waypoints[i+1].transform.position);
        }
        Gizmos.DrawLine(waypoints[waypoints.Length-1].transform.position, waypoints[0].transform.position);
        if (toStart) {
            Gizmos.color = Color.red;
            Gizmos.DrawLine(transform.position, waypoints[0].transform.position);
        }

    }
}
