using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum EnemyType
{
    Small,
    Medium,
    Large
}

public class Enemy : MonoBehaviour
{

    public Player player;
    public float timeDelay = 1f;
    public EnemyType type;

    private float timer = 0f;

    private void Awake()
    {
        player = GameObject.FindGameObjectsWithTag("Player")[0].GetComponent<Player>();
    }

    private void FixedUpdate()
    {

        float distanceFromPlayer = Vector3.Distance(transform.position, player.transform.position);
        Vector3 direction = player.transform.position - transform.position;
        direction.Normalize();

        if (distanceFromPlayer < 3f && type == EnemyType.Small)
        {
            gameObject.GetComponent<Rigidbody2D>().AddForce((-direction * 50) / distanceFromPlayer);
        }

        if (type == EnemyType.Medium)
        {
            gameObject.GetComponent<Rigidbody2D>().AddForce((direction * 50) / distanceFromPlayer);
            if (distanceFromPlayer < 1.5f)
            {
                player.gameObject.GetComponent<Player>().IncrementDiameter();
                Destroy(gameObject);
            }
        }

        timer += Time.deltaTime;
        if (timer >= timeDelay)
        {
            float chance = Random.Range(0, 3);
            if (chance == 0)
            {
                direction = Random.insideUnitCircle.normalized;
                gameObject.GetComponent<Rigidbody2D>().AddForce(direction * 5, ForceMode2D.Impulse);
            }
            timer = 0f;
        }
    }

    public void GetSucked(Transform player)
    {
        float distance = Vector3.Distance(transform.position, player.position);
        Vector3 direction = player.position - transform.position;
        direction.Normalize();
        gameObject.GetComponent<Rigidbody2D>().AddForce((direction * 50) / distance);

        // Check if we have reached target
        if (distance < 1.5f)
        {
            player.gameObject.GetComponent<Player>().IncrementDiameter();
            Destroy(gameObject);
        }
    }

}
