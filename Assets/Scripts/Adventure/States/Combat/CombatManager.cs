using UnityEngine;

public class CombatManager : MonoBehaviour {
    public PlayerCombat player;
    public EnemyCombat enemy;

    public enum CombatTurn {
        Player,
        Enemy
    }
    public CombatTurn turn;

    void Start() {
        StartPlayerTurn();
    }

    public void StartPlayerTurn() {
        turn = CombatTurn.Player;
        player.BeginTurn(this);
    }

    public void EndPlayerTurn() {
        StartEnemyTurn();
    }

    public void StartEnemyTurn() {
        turn = CombatTurn.Enemy;
        enemy.BeginTurn(this);
    }

    public void EndEnemyTurn() {
        StartPlayerTurn();
    }
}