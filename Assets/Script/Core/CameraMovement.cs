using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraMovement : MonoBehaviour
{
    public Transform player;
    public Transform enemy;
    public float speed;
    private Vector2 pos;
    private Vector2 vel;
    

    // Update is called once per frame
    void Update()
    {
        pos = (player.position + enemy.position) * 0.5f;
        transform.position = Vector2.Lerp(transform.position, pos, speed * Time.deltaTime);
        transform.position = new Vector3(transform.position.x, transform.position.y, -10);
    }
}
