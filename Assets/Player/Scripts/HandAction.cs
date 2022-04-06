using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HandAction : MonoBehaviour
{
    public Transform Hand;
    public float KnockBack;

    private bool CanAttack = true;
    private float AttackCool;

    private Vector3 StartPos;
    private Vector3 StartRotation;

    // Start is called before the first frame update
    void Start()
    {
        StartPos = Hand.localPosition;
        StartRotation = Hand.localEulerAngles;
    }

    // Update is called once per frame
    void Update()
    {
        if (AttackCool > 0)
            AttackCool -= Time.deltaTime;
        else
            CanAttack = true;

        if (Input.GetButton("Swing") && CanAttack == true)
            StartCoroutine(HandAnimation());
    }

    void HitCheck()
    {
        Collider[] Check = Physics.OverlapSphere(Hand.position, 0.8f);
        foreach (Collider col in Check)
        {
            Debug.Log("Hit " + col.gameObject.name);
            if (col.gameObject.tag == "Enemy")
                col.gameObject.GetComponent<Rigidbody>().velocity = (new Vector3(0, 0.5f, 0) + transform.forward) * KnockBack;
        }
    }

    IEnumerator HandAnimation()
    {
        CanAttack = false;
        AttackCool = 1;

        for (int i = 0; i < 20; i++)
        {
            Hand.localPosition += new Vector3(-0.05f, 0, 0.01f);
            Hand.eulerAngles += new Vector3(35 * 0.04f, -40 * 0.04f, 40 * 0.04f);
            yield return new WaitForSeconds(0.005f);
        }

        HitCheck();

        for (int i = 0; i < 20; i++)
        {
            Hand.localPosition += new Vector3(0.05f, 0, -0.01f);
            Hand.eulerAngles -= new Vector3(35 * 0.04f, -40 * 0.04f, 40 * 0.04f);
            yield return new WaitForSeconds(0.005f);
        }
        Hand.localPosition = StartPos;
        Hand.localEulerAngles = StartRotation;
    }

    void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.white;
        Gizmos.DrawWireSphere(Hand.position, 0.8f);
    }
}
