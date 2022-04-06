using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerHud : MonoBehaviour
{
    public PlayerStats Stats;
    public Text Health_TXT;
    public Slider HealthBar;

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void FixedUpdate()
    {
        UpdateUI();
    }

    void UpdateUI()
    {
        HealthBar.maxValue = Stats.MaxHealth;
        HealthBar.value = Stats.CurrentHealth;

        float HealthPercent = Stats.CurrentHealth / Stats.MaxHealth * 100;
        Health_TXT.text = Mathf.Round(HealthPercent) + "%";
    }
}
