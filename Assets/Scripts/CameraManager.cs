using UnityEngine;

public class CameraManager : MonoBehaviour {
    public Vector2 TargetPosition {
        get {
            return targetPosition;
        }
        set {
            Rule = CameraRule.Custom;
            targetPosition = value;
        }
    }
    Vector2 targetPosition;
    public CameraRule Rule { get; set; }
    public enum CameraRule { Custom, Roam, Cutscene, Combat }

    [SerializeField] Character character;

    void Start() {
        Rule = CameraRule.Roam;
    }

    void Update() {
        Vector3 target = new();
        if (Rule == CameraRule.Custom) {
            target = new Vector3(TargetPosition.x, TargetPosition.y, -10);
        }
        if (Rule == CameraRule.Roam) {
            Vector2 pos = character.transform.position;
            target = new Vector3(pos.x, pos.y, -10);
        }
        if (Rule == CameraRule.Cutscene) {
            //...
        }
        if (Rule == CameraRule.Combat) {
            //...
        }

        float speed = (target - transform.position).magnitude * 7;
        if (speed == 0) return;
        Vector3 direction = (target - transform.position).normalized;
        transform.position += direction * speed * Time.deltaTime;
    }
}
