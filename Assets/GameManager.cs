using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEditor;
using UnityEngine;
using UnityEngine.UI;

public class GameManager : Singleton<GameManager>
{
    [SerializeField] private RectTransform _victoryPanel;
    [SerializeField] private RectTransform _defeatPanel;
    [SerializeField] private TMP_Text _defeatDescription;
    [SerializeField] private int _currentEnemyMana;
    [SerializeField] private GameObject _rulebook;

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
        ShowRulebook(false);
        ShowRulebook(true);
    }

    void OnEnable()
    {
        BattleManager.e_OnVictory += OnVictory;
        BattleManager.e_OnDefeat += OnDefeat;
    }

    void OnDisable()
    {
        BattleManager.e_OnVictory -= OnVictory;
        BattleManager.e_OnDefeat -= OnDefeat;
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

    public void OnDefeat()
    {
        _defeatPanel.gameObject.SetActive(true);
    }

    public void SetDefeatDescription(string description)
    {
        _defeatDescription.text = description;
    }

    public void ShowRulebook(bool active)
    {
        _rulebook.SetActive(active);
    }
}