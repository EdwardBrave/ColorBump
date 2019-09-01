using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CamMotion : MonoBehaviour
{
    public Transform player;

    public float zMotion = 0F;

    public float speed;

    private float startXPos;

    // Start is called before the first frame update
    void Start()
    {
        startXPos = transform.position.x;
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        transform.position = new Vector3(
            startXPos - (startXPos - player.position.x) * zMotion, 
            transform.position.y, 
            transform.position.z + speed * Time.fixedDeltaTime);
    }
}
