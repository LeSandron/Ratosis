using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RatBehaviour : MonoBehaviour
{
    private GameObject[] foodQuantity;
    public Transform closestFood;
    private float closeDistance;
    private float currentDistance;
    private Transform nearFood;

    private GameObject[] waterQuantity;
    public Transform closestWater;
    private Transform nearWater;

    private int foodCount;
    public GameObject Rato;

    private bool targetFoundF;
    private bool targetFoundW;
    private float timer;
    private float timer2;
    public float speed;
    private float direction;
    
    public float food;
    private float water;
    public Rigidbody2D rb;
    // Start is called before the first frame update
    void Start()
    {
        foodCount = 0;
        targetFoundW = false;
        targetFoundF = false;
        timer = 100f;
        timer2 = 10f;
        speed = 1f;
        food = 1000f;
        water = 1000f;
        rb = GetComponent<Rigidbody2D>();
       
        
    }

    // Update is called once per frame
    void Update()
    {
        //Priority list - Priotise drinking first, then eating, then roaming
        
        timer -= 1f;
        food -= 1f;
        water -= 1f;
        timer2 -= 1f;
        
        if (timer2 <=0f && targetFoundF == true)
        {
            targetFoundF = false;
        }
        if (timer2 <= 0f && targetFoundW == true)
        {
            targetFoundW = false;
        }
        if (Input.GetKeyDown(KeyCode.Space))
        {
            print(targetFoundF);
            print(targetFoundW);
        }
        if (water <= 50)
        {
            seekWater();
            if (targetFoundW == true)
            {
              
                rb.velocity = new Vector2(0, 0);
                transform.position = Vector2.MoveTowards(transform.position, nearWater.position, Time.deltaTime * speed);
                Vector2 direction = nearWater.position - transform.position;
                direction.Normalize();
                float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;
                transform.rotation = Quaternion.Euler(Vector3.forward * (angle + 270f));
            }
            
        }
        if (food <= 50 && targetFoundW == false)
        {
            seekFood();
             if (targetFoundF == true)
            {
               
                rb.velocity = new Vector2(0, 0);
                transform.position = Vector2.MoveTowards(transform.position, nearFood.position, Time.deltaTime * speed);
                Vector2 direction = nearFood.position - transform.position;
                direction.Normalize();
                float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;
                transform.rotation = Quaternion.Euler(Vector3.forward * (angle + 270f));

            }
            
        }


        if (timer <= 0 && targetFoundW == false && targetFoundF == false)
        {
          rb.velocity = new Vector3(Random.Range(-1, 2),Random.Range(-1,2));

          Vector3 Rotation = new Vector3(0, 0, Random.Range(0, 360));
          Quaternion rotation = Quaternion.Euler(Rotation);
          transform.rotation = rotation;
          timer = 50;
        }
            
           
        


        if (foodCount >= 5 && food >= 50 && water >= 50)
        {
            Instantiate(Rato, transform.position, transform.rotation);
            Instantiate(Rato, transform.position, transform.rotation);
            Instantiate(Rato, transform.position, transform.rotation);
            Instantiate(Rato, transform.position, transform.rotation);
            Destroy(gameObject);
        }

        if (food < 0)
        {
            food = 0;
        }
        if (water < 0)
        {
            water = 0;
        }
       
    }
    public Transform seekFood()
    {
        //Locate food
        foodQuantity = GameObject.FindGameObjectsWithTag("Food");
        closeDistance = Mathf.Infinity;
        foreach (GameObject go in foodQuantity)
        {
            currentDistance = Vector3.Distance(transform.position, go.transform.position);
            if (currentDistance < closeDistance)
            {
                closeDistance = currentDistance;
                nearFood = go.transform;
                targetFoundF = true;
            }
        }
        return nearFood;
        //move to food
    }

    public Transform seekWater()
    {
        //locate water
        waterQuantity = GameObject.FindGameObjectsWithTag("Water");
        closeDistance = Mathf.Infinity;
        foreach(GameObject go in waterQuantity)
        {
            currentDistance = Vector3.Distance(transform.position, go.transform.position);
            if (currentDistance < closeDistance)
            {
                closeDistance = currentDistance;
                nearWater = go.transform;
                targetFoundW = true;
            }
        }
        return nearWater;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Water")
        {
            water += 300;
            print(water);
            print(food);
            targetFoundW = false;
        }

        if (collision.gameObject.tag =="Food")
        {
            food += 50;
            print(water);
            print(food);
            targetFoundF = false;
            foodCount += 1;
        }
    }



}
