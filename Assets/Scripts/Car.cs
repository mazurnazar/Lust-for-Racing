using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class Car : MonoBehaviour
{
    private float speed=0;
    private float speedUp = .01f;

    Rigidbody carRigidbody;
    Vector3 moveDirections;
    [SerializeField]float fuel = 100f;
    float fuelSpeed = .01f;
    [SerializeField] MoveCar moveCar;
    Vector2 direction;
    public bool canMove = true;
    // Start is called before the first frame update
    void Start()
    {
        direction = -transform.up;
        carRigidbody = GetComponent<Rigidbody>();
        moveDirections = new Vector3(transform.position.x*2, 0, 0);
       // StartCoroutine(Move(-1));
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        if (fuel > 0&&canMove) MoveForward();
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
                StartCoroutine(Move(-1));
                break;
            case "right":
                StartCoroutine(Move(1));
                break;
        }
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
        

        //transform.position -= moveDirections;
    }
    // move right

    // for slowing down and speeding up
    public IEnumerator ChangeSpeed(float newSpeed)
    {
        yield return new WaitForSeconds(0f);
        speed = newSpeed;
    }

}
