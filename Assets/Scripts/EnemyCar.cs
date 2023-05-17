using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyCar : MonoBehaviour
{
    [SerializeField] public float speed;
    private int direction;
    private float minCarDistance = 2f;

    // Start is called before the first frame update
    void Start()
    {
        direction = transform.rotation.z == 0 ? 1 : -1;
    }

    // Update is called once per frame
    void Update()
    {
        transform.position += Vector3.forward * direction* speed * Time.deltaTime;
    }
    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.GetComponent<EnemyCar>())
        {
            speed = collision.gameObject.GetComponent<EnemyCar>().speed;
            transform.position -=new Vector3(0,0, minCarDistance * direction);
        }
    }
}
