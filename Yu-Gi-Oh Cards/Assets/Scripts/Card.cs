using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class Card : MonoBehaviour
{
    public float _attack;
    public float _health;

    public TextMeshProUGUI _attackText;
    public TextMeshProUGUI _healthText;
    void Start()
    {
        _attackText = transform.GetChild(0).GetChild(2).GetChild(1).GetComponent<TextMeshProUGUI>();
        _healthText = transform.GetChild(0).GetChild(3).GetChild(1).GetComponent<TextMeshProUGUI>();

    }
    void Update()
    {
        //  All values on cards
        _attackText.text = _attack.ToString();
        _healthText.text = _health.ToString();

        if (_health <= 0) Destroy(transform.gameObject);
    }
}
