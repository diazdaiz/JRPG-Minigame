using System;
using TMPro;
using UnityEngine;
using UnityEngine.Rendering.Universal;
using UnityEngine.UI;

public class CombatVisual : MonoBehaviour {
    AdventureManager Adventure => AdventureManager.Instance;
    [SerializeField] CombatManager combat;
    [SerializeField] RectTransform playerChooseActionContainer;
    [SerializeField] Image attackSelectionImage;
    [SerializeField] Image healSelectionImage;
    [SerializeField] Image enchantSelectionImage;
    [SerializeField] TextMeshProUGUI enchantTmp;

    [SerializeField] RectTransform playerHPContainer;
    [SerializeField] RectTransform playerHPBar;
    [SerializeField] TextMeshProUGUI playerHPText;
    [SerializeField] RectTransform enemyHPContainer;
    [SerializeField] RectTransform enemyHPBar;
    [SerializeField] TextMeshProUGUI enemyHPText;

    [SerializeField] TextMeshProUGUI damageText;
    [SerializeField] RectTransform timerContainer;
    [SerializeField] RectTransform timerBar;

    [SerializeField] RectTransform parryText;
    [SerializeField] Light2D light2D;
    float t = 0;

    void Update() {
        if (!combat.isCombatting) {
            playerHPContainer.anchoredPosition = new Vector2(playerHPContainer.anchoredPosition.x, 700);
            enemyHPContainer.anchoredPosition = new Vector2(enemyHPContainer.anchoredPosition.x, 700);
            playerChooseActionContainer.anchoredPosition = new(-550, playerChooseActionContainer.anchoredPosition.y);
            timerContainer.anchoredPosition = new Vector2(timerContainer.anchoredPosition.x, 1100);
            return;
        }
        enchantTmp.text = $"Enchant Staff\r\n(Current Mult: {combat.staffMultiplier})";

        playerHPContainer.anchoredPosition = new Vector2(playerHPContainer.anchoredPosition.x, 451);
        playerHPBar.offsetMax = new Vector2(-7 - (575 - 7) * (100 - combat.Player.Combat.HealthPoint) / 100f, playerHPBar.offsetMax.y);
        playerHPText.text = $"{combat.Player.Combat.HealthPoint.ToString("F0")}/100";

        enemyHPContainer.anchoredPosition = new Vector2(enemyHPContainer.anchoredPosition.x, 451);
        enemyHPBar.offsetMax = new Vector2(-7 - (575 - 7) * (100 - combat.Enemy.Combat.HealthPoint) / 100f, enemyHPBar.offsetMax.y);
        enemyHPText.text = $"{combat.Enemy.Combat.HealthPoint.ToString("F0")}/100";

        if (combat.state == CombatManager.CombatState.PlayerAction && combat.setTimer != 0) {
            timerContainer.anchoredPosition = new Vector2(timerContainer.anchoredPosition.x, 316);
            timerBar.offsetMax = new Vector2(-7 - (1134 - 7) * (combat.setTimer - combat.currentTimer) / combat.setTimer, timerBar.offsetMax.y);
            damageText.text = combat.attackValue.ToString("F0");
        }
        else {
            timerContainer.anchoredPosition = new Vector2(timerContainer.anchoredPosition.x, 1100);
        }

        if (combat.state == CombatManager.CombatState.EnemyAction) {
            t += Time.deltaTime;
            parryText.anchoredPosition = new Vector2(parryText.anchoredPosition.x, -392);
            light2D.intensity = 20 + MathF.Cos(t * 10) * 10;
        }
        else {
            parryText.anchoredPosition = new Vector2(parryText.anchoredPosition.x, -800);
            light2D.intensity = 3.18f;
        }

        if (combat.state == CombatManager.CombatState.ChoosePlayerAction) {
            playerChooseActionContainer.anchoredPosition = new(26f, playerChooseActionContainer.anchoredPosition.y);
            Color hoveredColor = new Color(43f / 255f, 43f / 255f, 43f / 255f, 1f);
            Color nonhoveredColor = new Color(63f / 255f, 63f / 255f, 63f / 255f, 1f);
            if (combat.hoveredAction == 0) {
                attackSelectionImage.color = hoveredColor;
            }
            else {
                attackSelectionImage.color = nonhoveredColor;
            }
            if (combat.hoveredAction == 1) {
                healSelectionImage.color = hoveredColor;
            }
            else {
                healSelectionImage.color = nonhoveredColor;
            }
            if (combat.hoveredAction == 2) {
                enchantSelectionImage.color = hoveredColor;
            }
            else {
                enchantSelectionImage.color = nonhoveredColor;
            }
        }
        else {
            playerChooseActionContainer.anchoredPosition = new(-550, playerChooseActionContainer.anchoredPosition.y);

        }
    }
}
