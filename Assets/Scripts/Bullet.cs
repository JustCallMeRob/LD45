using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{

    public float speed = 5f;
    public float lifetime = 3f;

    private float timer = 0f;

    private void FixedUpdate()
    {
        timer += Time.deltaTime;
        if (timer > lifetime)
        {
            Destroy(this.gameObject);
        }
        transform.Translate(transform.up * speed * Time.deltaTime, Space.World);
        // transform.Translate(transform.up * speed * Time.deltaTime);
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        Debug.Log(collision.gameObject.tag);
        if (collision.gameObject.tag == "Block")
        {
            Destroy(collision.gameObject);
            Destroy(gameObject);
        }
    }
}
