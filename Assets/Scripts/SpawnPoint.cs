using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnPoint : MonoBehaviour
{

    public List<GameObject> prefab;
    public float timeBetweenSpawns = 1f;

    private float timer = 0f;
    private float x, y;

    // Update is called once per frame
    void Update()
    {
        timer += Time.deltaTime;
        if (timer >= timeBetweenSpawns)
        {
            GameObject current = Instantiate(prefab[Random.Range(0, prefab.Count)]);
            int relativePosition = Random.Range(0, 4);
            switch (relativePosition)
            {
                // right
                case 0:
                    x = Random.Range(-0.1f, 0f);
                    y = Random.Range(-0.1f, 1.1f);
                    break;
                // left
                case 1:
                    x = Random.Range(1f, 1.1f);
                    y = Random.Range(-0.1f, 1.1f);
                    break;
                // up
                case 2:
                    x = Random.Range(-0.1f, 1.1f);
                    y = Random.Range(1f, 1.1f);
                    break;
                // down
                case 3:
                    x = Random.Range(-0.1f, 1.1f);
                    y = Random.Range(-0.1f, 0f);
                    break;
            }
            Vector3 v3Pos = Camera.main.ViewportToWorldPoint(new Vector3(x, y, 11f));
            current.transform.position = v3Pos;
            current.transform.Rotate(0f, 0f, Random.Range(0f, 90f));
            current.GetComponent<Rigidbody2D>().AddTorque(Random.Range(0f, 10f));
            current.GetComponent<Rigidbody2D>().AddForce(new Vector2(Random.Range(0f, 10f), Random.Range(0f, 10f)));
            timer = 0f;
        }
    }
}
