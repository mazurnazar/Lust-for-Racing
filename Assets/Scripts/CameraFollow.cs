using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    Camera camera;
    Vector3 offset;
    [SerializeField] GameObject player;
    // Start is called before the first frame update
    void Start()
    {
        camera = GetComponent<Camera>();
        offset = transform.position - player.transform.position;
    }

    // Update is called once per frame
    void Update()
    {
        transform.position = player.transform.position + offset;
    }
}
