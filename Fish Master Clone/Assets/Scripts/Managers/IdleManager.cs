using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class IdleManager : MonoBehaviour
{

    [HideInInspector]
    public int length;
    [HideInInspector]
    public int strength;
    [HideInInspector]
    public int offlineEarnings;
    [HideInInspector]
    public int lengthCost;
    [HideInInspector]
    public int strengthCost;
    [HideInInspector]
    public int offlineEarningsCost;
    [HideInInspector]
    public int wallet;
    [HideInInspector]
    public int totalEarn;

    private int[] costs = new int[]
    {
        455,
        577,
        607,
        665,
        784,
        1249,
        2243,
        2524,
        2962,
        3009,
        3160,
        3507,
        3747,
        3968,
        3987
    };
    public static IdleManager instance;
    private void Awake()
    {
        if (IdleManager.instance)
        {
            UnityEngine.Object.Destroy(gameObject);
        }
        else
        {
            IdleManager.instance = this;
        }
        length = -PlayerPrefs.GetInt("Length", 30);
        strength = PlayerPrefs.GetInt("Strength", 3);
        offlineEarnings = PlayerPrefs.GetInt("Offline", 3);
        lengthCost = costs[-length / 10 - 3];
        strengthCost = costs[strength - 3];
        offlineEarningsCost = costs[offlineEarnings - 3];
        wallet = PlayerPrefs.GetInt("Wallet", 0);

    }
    
    private void OnApplicationPause(bool paused)
    {
        if (paused)
        {
            DateTime now = DateTime.Now;
            PlayerPrefs.SetString("Date", now.ToString());
            MonoBehaviour.print(now.ToString());
        }
        else
        {
            string @string = PlayerPrefs.GetString("Date", string.Empty);
            if (@string!=string.Empty)
            {
                DateTime d = DateTime.Parse(@string);
                totalEarn=(int)((DateTime.Now- d ).TotalMinutes*offlineEarnings+1.0);
                ScreenManager.instance.ChangeScreen(Screens.RETURN);
            }
        }
    }
    private void OnApplicationQuit()
    {
        OnApplicationPause(true);
    }
   
    // Update is called once per frame
    void Update()
    {
        
    }
    public void BuyLength()
    {
        length -= 10;
        wallet -= lengthCost;
        lengthCost = costs[-length / 10 - 3];
        PlayerPrefs.SetInt("Length", -length);
        PlayerPrefs.SetInt("Wallet", wallet);
        ScreenManager.instance.ChangeScreen(Screens.MAIN);
    }

    public void BuyStrength()
    {
        strength++;
        wallet -= strengthCost;
        strengthCost = costs[strength - 3];
        PlayerPrefs.SetInt("Strength", strength);
        PlayerPrefs.SetInt("Wallet", wallet);
        ScreenManager.instance.ChangeScreen(Screens.MAIN);
    }

    public void BuyOfflineEarnings()
    {
        offlineEarnings++;
        wallet -= offlineEarningsCost;
        strengthCost = costs[offlineEarnings - 3];
        PlayerPrefs.SetInt("Offline", offlineEarnings);
        PlayerPrefs.SetInt("Wallet", wallet);
        ScreenManager.instance.ChangeScreen(Screens.MAIN);
    }

    public void CollectMoney()
    {
        wallet += totalEarn;
        PlayerPrefs.SetInt("Wallet", wallet);
        ScreenManager.instance.ChangeScreen(Screens.MAIN);
    }
    public void CollectDoubleMoney()
    {
        wallet += totalEarn * 2;
        PlayerPrefs.SetInt("Wallet", wallet);
        ScreenManager.instance.ChangeScreen(Screens.MAIN);
    }
}
