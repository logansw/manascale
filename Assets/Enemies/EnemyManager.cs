using UnityEngine;
using System.Collections.Generic;

public class EnemyManager : MonoBehaviour
{
    public static EnemyManager Instance;
    [SerializeField] private List<Enemy> _enemyPrefabs;
    private Enemy _currentEnemy;

    void Start()
    {
        LoadNextEnemy(5);
    }

    public void LoadNextEnemy(int MaxMana)
    {
        _currentEnemy = Instantiate(ChooseEnemyType(), transform);
        BattleManager.Instance.CurrentEnemy = _currentEnemy;
        ManaManager.InstanceEnemy.MaxMana = MaxMana;
        ManaManager.InstanceEnemy.ResetMana();
    }

    private Enemy ChooseEnemyType()
    {
        return _enemyPrefabs[Random.Range(0, _enemyPrefabs.Count)];
    }
}