using AbductionCar.Managers;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CollisionHuman : MonoBehaviour
{
    [SerializeField]
    private GameObject PoliceCar;
    //運んでいる人数表示用テキスト
    [SerializeField] private Text countText;

    private int score = 0;

    private int count = 0;

    [SerializeField] AudioClip humanSound;
    [SerializeField] AudioClip hideoutSound;

    private void OnTriggerEnter(Collider collider)
    {
        //Sphereが衝突したオブジェクトがPlaneだった場合
        if (collider.gameObject.tag == "Human")
        {
            if (count >= 8)
            {
                return;
            }
            count += 1;
            Debug.Log(count);
            Destroy(collider.gameObject);
            AudioManager.Instance.SE.PlayOneShot(humanSound,0.1f);
        }
        else if (collider.gameObject.tag == "Hideout")
        {
            if (count > 0) {
                AudioManager.Instance.SE.PlayOneShot(hideoutSound, 0.4f);
            }
            if (count == 8)
            {
                score += 100;
            }
            score += count * 100;
            GameManager.Instance.AddScore(score - GameManager.Instance.GetScore());

            count = 0;
            Debug.Log(score);
            Vector3 pos = this.transform.position;
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

        }
        countText.text = "乗車人数  " + count.ToString() + "/8人";
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.tag == "Police")
        {
            Vector3 pos = this.transform.position;
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

            //Vector3 posPolice = PoliceCar.transform.position;
            //posPolice.x = -106;
            //posPolice.y = 0;
            //posPolice.z = 65;
            //PoliceCar.transform.position = posPolice;
            //Vector3 rotPolice = PoliceCar.transform.eulerAngles;
            //rotPolice.x = 0;
            //rotPolice.y = 0;
            //rotPolice.z = 0;
            //PoliceCar.transform.eulerAngles = rotPolice;
            collision.gameObject.GetComponent<Rigidbody>().velocity = Vector3.zero;
            collision.gameObject.GetComponent<Rigidbody>().angularVelocity = Vector3.zero;

            count = 0;
        }
        countText.text = "乗車人数  " + count.ToString() + "/8人";
    }
}
