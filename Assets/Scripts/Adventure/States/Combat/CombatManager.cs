using Fungus;
using System.Collections;
using UnityEngine;

public class CombatManager : MonoBehaviour {
    AdventureManager Adventure => AdventureManager.Instance;
    Flowchart Flowchart => StoryManager.Instance.Flowchart;
    public Character Player => player;
    public Character Enemy => enemy;

    [SerializeField] Character player;
    [SerializeField] Character enemy;

    //[SerializeField] ChooseActionUI
    public int hoveredAction;
    public float attackValue = 0f;
    public float staffMultiplier = 1f;
    public CombatState state;
    public bool isCombatting;
    public float setTimer;
    public float currentTimer;

    bool firstDialogue = false;
    Block block;

    public enum CombatState {
        ChoosePlayerAction,
        PlayerAction,
        EnemyAction
    }

    public void Begin() {
        state = CombatState.ChoosePlayerAction;
        isCombatting = true;
        player.transform.position = enemy.transform.position + new Vector3(-5, 0, 0);
    }

    public void SelectAction() {
        state = CombatState.PlayerAction;
        if (hoveredAction == 0) {
            StartCoroutine(ChargeAttack());
        }
        else if (hoveredAction == 1) {
            StartCoroutine(Heal());
        }
        else if (hoveredAction == 2) {
            StartCoroutine(EnchantStaff());
        }
    }

    IEnumerator ChargeAttack() {
        currentTimer = 0f;
        attackValue = 0f;
        setTimer = 5f;
        while (currentTimer < setTimer) {
            if (Input.GetKeyDown(KeyCode.Space)) {
                attackValue += 1 * staffMultiplier;
            }
            currentTimer += Time.deltaTime;
            yield return null;
        }
        enemy.Combat.HealthPoint -= attackValue;
        yield return new WaitForSeconds(1f);
        setTimer = 0f;
        if (enemy.Combat.HealthPoint <= 0) {
            Finish();
            block = Flowchart.FindBlock("5.C3. Combat dialogue 2");
            Adventure.ChangeState(AdventureManager.AdventureState.Cutscene);
            Flowchart.ExecuteBlock(block);

        }
        else {
            if (firstDialogue == false) {
                block = Flowchart.FindBlock("5.C2. Combat dialogue 1");
                Adventure.ChangeState(AdventureManager.AdventureState.Cutscene);
                Flowchart.ExecuteBlock(block);
                Adventure.StartCoroutine(CheckForCutsceneEnding());
                firstDialogue = true;
            }
            else {
                StartCoroutine(EnemyAttack());
            }
        }
    }

    IEnumerator EnchantStaff() {
        float t = 0f;
        Item staff = null;
        for (int i = 0; i < player.Inventory.Items.Count; i++) {
            if (player.Inventory.Items[i].GetType() == typeof(MagicalStaff)) {
                staff = player.Inventory.Items[i];
            }
        }
        Vector3 initialPos = staff.transform.position;
        bool doAction = false;
        while (t < 2) {
            t += Time.deltaTime;
            staff.transform.position = initialPos + new Vector3(0, (-Mathf.Cos(Mathf.PI * t) + 1f) / 5f, 0);
            if (t > 1.2f && !doAction) {
                doAction = true;
                staffMultiplier += 1f;
            }
            yield return null;
        }
        staff.transform.position = initialPos;
        yield return new WaitForSeconds(1f);
        StartCoroutine(EnemyAttack());
    }

    IEnumerator Heal() {
        float t = 0f;
        Item staff = null;
        for (int i = 0; i < player.Inventory.Items.Count; i++) {
            if (player.Inventory.Items[i].GetType() == typeof(MagicalStaff)) {
                staff = player.Inventory.Items[i];
            }
        }
        Vector3 initialPos = staff.transform.position;
        bool doAction = false;
        while (t < 2) {
            t += Time.deltaTime;
            staff.transform.position = initialPos + new Vector3(0, (-Mathf.Cos(Mathf.PI * t) + 1f) / 5f, 0);
            if (t > 1.2f && !doAction) {
                doAction = true;
                player.Combat.HealthPoint += 25 * staffMultiplier;
            }
            yield return null;
        }
        staff.transform.position = initialPos;
        yield return new WaitForSeconds(1f);
        StartCoroutine(EnemyAttack());
    }

    IEnumerator EnemyAttack() {
        state = CombatState.EnemyAction;
        yield return new WaitForSeconds(3);
        float t = 0f;
        Vector3 initialPos = enemy.transform.position;
        bool parrying = false;
        Vector3 direction = (player.transform.position - enemy.transform.position).normalized;
        Vector3 targetPos = initialPos + direction * (player.transform.position - enemy.transform.position).magnitude * 2;
        float damage = 40;
        while (t < 3) {
            t += Time.deltaTime;
            if (t > 1f) {
                float x = t - 1f;
                float e = 2.71828f;
                float reduction = 1 - Mathf.Pow(e, -50 * Mathf.Pow(x - 0.45f, 2));
                damage = 40 * reduction;
                enemy.transform.position = initialPos + (targetPos - initialPos) * 1 * x;
                if (!parrying && Input.GetKeyDown(KeyCode.Space)) {
                    Debug.Log(x);
                    player.Combat.HealthPoint -= damage;
                    parrying = true;
                }
            }
            yield return null;
        }
        if (!parrying) {
            player.Combat.HealthPoint -= damage;
        }
        enemy.transform.position = initialPos;
        yield return new WaitForSeconds(0.5f);
        state = CombatState.ChoosePlayerAction;
    }

    private void Update() {
        if (!isCombatting && Adventure.State != AdventureManager.AdventureState.Combat) {
            return;
        }
        if (state == CombatState.ChoosePlayerAction) {
            if (Input.GetKeyDown(KeyCode.W) || Input.GetKeyDown(KeyCode.UpArrow)) {
                hoveredAction += 2;
            }
            if (Input.GetKeyDown(KeyCode.S) || Input.GetKeyDown(KeyCode.DownArrow)) {
                hoveredAction += 1;
            }
            hoveredAction %= 3;
            if (Input.GetKeyDown(KeyCode.Space)) {
                SelectAction();
            }
        }
    }

    IEnumerator CheckForCutsceneEnding() {
        while (block.IsExecuting()) {
            yield return null;
        }
        Adventure.ChangeState(AdventureManager.AdventureState.Combat);
        StartCoroutine(EnemyAttack());
    }

    public void Finish() {
        isCombatting = false;
    }
}