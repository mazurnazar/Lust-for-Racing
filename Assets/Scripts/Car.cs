using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class Car : MonoBehaviour
{
    private float speed=1;
    public float Speed { get => speed; set => speed = value; }
    private float speedUp = .01f;

    Rigidbody carRigidbody;
    Vector3 moveDirections;
    float fuelCap = 100f;
    [SerializeField] float fuel 
    float fuelSpeed = .01f;
    [SerializeField] MoveCar moveCar;
    Vector2 direction;
    public bool canMove = true;
    private float distanceToMove;
    // Start is called before the first frame update
    void Start()
    {
        distanceToMove = transform.position.x * 2;
        direction = -transform.up;
        carRigidbody = GetComponent<Rigidbody>();
        moveDirections = new Vector3(distanceToMove, 0, 0);
        fuel = fuelCap;
        // StartCoroutine(Move(-1));
       // Debug.Log(CheckForBorders(Vector3.right));
    }
    public void Refuel()
    {
        fuel = fuelCap;
    }
    // Update is called once per frame
    void FixedUpdate()
    {
        if (fuel > 0 && canMove) MoveForward();
        if (fuel > 0 && canMove) MoveForward();
    }
    public void MoveForward()
    {
        //carRigidbody.velocity = -transform.up * speed;
      
            transform.position += -transform.up * speed * Time.deltaTime;
            speed += speedUp;
            fuel -= fuelSpeed;
    }

    // Add check for borders colliding
    public void MoveDirection(string direction)
    {
        switch (direction)
        {
            case "left":
                if(!CheckForBorders(-Vector3.right))
                StartCoroutine(Move(-1));
                break;
            case "right":
                if (!CheckForBorders(Vector3.right))
                    StartCoroutine(Move(1));
                break;
        }
    }
    public bool CheckForBorders(Vector3 direction)
    {
        RaycastHit hit;
        if (Physics.Raycast(transform.position, transform.TransformDirection(direction), out hit, distanceToMove))
        {
            //Debug.Log(hit.collider.name);
            return true;
        }
        return false;
    }
    // move left
    public IEnumerator Move(int direction)
    {
        canMove = false;
        float i = 0;
        Vector3 initPos = transform.position;
        Vector3 newPos = transform.position + moveDirections * direction;

        while (i <= 1)
        {
            transform.position = Vector3.Lerp(initPos, newPos, i);
           
            yield return new WaitForSeconds(.01f);
            i += 0.1f;
        }
        canMove = true;
    }
    public IEnumerator ChangeSpeed(float newSpeed)
    {
        yield return new WaitForSeconds(0f);
        speed = newSpeed;
    }
    private void OnCollisionEnter(Collision collision)
    {
        collision.gameObject.GetComponent<Obstacle>().CheckObstacle(this);
    }
}
