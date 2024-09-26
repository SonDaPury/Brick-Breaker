using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class BrickHealthManager : MonoBehaviour
{
    public int brickHealth;

    [SerializeField]
    private TextMeshProUGUI brickHealthText;

    private void Update()
    {
        brickHealthText.text = " " + brickHealth.ToString();

        if (brickHealth <= 0) { }
    }

    private void TakeDamage(int damageToTake)
    {
        brickHealth -= damageToTake;
    }
}
