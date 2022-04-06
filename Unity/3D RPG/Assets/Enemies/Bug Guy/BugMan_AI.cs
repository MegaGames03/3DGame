using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BugMan_AI : MonoBehaviour
{
    public BugManState bugManState;
    public Transform Target;
    public float AttackRange;
    public float SightRange;
    public float Speed;

    private EnemyStats enemyStats;
    private Animator animator;
    private Rigidbody rb;
    private float cooldown;
    private bool Attacking;

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody>();
        enemyStats = GetComponent<EnemyStats>();
        animator = GetComponent<Animator>();
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        if (Target == null)
        {
            PlayerCheck();
            if (cooldown > 0)
                cooldown -= Time.deltaTime;
            else
                ChangeWander();
        }

        if (bugManState == BugManState.MoveTo)
        {
            MoveTo();
            if (Attacking == false)
                AttackCheck();
        }
    }

    void ChangeWander()
    {
        cooldown = 4;
        int NewState = Random.Range(0, 5);
        if (NewState == 0)
            rb.velocity = new Vector3(0, 0, 0);
        else
        {
            StartCoroutine(RotateAnimate(new Vector3(0, Random.Range(-180, 180), 0)));
        }
    }

    IEnumerator RotateAnimate(Vector3 NewRotatition)
    {
        Vector3 RotateNeeded = transform.eulerAngles - NewRotatition;
        for (int i = 0; i < 50; i++)
        {
            transform.eulerAngles += RotateNeeded * 0.02f;
            rb.velocity = transform.forward;
            yield return new WaitForSeconds(0.01f);
        }
    }

    IEnumerator Attack(GameObject player)
    {
        StartCoroutine(player.GetComponent<PlayerStats>().TakeDamage(enemyStats.GenDamage()));
        player.GetComponent<Rigidbody>().AddForce((transform.forward + new Vector3(0, 0.5f, 0)) * enemyStats.KnockBack * 10);
        Attacking = true;
        Retreat();
        yield return new WaitForSeconds(0.3f);
        Attacking = false;
    }

    void AttackCheck()
    {
        Collider[] Check = Physics.OverlapSphere(transform.GetChild(1).position, AttackRange);
        foreach (Collider col in Check)
        {
            if (col.gameObject.tag == "Player")
                StartCoroutine(Attack(col.gameObject));
        }
    }

    void MoveTo()
    {
        Vector3 direction = Target.position - transform.position;
        float angle = Mathf.Atan2(direction.x, direction.z) * Mathf.Rad2Deg;
        Quaternion newQuaternion = Quaternion.Euler(new Vector3(0, angle, 0));
        transform.rotation = Quaternion.Slerp(transform.rotation, newQuaternion, 4 * Time.deltaTime);

        Vector3 Direction = (transform.forward);
        float MaxSpeed = Speed * 2;

        if (Mathf.Abs(rb.velocity.x) < Mathf.Abs(Direction.x) * Speed && Mathf.Abs(rb.velocity.z) < Mathf.Abs(Direction.z) * Speed)
            rb.velocity = Direction * Speed;

        rb.AddForce(Direction * MaxSpeed);

        if (Mathf.Abs(rb.velocity.x) > MaxSpeed || Mathf.Abs(rb.velocity.z) > MaxSpeed)
        {
            float XZ = Mathf.Abs(rb.velocity.x) + Mathf.Abs(rb.velocity.z);
            float XVelocity = Mathf.Abs(rb.velocity.x) / XZ;
            float ZVelocity = Mathf.Abs(rb.velocity.z) / XZ;

            rb.velocity = new Vector3(Direction.x * XVelocity * MaxSpeed, rb.velocity.y, Direction.z * ZVelocity * MaxSpeed);
        }
        rb.velocity = new Vector3(rb.velocity.x * 0.98f, rb.velocity.y, rb.velocity.z * 0.98f);

        if (Target.position.y - 0.2f < transform.position.y)
            rb.AddForce(new Vector3(0, -Speed * 0.3f, 0));
        else if (Target.position.y + 0.2f > transform.position.y)
            rb.AddForce(new Vector3(0, Speed * 0.3f, 0));

    }

    void Retreat()
    {
        rb.velocity = ((-transform.forward * 2 + new Vector3(0, 1, 0)) * Speed);
    }

    void PlayerCheck()
    {
        Collider[] Check = Physics.OverlapSphere(transform.position, SightRange);
        foreach (Collider col in Check)
        {
            if (col.gameObject.tag == "Player")
            {
                Target = col.transform;
                break;
            }
            else
            {
                Target = null;
            }
        }

        if (Target != null)
            bugManState = BugManState.MoveTo;
        else
            bugManState = BugManState.Wander;
    }

    void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.white;
        Gizmos.DrawWireSphere(transform.position, SightRange);

        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.GetChild(1).position, AttackRange);
    }
}

public enum BugManState { Wander, MoveTo }