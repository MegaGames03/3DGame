using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SwingSword : MonoBehaviour
{
    public Transform Sword;
    public float KnockBack;

    private PlayerStats Stats;
    private bool CanAttack = true;
    private float AttackCool;
    private Skills skills;

    private Vector3 StartPos;
    private Vector3 StartRotation;

    // Start is called before the first frame update
    void Start()
    {
        StartPos = Sword.localPosition;
        StartRotation = Sword.localEulerAngles;
        Stats = GetComponent<PlayerStats>();
        skills = GetComponent<Skills>();
    }

    // Update is called once per frame
    void Update()
    {
        if (AttackCool > 0)
            AttackCool -= Time.deltaTime;
        else
            CanAttack = true;

        if (Input.GetButton("Swing") && CanAttack == true)
            StartCoroutine(SwordAnimation());
    }

    void HitCheck()
    {
        Collider[] Check = Physics.OverlapSphere(Sword.position + transform.forward * 0.75f, 0.75f);
        foreach (Collider col in Check)
        {
            Debug.Log("Hit " + col.gameObject.name);
            if (col.gameObject.tag == "Enemy")
            {
                col.gameObject.GetComponent<Rigidbody>().velocity = (transform.forward) * KnockBack;
                StartCoroutine(col.gameObject.GetComponent<EnemyStats>().TakeDamage(Stats.GenDamage(false)));
                skills.AddCombatEXP(Stats.Damage);
            }
            else if (col.gameObject.tag == "Resource")
            {
                StartCoroutine(col.gameObject.GetComponent<ResourceStats>().TakeDamage(Stats.GenDamage(true)));
                skills.AddChopingEXP(Stats.HarvestDamage * 0.1f);
            }
        }
    }

    IEnumerator SwordAnimation()
    {
        CanAttack = false;
        AttackCool = 1;

        for (int i = 0; i < 20; i++)
        {
            Sword.localPosition += new Vector3(-0.05f, 0, 0.01f);
            Sword.eulerAngles += new Vector3(35 * 0.04f, -40 * 0.04f, 40 * 0.04f);
            yield return new WaitForSeconds(0.005f);
        }

        HitCheck();

        for (int i = 0; i < 20; i++)
        {
            Sword.localPosition += new Vector3(0.05f, 0, -0.01f);
            Sword.eulerAngles -= new Vector3(35 * 0.04f, -40 * 0.04f, 40 * 0.04f);
            yield return new WaitForSeconds(0.005f);
        }
        Sword.localPosition = StartPos;
        Sword.localEulerAngles = StartRotation;
    }

    void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.white;
        Gizmos.DrawWireSphere(Sword.position + transform.forward * 0.75f, 0.75f);
    }
}
