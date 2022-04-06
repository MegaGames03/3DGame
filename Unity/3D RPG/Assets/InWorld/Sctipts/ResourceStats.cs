using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ResourceStats : MonoBehaviour
{
    public int MaxHealth;
    public float CurrentHealth;
    public string ItemAdded;
    public int ItemCount;
    public int GrowTime;
    private InGameUI HealthUI;
    private Crafting crafting;

    // Start is called before the first frame update
    void Awake()
    {
        crafting = GameObject.Find("Crafting").GetComponent<Crafting>();
        HealthUI = transform.GetChild(0).GetComponent<InGameUI>();
        CurrentHealth = MaxHealth;
        HealthUI.HealthBar.maxValue = MaxHealth;
        HealthUI.HealthBar.value = CurrentHealth;
    }

    public IEnumerator TakeDamage(int dam)
    {
        float healthpass = CurrentHealth - dam;
        HealthUI.HitSpawn(dam);
        for (int i = 0; i < 9; i++)
        {
            CurrentHealth -= dam * 0.1f;
            HealthUI.HealthBar.value = CurrentHealth;
            yield return new WaitForSeconds(0.01f);
            if (CurrentHealth <= 0)
            {
                Harvest();
                break;
            }
        }
        CurrentHealth = healthpass;
        HealthUI.HealthBar.value = CurrentHealth;
    }

    public void Harvest()
    {
        GetComponent<Collider>().enabled = false;
        for (int i = 0; i < transform.childCount; i++)
        {
            transform.GetChild(i).gameObject.SetActive(false);
        }
        crafting.AddItem(ItemAdded, ItemCount);
        Invoke("Grow", GrowTime);
    }

    public void Grow()
    {
        GetComponent<Collider>().enabled = false;
        for (int i = 0; i < transform.childCount; i++)
        {
            transform.GetChild(i).gameObject.SetActive(true);
        }

        CurrentHealth = MaxHealth;
    }
}
