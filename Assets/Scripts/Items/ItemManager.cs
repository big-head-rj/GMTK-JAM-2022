using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Singleton;
using TMPro;

public class ItemManager : Singleton<ItemManager>
{
    public TextMeshProUGUI uiTextCoins = null;
    //public TextMeshProUGUI uiTextLife;

    public int coins;
    //public int life;
    public int turbo;

    protected override void Awake()
    {
        base.Awake();
    }

    private void Start()
    {
        coins = 0;
        //life = 0;
    }

    private void Update()
    {
        UpdateUI();
    }

    public void AddCoins(int amount = 1)
    {
        coins += amount;
    }

    public void AddLife(int amount = 1)
    {
        //life += amount;
    }

    public void AddTurbo(int amount = 1)
    {
        turbo += amount;
    }

    public void UpdateUI()
    {
        uiTextCoins.text = "x " + coins;
        //uiTextLife.text = "x " + life;
    }
}
