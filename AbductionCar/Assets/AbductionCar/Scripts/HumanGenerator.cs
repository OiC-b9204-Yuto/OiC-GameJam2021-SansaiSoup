using AbductionCar.Managers;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HumanGenerator : MonoBehaviour
{
    [SerializeField] GameObject Human;
    List<GameObject> instanceList = new List<GameObject>();

    [SerializeField] private float maxHuman = 5;
    [SerializeField] private float interval = 5;
    private float initInterval;
    private Vector2 minSpawnPoint;
    private Vector2 maxSpawnPoint;
    
    void Start()
    {
        initInterval = interval;
        interval = 0;
        float xPos = transform.position.x;
        float zPos = transform.position.z;
        float xScaleHalf = transform.localScale.x / 2.0f;
        float zScaleHalf = transform.localScale.z / 2.0f;
        minSpawnPoint = new Vector2(xPos - xScaleHalf, zPos - zScaleHalf);
        maxSpawnPoint = new Vector2(xPos + xScaleHalf, zPos + zScaleHalf);
    }
    
    // Update is called once per frame
    void Update()
    {
        if (GameManager.Instance.IsPause || GameManager.Instance.IsEnd)
        {
            return;
        }

        if (GameManager.Instance.IsStart)
        {
            if (instanceList.Count < maxHuman) {

                interval -= Time.deltaTime;
                if (interval <= 0)
                {
                    Generate();
                    interval += initInterval;
                }
            }
            else
            {
                for (int i = instanceList.Count - 1; i >= 0; i--)
                {
                    if (instanceList[i] == null)
                    {
                        instanceList.RemoveAt(i);
                    }
                }
            }
        }
    }
    public void Generate()
    {
        GameObject instance = (GameObject)Instantiate(Human,
        new Vector3(Random.Range (minSpawnPoint.x, maxSpawnPoint.x), transform.position.y, Random.Range(minSpawnPoint.y, maxSpawnPoint.y)),
        Quaternion.identity);
        instanceList.Add(instance);
    }
}
