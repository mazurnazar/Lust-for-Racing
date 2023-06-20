using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class Car : MonoBehaviour
{
    [SerializeField] PlayerMovement player;
    public float speedUp = .01f;
    public float maxSpeed = 10f;
    public float fuelSpeed = 10f;

    [SerializeField] RoadSpawner roadSpawner;
    void Awake()
    {
        player = transform.root.GetComponent<PlayerMovement>();
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.GetComponent<Obstacle>())
        {
            collision.gameObject.GetComponent<Obstacle>().CheckObstacle(player);
        }
    }
    private void OnTriggerEnter(Collider other)
    {
        if(other.gameObject.tag=="Spawn")
        {
            roadSpawner.MoveRoad();
        } 
        else
        other.gameObject.GetComponent<Obstacle>().CheckObstacle(player);
    }
}
