using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DamagePlayer : MonoBehaviour
{

    [SerializeField] public float DamageAmount;

    public static event Action<float> DoDamage;

    private void OnCollisionEnter2D(Collision2D col)
    {
        if (col.gameObject.CompareTag("Player"))
        {
            DoDamage?.Invoke(DamageAmount);
        }
    }
}
