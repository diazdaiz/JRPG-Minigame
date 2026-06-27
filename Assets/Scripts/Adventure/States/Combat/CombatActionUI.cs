using UnityEngine;
using UnityEngine.UI;

public class CombatActionUI : MonoBehaviour {
    public Text attackText;
    public Text defendText;

    public Color normalColor = Color.white;
    public Color selectedColor = Color.yellow;

    public void Show(bool show) {
        gameObject.SetActive(show);
    }

    public void SetSelection(int index) {
        attackText.color = normalColor;
        defendText.color = normalColor;

        switch (index) {
            case 0:
                attackText.color = selectedColor;
                break;

            case 1:
                defendText.color = selectedColor;
                break;
        }
    }
}