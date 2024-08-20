using UnityEngine;
using System.Collections.Generic;

public class EnemyManager : Singleton<EnemyManager>
{
    [SerializeField] private List<Enemy> _enemyPrefabs;
    public Enemy CurrentEnemy;
    [SerializeField] private EnemyIntent _enemyIntent;

    public override void Initialize()
    {
        // Do nothing
    }

    public void LoadNextEnemy(int MaxMana)
    {
        if (CurrentEnemy != null)
        {
            Destroy(CurrentEnemy.gameObject);
        }
        ManaManager.InstanceEnemy.MaxMana = MaxMana;
        CurrentEnemy = Instantiate(ChooseEnemyType(), transform);
        CurrentEnemy.Initialize(_enemyIntent);
        BattleManager.Instance.CurrentEnemy = CurrentEnemy;
        ManaManager.InstanceEnemy.ResetMana();
    }

    private Enemy ChooseEnemyType()
    {
        return _enemyPrefabs[Random.Range(0, _enemyPrefabs.Count)];
    }
}