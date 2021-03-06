﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance;
    public float StartingFunds = 100f;
    public float DailyPay = 50f;
    public int WinDay = 8;
    public float AOE = 1f;
    public float MinStressFromWork;
    public float MaxStressFromWork;
    public float MinStressFromSleep; 
    public float MaxStressFromSleep;
    public float InitialStress;
    public List<HouseplantData> Plants;
    public SoundAudioClip[] AllGameSounds;
    public bool GameIsGoing;
    public float CarpetLight = 30f;
    public GameState CurrentState = GameState.tutorial;
    public bool UIPanelUp = false;

    private void Awake()
    {
        Instance = this;
        PlayerPrefs.SetFloat("Funds", StartingFunds);
        PlayerPrefs.SetFloat("Stress", InitialStress);
        UnpauseGame();
    }

    void Update()
    {
        if (Input.GetKeyDown("escape"))
        {
            Events.OpenMenu();
        }
    }

    public float GetFunds()
    {
        return PlayerPrefs.GetFloat("Funds");
    }

    public void UpdateFunds(float amount)
    {
        float newAmount = GetFunds() + amount;
        PlayerPrefs.SetFloat("Funds", newAmount);
        Events.UpdateFunds((int)newAmount);
    }

    public float GetStress()
    {
        return PlayerPrefs.GetFloat("Stress");
    }

    public void UpdateStress(float amount)
    {
        PlayerPrefs.SetFloat("Stress", Mathf.Clamp(GetStress() + amount, 0, 100));
        Events.UpdateStress();
    }

    public void AddDailySalary()
    {
        UpdateFunds(DailyPay);
    }

    public void StartGame()
    {
        CurrentState = GameState.game;
        UnpauseGame();
    }

    public void PauseGame()
    {
        GameIsGoing = false;
        Time.timeScale = 0;
    }

    public void UnpauseGame()
    {
        if (CurrentState == GameState.game)
        {
            GameIsGoing = true;
        }
        Time.timeScale = 1;
    }

    public void EndGame()
    {
        CurrentState = GameState.gameOver;
        PauseGame();
    }

    [System.Serializable]
    public class SoundAudioClip
    {
        public SoundManager.Sound sound;
        public AudioMixerGroup mixerGroup;
        public AudioClip Clip;
    }

    public enum GameState
    {
        tutorial, game, gameOver
    }
}
