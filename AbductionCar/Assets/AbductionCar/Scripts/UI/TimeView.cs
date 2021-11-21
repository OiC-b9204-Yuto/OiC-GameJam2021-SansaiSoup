using AbductionCar.Managers;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(Text))]
public class TimeView : MonoBehaviour
{
    Text text;

    private void Awake()
    {
        text = GetComponent<Text>();
    }

    private void Start()
    {
        text.text = GameManager.Instance.GetTime().ToString();
    }

    void Update()
    {
        text.text = "Žc‚èŽžŠÔ  " + GameManager.Instance.GetTime().ToString("F1").PadLeft(5) + "•b";
    }
}
