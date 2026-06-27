using UnityEngine;

public class PlayerCombat : CharacterCombat {
    CombatManager combat;
    public CombatActionUI actionUI;

    int selectedAction;

    public void BeginTurn(CombatManager combat) {
        this.combat = combat;

        selectedAction = 0;

        actionUI.Show(true);
        actionUI.SetSelection(selectedAction);
    }

    void Update() {
        if (combat == null)
            return;

        if (combat.turn != CombatManager.CombatTurn.Player)
            return;

        if (Input.GetKeyDown(KeyCode.UpArrow)) {
            selectedAction = Mathf.Max(0, selectedAction - 1);
            actionUI.SetSelection(selectedAction);
        }

        if (Input.GetKeyDown(KeyCode.DownArrow)) {
            selectedAction = Mathf.Min(1, selectedAction + 1);
            actionUI.SetSelection(selectedAction);
        }

        if (Input.GetKeyDown(KeyCode.Return)) {
            switch (selectedAction) {
                case 0:
                    Attack();
                    break;

                case 1:
                    Defend();
                    break;
            }
        }
    }

    void Attack() {
        actionUI.Show(false);
        combat.EndPlayerTurn();
    }

    void Defend() {
        actionUI.Show(false);
        combat.EndPlayerTurn();
    }
}