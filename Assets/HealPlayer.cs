using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealPlayer : MonoBehaviour
{
    [SerializeField] public float healAmount;

    public static event Action<float> Heal;

    private void OnCollisionEnter2D(Collision2D col)
    {
        if (col.gameObject.CompareTag("Player"))
        {
            Heal?.Invoke(healAmount);
        }
    }
}
