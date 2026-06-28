using UnityEngine;

public class CharacterCombat : MonoBehaviour {
    public float HealthPoint {
        get {
            return healthPoint;
        }
        set {
            healthPoint = Mathf.Clamp(value, 0f, 100f);
        }
    }
    [SerializeField] float healthPoint = 100;
}
