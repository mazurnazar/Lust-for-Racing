using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    private GameObject player;
    public Animator animator;
    [SerializeField] public float currentSpeed = 0;
    public float Speed { get => currentSpeed; set => currentSpeed = value; }
    private Car car;
    private float distanceToMove;

    private float fuelCap = 100f;
    [SerializeField] float currentFuel;
    public bool canMove = true;
    public bool canSlide = true;
    public AnimationClip oilLeft, oilRight;
    // Start is called before the first frame update
    void Start()
    {
        car = GetComponentInChildren<Car>();
    }

    void Awake()
    {
        player = gameObject;
        animator = player.GetComponent<Animator>();
        distanceToMove = player.transform.position.x * 2;
        currentFuel = fuelCap;

    }
    public void Refuel()
    {
        currentFuel = fuelCap;
    }
    void FixedUpdate()
    {
        if (currentFuel > 0 && canMove && Manager.Instance.isPlaying) MoveForward();

    }
    public void MoveForward()
    {
        player.transform.position += Vector3.forward * currentSpeed * Time.deltaTime;
        if (currentSpeed < car.maxSpeed) currentSpeed +=car.speedUp;
        currentFuel -=car.fuelSpeed;
    }
    public void MoveDirection(string direction)
    {
        switch (direction)
        {
            case "left":
                if (!CheckForBorders(-Vector3.right))
                  //  MoveLeft();
                Move("MoveLeft");
                break;
            case "right":
                if (!CheckForBorders(Vector3.right))
                   // MoveRight();
                Move("MoveRight");
                break;
        }
    }
    public bool CheckForBorders(Vector3 direction)
    {
        RaycastHit hit;
        if (Physics.Raycast(player.transform.position, transform.TransformDirection(direction), out hit, distanceToMove)) return true;
        return false;
    }
    public void Move(string direction)
    {
        animator.SetBool(direction, true);
        animator.SetFloat("speed", currentSpeed / 20f + 0.5f);
        canSlide = false;
    }
    public void StopMove(string direction)
    {
        animator.SetBool(direction, false);
        canSlide = true ;
    }
    /*
    public void MoveLeft()
    {
        animator.SetBool("MoveLeft", true);
        canSlide = false;
    }
    public void StopLeft()
    {
        animator.SetBool("MoveLeft", false);
        canSlide = true;
    }
    public void StopOilLeft()
    {
        animator.SetBool("MoveOilLeft", false);
        canSlide = true;
    }

    //move right
    public void MoveRight()
    {
        animator.SetBool("MoveRight", true);
        canSlide = false;
    }
    public void StopRight()
    {
        animator.SetBool("MoveRight", false);
        canSlide = true;
    }
    public void StopOilRight()
    {
        animator.SetBool("MoveOilRight", false);
        canSlide = true;
    }
    */
}
