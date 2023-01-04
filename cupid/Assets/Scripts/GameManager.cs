using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public GameObject red;
    public GameObject green;
    public GameObject blue;

    // Start is called before the first frame update
    void Start()
    {
        Instantiate(red, new Vector3(0, 0, 0), Quaternion.identity);
        Instantiate(green, new Vector3(0, 1, 0), Quaternion.identity);
        Instantiate(blue, new Vector3(0, 2, 0), Quaternion.identity);
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
