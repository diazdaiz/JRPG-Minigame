using System.Collections;
using UnityEngine;

public class EnemyCombat : CharacterCombat {
    CombatManager combat;

    int patternIndex;

    public void BeginTurn(CombatManager combat) {
        this.combat = combat;
        StartCoroutine(AttackPattern());
    }

    IEnumerator AttackPattern() {
        switch (patternIndex) {
            case 0:
                yield return FireStraight();
                break;
            case 1:
                yield return FireLeftRight();
                break;
        }

        patternIndex = (patternIndex + 1) % 2;

        combat.EndEnemyTurn();
    }

    IEnumerator FireStraight() {
        Debug.Log("Bullet");
        yield return new WaitForSeconds(.3f);
        Debug.Log("Bullet");
        yield return new WaitForSeconds(.3f);
        Debug.Log("Bullet");
    }

    IEnumerator FireLeftRight() {
        Debug.Log("Left");
        yield return new WaitForSeconds(.2f);
        Debug.Log("Right");
        yield return new WaitForSeconds(.2f);
        Debug.Log("Left");
        yield return new WaitForSeconds(.2f);
        Debug.Log("Right");
    }
}