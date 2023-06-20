using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyCar : MonoBehaviour
{
    [SerializeField] float speed = 2;
    private int direction;
    [SerializeField] GameObject player;
    bool canMove = true;
    // Start is called before the first frame update
    void Start()
    {
        direction = transform.parent.rotation.y == 0 ? -1 : 1;
        player = GameObject.Find("Player");
        Manager.Instance.stopStart += StopCar;
    }

    // Update is called once per frame
    void Update()
    {
        if (canMove)
        {
            transform.position += Vector3.forward * direction * speed * Time.deltaTime;
        }
        if (transform.position.z < player.transform.position.z) Destroy(gameObject);

    }
    public void StopCar()
    {
        canMove = !canMove;
    }
}
