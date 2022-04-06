using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyStats : MonoBehaviour
{
    public int MaxHealth;
    public float CurrentHealth;
    public int Damage;
    public float KnockBack;

    private InGameUI HealthUI;

    // Start is called before the first frame update
    void Awake()
    {
        HealthUI = transform.GetChild(0).GetComponent<InGameUI>();
        CurrentHealth = MaxHealth;
        HealthUI.HealthBar.maxValue = MaxHealth;
        HealthUI.HealthBar.value = CurrentHealth;
    }

    public IEnumerator TakeDamage(int dam)
    {
        float healthpass = CurrentHealth - dam;
        for (int i = 0; i < 50; i++)
        {
            CurrentHealth -= dam * 0.02f;
            HealthUI.HealthBar.value = CurrentHealth;
            yield return new WaitForSeconds(0.005f);
        }
        CurrentHealth = healthpass;
    }

    public int GenDamage()
    {
        float Dam = Random.Range(0.8f, Damage * 1.2f);
        Dam = Mathf.Round(Dam);
        return (int)Dam;
    }
}
