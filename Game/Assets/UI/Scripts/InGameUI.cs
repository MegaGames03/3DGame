using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class InGameUI : MonoBehaviour
{
    public GameObject HitObject;
    public Slider HealthBar;
    public bool UIActive;

    private Transform Target;

    void Awake()
    {
        Target = GameObject.Find("Player").transform;
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        Vector3 direction = Target.position - transform.position;
        if (Mathf.Abs(direction.x) + Mathf.Abs(direction.z) < 15)
        {
            UIActive = true;
            ChangeActive(UIActive);
            float angle = Mathf.Atan2(direction.x, direction.z) * Mathf.Rad2Deg;
            Quaternion newQuaternion = Quaternion.Euler(new Vector3(0, angle, 0));
            transform.rotation = Quaternion.Slerp(transform.rotation, newQuaternion, 1);
        }
        else
        {
            UIActive = false;
            ChangeActive(UIActive);
        }
    }

    void ChangeActive(bool state)
    {
        for (int i = 0; i < transform.childCount; i++)
        {
            transform.GetChild(i).gameObject.SetActive(state);
        }
    }

    public void HitSpawn(float Dam)
    {
        GameObject NewHit = Instantiate(HitObject, transform.GetChild(0));
        NewHit.transform.parent = transform.parent.parent;
        NewHit.GetComponent<HitObject>().Damage = (int)Dam;
    }
}
