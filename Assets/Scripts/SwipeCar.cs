using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class SwipeCar : MonoBehaviour
{
    [SerializeField] public Text text;
   // [SerializeField] GameObject player;
    [SerializeField] PlayerMovement player;
    private Vector2 firstTouchPos, secondTouchPos, currentSwipe;
    private float timeClick;
    private float minSwipeLength = 200f;
    //[SerializeField] private float speed = 5f;
    Car car;

    private void Start()
    {
        //car = player.GetComponent<Car>();
    }
    private void Update()
    {
         Swipe();
    }

    void Swipe()
    {
       // if (!isMoving) return;

        if (Input.touches.Length > 0)
        {
            Touch t = Input.GetTouch(0);
            if (t.phase == TouchPhase.Began)
            {
                //save began touch 2d point
                firstTouchPos = new Vector2(t.position.x, t.position.y);
                timeClick = Time.time;

            }
            if (t.phase == TouchPhase.Ended)
            {
                //save ended touch 2d point
                secondTouchPos = new Vector2(t.position.x, t.position.y);

                //create vector from the two points
                currentSwipe = new Vector3(secondTouchPos.x - firstTouchPos.x, secondTouchPos.y - firstTouchPos.y);

                if (Time.time - timeClick > 0.1f && currentSwipe.magnitude > minSwipeLength)
                {
                    //normalize the 2d vector
                    currentSwipe.Normalize();
                    if (currentSwipe.y < 0.5f)
                    {
                        if (currentSwipe.x > 0) player.MoveDirection("right");
                        else if (currentSwipe.x < 0) player.MoveDirection("left");
                    }
                }
              
            }
        }
    }

}
