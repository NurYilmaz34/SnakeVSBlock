using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class ForageValueControler : MonoBehaviour
{
    public int forageValue;

    public TextMeshPro forageValueText;

    private void Awake()
    {
        forageValueText = GetComponentInChildren<TextMeshPro>();
        forageValue = Random.Range(1, 5);
    }

    private void Start()
    {
        forageValueText.SetText(forageValue.ToString());
    }
}
