using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HealthDisplay : MonoBehaviour
{
    private Text _healthText;
    private Player player;

    // Start is called before the first frame update
    void Start()
    {
        _healthText = GetComponent<Text>();
        player = FindObjectOfType<Player>();
    }

    // Update is called once per frame
    void Update()
    {
        _healthText.text = player.Health.ToString();
    }
}
