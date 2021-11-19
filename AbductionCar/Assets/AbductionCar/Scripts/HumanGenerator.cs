using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HumanGenerator : MonoBehaviour
{
    [SerializeField] GameObject Human;
    // Start is called before the first frame update
    void Start()
    {
        
    }
    
    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            Generate();
        };
    }
    public void Generate()
    {
        GameObject instance = (GameObject)Instantiate(Human,
        new Vector3(Random.Range (-10.0f, 10.0f), 0.0f, Random.Range(-10.0f, 10.0f)),
        Quaternion.identity);
    }
}
