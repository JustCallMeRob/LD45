using System;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    public int diameter = 2;
    public float speed = 2f;
    public float rotationSpeed = 2f;
    public float maxSpeed = 100f;

    public int mass = 10;
    public Rigidbody2D rb;
    public Inventory inventory;
    public GameObject empty;
    public GameObject bullet;
    public bool lockRotate = false;

    private List<GameObject> empties = new List<GameObject>();
    private int radius;
    private int blueBlockCount = 0;
    private List<Vector2> redBlockPositions = new List<Vector2>();
    private bool inEditMode = false;
    private List<Vector2> gridPositions = new List<Vector2>();

    private void OnCollisionEnter2D(Collision2D collision)
    {
        Debug.Log(collision.collider.tag);
    }

    // Start is called before the first frame update
    void Start()
    {
        gridPositions.Clear();
        rb = GetComponent<Rigidbody2D>();
        radius = Mathf.FloorToInt(diameter / 2);
        Debug.Log("Diameter:" + diameter + " Radius:" + radius);
    }

    private void Update()
    {
        Edit();
    }

    private void FixedUpdate()
    {
        RotateRelativeToMouse();
        //RotateRelativeToAccelerometer();
        Move();
        //MovePhone();
        Suck();
    }

    private void RotateRelativeToMouse()
    {
        if (!lockRotate)
        {
            Vector3 mousePos = Input.mousePosition;
            mousePos.z = 5.23f;

            Vector3 objectPos = Camera.main.WorldToScreenPoint(transform.position);
            mousePos.x = mousePos.x - objectPos.x;
            mousePos.y = mousePos.y - objectPos.y;

            float angle = Mathf.Atan2(mousePos.y, mousePos.x) * Mathf.Rad2Deg;
            transform.rotation = Quaternion.Euler(new Vector3(0, 0, angle - 90));
        }
    }

    private void RotateRelativeToAccelerometer()
    {
        if (!lockRotate)
        {
            // copy the rotation of the object itself into a buffer
            Quaternion localRotation = transform.rotation;

            // find speed based on delta
            float curSpeed = Time.deltaTime * rotationSpeed;
            // first update the current rotation angles with input from acceleration axis
            localRotation.z += Input.acceleration.x * curSpeed;

            // then rotate this object accordingly to the new angle
            transform.rotation = localRotation;
        }
    }

    private void Move()
    {
        if (blueBlockCount > 0)
        {
            if (Input.GetKey(KeyCode.W))
            {
                rb.AddForce(transform.up * speed / (mass / 5));
            }
        }
    }

    private void MovePhone()
    {
        foreach (Touch touch in Input.touches)
        {
            if (touch.phase == TouchPhase.Stationary)
            {
                rb.AddForce(transform.up * speed / (mass/5));
            }
        }
    }

    private void Suck()
    {
        if (Input.GetKey(KeyCode.Space))
        {
            GameObject[] blocks = GameObject.FindGameObjectsWithTag("Block");
            foreach (var block in blocks)
            {
                Block blockScript = block.GetComponent<Block>();
                if (!blockScript.attached)
                    blockScript.GetSucked(transform);
            }

            GameObject[] cores = GameObject.FindGameObjectsWithTag("Enemy");
            foreach (var core in cores)
            {
                Enemy enemyScript = core.GetComponent<Enemy>();
                if (enemyScript.type == EnemyType.Small)
                    enemyScript.GetSucked(transform);
            }
        }
    }

    public void AddBlock(Block block)
    {
        BlockItem item = block.item;
        inventory.AddItem(item, 1);
    }

    public void AttachBlock(BlockItem block, RaycastHit2D hit)
    {
        try
        {
            Vector2 blockPosition = hit.collider.gameObject.GetComponent<Empty>().gridPosition;
            if (!gridPositions.Contains(blockPosition))
            {
                GameObject newBlock = Instantiate(block.prefab, this.transform);
                newBlock.GetComponent<Block>().attached = true;
                newBlock.GetComponent<Block>().SetAttached();
                gridPositions.Add(blockPosition);
                newBlock.transform.localPosition = hit.collider.gameObject.transform.localPosition;
                newBlock.transform.localScale = hit.collider.gameObject.transform.localScale;

                Debug.Log(block.type);

                if (block.type == BlockTypes.Blue)
                {
                    blueBlockCount++;
                    if (speed * blueBlockCount > maxSpeed)
                    {
                        speed = maxSpeed;
                    }
                    else
                    {
                        speed *= blueBlockCount;
                    }
                    mass += 10;
                }

                if (block.type == BlockTypes.Red)
                {
                    redBlockPositions.Add(newBlock.transform.position);
                }

                inventory.RemoveItem(block, 1);
            }
            else
            {
                Debug.Log("There is already a block here.");
            }
        }
        catch (Exception e)
        {
            Debug.Log(e);
        }
    }

    private void OnApplicationQuit()
    {
        inventory.Container.Clear();
    }

    private void Edit()
    {
        if (Input.GetKeyDown(KeyCode.E) && !inEditMode)
        {
            inEditMode = true;
            lockRotate = true;
            Time.timeScale = 0.3f;
            if (inEditMode)
            {
                for (int y = -radius; y < diameter - radius; y++)
                {
                    for (int x = -radius; x < diameter - radius; x++)
                    {
                        if (x == 0 && y == 0)
                        {
                            continue;
                        }
                        GameObject current = Instantiate(empty, this.transform);
                        current.GetComponent<Empty>().SetGridPosition(x, y);
                        empties.Add(current);
                        current.transform.localPosition = new Vector3(x / this.transform.localScale.x, y / this.transform.localScale.y, 1f);
                    }
                }
            }
        }
        else if (Input.GetKeyUp(KeyCode.E))
        {
            inEditMode = false;
            lockRotate = false;
            Time.timeScale = 1f;
            foreach (var empty in empties)
            {
                Destroy(empty);
            }
        }
    }

    public void IncrementDiameter()
    {
        diameter++;
        radius = Mathf.FloorToInt(diameter / 2);
    }
}
