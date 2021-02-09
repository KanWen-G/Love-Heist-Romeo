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
    private GameObject player;
    

    // Start is called before the first frame update
    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player");
        fov = fieldOfView.fov;
        viewDistance = fieldOfView.viewDistance;
        waitTime = startWaitTime;
        randomSpot = Random.Range(0,moveSpots.Length);
        lastMoveDir = new Vector3(Random.Range(-20, 20), Random.Range(-20, 20), 0);
    }

    // Update is called once per frame
    void Update()
    {
        FindTargetPlayer();
        fieldOfView.SetOrigin(transform.position);
        transform.position = Vector2.MoveTowards(transform.position, moveSpots[randomSpot].position, speed * Time.deltaTime);
        if (Vector2.Distance(transform.position, moveSpots[randomSpot].position) < 0.2f){
            if(waitTime <= 0){
                lastMoveDir = new Vector3(Random.Range(-20, 20), Random.Range(-20, 20), 0);
                randomSpot = Random.Range(0,moveSpots.Length);
                waitTime = startWaitTime;
                fieldOfView.SetAimDirection(lastMoveDir);
            } else {
                waitTime -= Time.deltaTime;
            }
        }


    }

   private void FindTargetPlayer(){
        if (Vector3.Distance(GetPosition(), player.transform.position) < viewDistance) {
            // Player inside viewDistance
            Vector3 dirToPlayer = (player.transform.position - GetPosition()).normalized;
            if (Vector3.Angle(GetAimDir(), dirToPlayer) < fov / 2f) {
                // Player inside Field of View
                RaycastHit2D raycastHit2D = Physics2D.Raycast(GetPosition(), dirToPlayer, viewDistance);
                if (raycastHit2D.collider != null) {
                    // Hit something
                    if (raycastHit2D.collider.gameObject.GetComponent<playerMovement>() != null) {
                        // Hit Player
                        AttackingPlayer();
                    } else {
                        // Hit something else
                    }
                }
            }
        }
    }

    public void AttackingPlayer() {
        Debug.Log("Hello:  I see you");
    }

    public Vector3 GetPosition() {
        return transform.position;
    }

    public Vector3 GetAimDir() {
        return lastMoveDir;
    }

}
