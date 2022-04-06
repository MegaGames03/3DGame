using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerStats : MonoBehaviour
{
    public int MaxHealth;
    public float CurrentHealth;

    public int Defence;
    public int Damage;
    public int HarvestDamage;

    // Start is called before the first frame update
    void Start()
    {
        CurrentHealth = MaxHealth;
    }

    public IEnumerator TakeDamage(int dam)
    {
        Debug.Log("Player hit for " + dam + " damage");
        float healthpass = CurrentHealth - dam;
        for (int i = 0; i < 9; i++)
        {
            CurrentHealth -= dam * 0.1f;
            yield return new WaitForSeconds(0.01f);
        }
        CurrentHealth = healthpass;
    }

    public int GenDamage(bool Harvest)
    {
        if (Harvest == false)
        {
            float Dam = Random.Range(0.8f, Damage * 1.2f);
            Dam = Mathf.Round(Dam);
            return (int)Dam;
        }
        else
        {
            float Dam = Random.Range(0.8f * HarvestDamage, HarvestDamage * 1.2f);
            Dam = Mathf.Round(Dam);
            return (int)Dam;
        }
    }
}
