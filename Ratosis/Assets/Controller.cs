using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Controller : MonoBehaviour
{
    private Vector3 mousePosition;
    public GameObject Food;
    public GameObject Water;

    
    // Start is called before the first frame update
    void Start()
    {
      
    }

    // Update is called once per frame
    void Update()
    {

        mousePosition = Input.mousePosition;
        mousePosition = Camera.main.ScreenToWorldPoint(mousePosition);
        mousePosition.z = 0;


        if (Input.GetMouseButtonDown(0))
        {
            Instantiate(Food, mousePosition, transform.rotation);
        }

        if (Input.GetMouseButtonDown(1))
        {
            Instantiate(Water, mousePosition, transform.rotation);
        }
    }
}
