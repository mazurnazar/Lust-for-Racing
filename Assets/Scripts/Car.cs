using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class Car : MonoBehaviour
{
    public GameObject player;
    //public Animator animator;
   // [SerializeField]private float speed=0;
    //public float Speed { get => speed; set => speed = value; }

    public float speedUp = .01f;
    public float maxSpeed = 10f;

    //Rigidbody carRigidbody;
    Vector3 moveDirections;

    //float fuelCap = 100f;
    //[SerializeField] float fuel;
    public float fuelSpeed = .1f;

    [SerializeField] SwipeCar moveCar;
   // Vector2 direction;
   // public bool canMove = true;
    //private float distanceToMove;

    [SerializeField] RoadSpawner roadSpawner;
    // Start is called before the first frame update
    void Awake()
    {
         player = transform.parent.gameObject;
       // animator = player.GetComponent<Animator>();
        //distanceToMove =player.transform.position.x * 2;
        //fuel = fuelCap;
       
    }
    public void Refuel()
    {
       // fuel = fuelCap;
    }
    // Update is called once per frame
    void FixedUpdate()
    {
      // if (fuel > 0 && canMove&&Manager.Instance.isPlaying) MoveForward();
       
    }
   /* public void MoveForward()
    {
        player.transform.position += Vector3.forward * speed * Time.deltaTime;
        if (speed < maxSpeed) speed += speedUp;
        fuel -= fuelSpeed;
    }*/
    public void StopMoving()
    {
       // animator.StopPlayback();
    }

    // Add check for borders colliding
   /* public void MoveDirection(string direction)
    {
        switch (direction)
        {
            case "left":
                if (!CheckForBorders(-Vector3.right))
                   StartCoroutine(MoveLeft());
                break;
            case "right":
                if (!CheckForBorders(Vector3.right))
                   StartCoroutine( MoveRight());
                break;
        }
    }*/
    /*public bool CheckForBorders(Vector3 direction)
    {
        RaycastHit hit;
        if (Physics.Raycast(player.transform.position, transform.TransformDirection(direction), out hit, distanceToMove))  return true;
        return false;
    }*/

    // move left
   /* public IEnumerator MoveLeft()
    {
        animator.SetBool("MoveLeft", true);
        yield return new WaitForEndOfFrame();
        animator.SetBool("MoveLeft", false);
    }
    //move right
    public IEnumerator MoveRight()
    {
        animator.SetBool("MoveRight", true);
        yield return new WaitForEndOfFrame();
        animator.SetBool("MoveRight", false);
    }*/
    
   /* public IEnumerator ChangeSpeed(float newSpeed)
    {
        yield return new WaitForSeconds(0f);
        speed = newSpeed;
    }*/
    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.GetComponent<Obstacle>())
        {
            collision.gameObject.GetComponent<Obstacle>().CheckObstacle(player.GetComponent<PlayerMovement>());
        }
    }
    private void OnTriggerEnter(Collider other)
    {
        if(other.gameObject.tag=="Spawn")
        {
            roadSpawner.MoveRoad();
        } 
        else
        other.gameObject.GetComponent<Obstacle>().CheckObstacle(player.GetComponent<PlayerMovement>());
    }
}
