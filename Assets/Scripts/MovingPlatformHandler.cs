using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovingPlatformHandler : MonoBehaviour {

    //slerps between two points

    [SerializeField] private Transform start;
    [SerializeField] private Transform end;
    [SerializeField][Range(0, 100)] private float speed;
    private float current;
    private bool movingForward;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update() {
        if (movingForward) {
            current += Time.deltaTime * speed;
        } else {
            current -= Time.deltaTime * speed;
        }
        float p = current / 100;
        if(p >= 1 || p <= 0) {
            movingForward = !movingForward;
        }
        transform.position = Vector3.Slerp(start.transform.position, end.transform.position, p);
    }
}
