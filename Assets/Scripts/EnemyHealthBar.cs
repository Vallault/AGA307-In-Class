using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class EnemyHealthBar : GameBehaviour
{
    public TMP_Text nameText;
    public Image healthFill;

    public void SetName(string _name)
    {
        nameText.text = _name;
    }
    public void UpdateHealthBar(int _health, int _maxHealth)
    {
        healthFill.fillAmount = MapTo01(_health, 0, _maxHealth);
    }
}
