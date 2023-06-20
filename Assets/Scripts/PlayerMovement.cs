using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerMovement : MonoBehaviour
{
    private GameObject player;
    public Animator animator;
    [SerializeField] public float currentSpeed = 0;
    public float Speed { get => currentSpeed; set => currentSpeed = value; }
    private Car car;
    private float distanceToMove;

    private float fuelCap = 100f;
    [SerializeField] public float currentFuel;
    public bool canMove = true;
    public bool canSlide = true;
    public AudioSource audioSource;
    // Start is called before the first frame update
    void Start()
    {
        car = GetComponentInChildren<Car>();
        animator = player.GetComponent<Animator>();
        Manager.Instance.stopStart += StopCar;
        animator.SetBool("shake", true);
        audioSource = GetComponent<AudioSource>();
        audioSource.mute = !Manager.Instance.Sound;
    }

    void Awake()
    {
        player = gameObject;
        animator = player.GetComponent<Animator>();
        distanceToMove = player.transform.position.x * 2;
        currentFuel = fuelCap;

    }
    public void StopCar()
    {
        canMove = !canMove;
        audioSource = GetComponent<AudioSource>();
        if (Manager.Instance.Sound) audioSource.mute = canMove ? false : true;
        else audioSource.mute = true;
     //   animator.SetBool("shake", canMove);
    }
    public void Refuel()
    {
        currentFuel = fuelCap;
    }
    void FixedUpdate()
    {
        if (canMove && Manager.Instance.isPlaying) MoveForward();
    }
    public void MoveForward()
    {
        player.transform.position += Vector3.forward * currentSpeed * Time.deltaTime;
        if (currentSpeed < car.maxSpeed && currentFuel > 0)
        {
            currentSpeed += car.speedUp;
        }
        currentFuel -= car.fuelSpeed;
        if (currentFuel <= 0) currentSpeed -= 2*car.speedUp;
        if (currentSpeed <= 0) Manager.Instance.GameOver();
    }
    public void MoveDirection(string direction)
    {
        switch (direction)
        {
            case "left":
                if (!CheckForBorders(-Vector3.right))
                Move("MoveLeft");
                break;
            case "right":
                if (!CheckForBorders(Vector3.right))
                Move("MoveRight");
                break;
        }
    }
    public bool CheckForBorders(Vector3 direction)
    {
        RaycastHit hit;
        if (Physics.Raycast(player.transform.position, 
                           transform.TransformDirection(direction), 
                           out hit, 
                           distanceToMove)) return true;
        return false;
    }
    public void Move(string direction)
    {
        if (canSlide)
        {
            animator.SetBool(direction, true);
            animator.SetFloat("speed", currentSpeed / 20f + 0.5f);
            canSlide = false;
        }
    }
    public void StopMove(string direction)
    {
        animator.SetBool(direction, false);
        canSlide = true ;
    }
}
