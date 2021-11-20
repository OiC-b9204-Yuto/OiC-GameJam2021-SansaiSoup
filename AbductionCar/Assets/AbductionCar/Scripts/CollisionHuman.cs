using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CollisionHuman : MonoBehaviour
{
    [SerializeField]
    private GameObject PoliceCar;

    public int count;
    private void OnCollisionEnter(Collision collision)
    {
        //Sphereが衝突したオブジェクトがPlaneだった場合
        if (collision.gameObject.tag=="Human")
        {
            count += 1;
            Debug.Log(count);
            collision.gameObject.SetActive(false);
        }
        if(collision.gameObject.tag=="Police")
        {
            Vector3 pos= this.transform.position;
            pos.x = 5;
            pos.y = 0;
            pos.z = -20;
            this.transform.position = pos;
            Vector3 rot = this.transform.eulerAngles;
            rot.x = 0;
            rot.y = 0;
            rot.z = 0;
            this.transform.eulerAngles = rot;
            this.GetComponent<Rigidbody>().velocity = Vector3.zero;
            this.GetComponent<Rigidbody>().angularVelocity = Vector3.zero;

            Vector3 posPolice = PoliceCar.transform.position;
            posPolice.x = -106;
            posPolice.y = 0;
            posPolice.z = 65;
            PoliceCar.transform.position = posPolice;
            Vector3 rotPolice = PoliceCar.transform.eulerAngles;
            rotPolice.x = 0;
            rotPolice.y = 0;
            rotPolice.z = 0;
            PoliceCar.transform.eulerAngles = rotPolice;
            PoliceCar.GetComponent<Rigidbody>().velocity = Vector3.zero;
            PoliceCar.GetComponent<Rigidbody>().angularVelocity = Vector3.zero;

            count = 0;
        }
    }
}
