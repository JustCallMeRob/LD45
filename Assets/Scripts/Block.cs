using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Block : MonoBehaviour
{
    public BlockTypes type;
    public bool attached;
    public float speed = 1f;
    public BlockItem item;

    private Player player;
    private Rigidbody2D rb;
    private BoxCollider2D bc;
    private Vector2 velocity;
    private float timer = 0f;
    private float timeDelay = 0.2f;

    private void Awake()
    {
        player = FindObjectOfType<Player>();
        bc = GetComponent<BoxCollider2D>();
        rb = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        if (!attached)
        {
            if (type == BlockTypes.Black)
            {
                Black();
            }
            else if (type == BlockTypes.Blue)
            {
                Blue();
            }
            else if (type == BlockTypes.Green)
            {
                Green();
            }
            else if (type == BlockTypes.Red)
            {
                Red();
            }
        }
        else
        {
            if (type == BlockTypes.Red)
            {
                player.Attack(gameObject.transform);
            }
        }
    }

    void Black()
    {

    }

    void Blue()
    {
        timer += Time.deltaTime;
        if (timer >= timeDelay)
        {
            float chance = Random.Range(0, 3);
            if (chance == 0)
            {
                Vector2 direction = Random.insideUnitCircle.normalized;
                gameObject.GetComponent<Rigidbody2D>().AddForce(direction * 5, ForceMode2D.Impulse);
            }
            timer = 0f;
        }
    }

    void Green()
    {

    }

    void Red()
    {
        timer += Time.deltaTime;
        if (timer >= timeDelay)
        {
            float chance = Random.Range(0, 3);
            if (chance == 0)
            {
                Vector2 direction = Random.insideUnitCircle.normalized;
                gameObject.GetComponent<Rigidbody2D>().AddForce(direction * 1, ForceMode2D.Impulse);
            }
            timer = 0f;
        }
    }

    public void SetAttached()
    {
        if (attached)
        {
            rb.isKinematic = true;
            gameObject.tag = "Attached";
            player.AddBlock(this);
        }
    }

    public void GetSucked(Transform target)
    {
        float distance = Vector3.Distance(transform.position, target.position);
        Vector3 direction = target.position - transform.position;
        direction.Normalize();
        gameObject.GetComponent<Rigidbody2D>().AddForce((direction * 50) / distance);

        // Check if we have reached target
        if (distance < 1.5f)
        {
            player.AddBlock(this);
            Destroy(gameObject);
        }
    }
}
