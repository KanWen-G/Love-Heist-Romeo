using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Patrol : MonoBehaviour
{
    public float speed;
    public Transform[] moveSpots;
    private int randomSpot;
    public float startWaitTime;
    public float waitTime;
    [SerializeField] private FieldOfView fieldOfView;
    private Vector3 lastMoveDir;
    private float viewDistance;
    private float fov;

    // Start is called before the first frame update
    void Start()
    {
        fov = fieldOfView.fov;
        viewDistance = fieldOfView.viewDistance;
        waitTime = startWaitTime;
        randomSpot = Random.Range(0,moveSpots.Length);
    }

    // Update is called once per frame
    void Update()
    {
        fieldOfView.SetOrigin(transform.position);
        transform.position = Vector2.MoveTowards(transform.position, moveSpots[randomSpot].position, speed * Time.deltaTime);
        if (Vector2.Distance(transform.position, moveSpots[randomSpot].position) < 0.2f){
            if(waitTime <= 0){
                randomSpot = Random.Range(0,moveSpots.Length);
                waitTime = startWaitTime;
                fieldOfView.SetAimDirection(GetAimDir());
            } else {
                waitTime -= Time.deltaTime;
            }
        }
    }

    public Vector3 GetPosition() {
        return transform.position;
    }

    public Vector3 GetAimDir() {
        lastMoveDir = new Vector3(Random.Range(-20, 20), Random.Range(-20, 20), 0);
        return lastMoveDir;
    }

}
