using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HitObject : MonoBehaviour
{
    public int Damage;
    // Start is called before the first frame update
    void Start()
    {
        Invoke("End", 1);
        transform.GetChild(0).GetComponent<Text>().text = Damage.ToString();
        GetComponent<Rigidbody>().velocity = (transform.right * Random.Range(-2.00f, 2.00f)) + (transform.up * 3);
    }

    void End()
    {
        Destroy(gameObject);
    }
}
