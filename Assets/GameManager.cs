using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using UnityEngine.UI;

public class GameManager : Singleton<GameManager>
{
    [SerializeField] private RectTransform _victoryPanel;
    [SerializeField] private int _currentEnemyMana;

    void Start()
    {
        StartCoroutine(InitializeGame());
    }

    private IEnumerator InitializeGame()
    {
        yield return null;
        ManaManager.InstancePlayer.Initialize();
        BattleManager.Instance.Initialize();
        SpellManager.Instance.Initialize();
        EnemyManager.Instance.Initialize();
        DeckManager.Instance.Initialize();
        ManaManager.InstanceEnemy.Initialize();
        ProgressionManager.Instance.Initialize();
        StateController.Instance.Initialize();
        // SpellDatabase.Instance.Initialize();
        Initialize();
    }

    public override void Initialize()
    {
        EnemyManager.Instance.LoadNextEnemy(_currentEnemyMana);
    }

    void OnEnable()
    {
        BattleManager.e_OnVictory += OnVictory;
    }

    void OnDisable()
    {
        BattleManager.e_OnVictory -= OnVictory;
    }

    public void NextBattle()
    {
        EnemyManager.Instance.LoadNextEnemy(_currentEnemyMana);
        BattleManager.Instance.Reset();
        _victoryPanel.gameObject.SetActive(false);
    }

    public void OnVictory()
    {
        _victoryPanel.gameObject.SetActive(true);
        _currentEnemyMana += 2;
        SpellManager.Instance.DeselectSpell();
    }
}