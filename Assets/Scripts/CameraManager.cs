using UnityEngine;

public class CameraManager : MonoBehaviour {
    public Vector2 TargetPosition {
        get {
            return targetPosition;
        }
        set {
            Rule = CameraRule.TargetPosition;
            targetPosition = value;
        }
    }
    public CameraRule Rule { get; set; }
    public CameraMovement Movement { get; set; }
    public float CameraSpeedMultiplier { get; set; } = 6;
    public Character Character {
        get {
            return character;
        }
        set {
            character = value;
        }
    }

    Vector2 targetPosition;
    public enum CameraRule { TargetPosition, FollowCharacter, Combat }
    public enum CameraMovement { SlowedTowardTarget, Linear }

    [SerializeField] Character character;

    void Start() {
        Rule = CameraRule.FollowCharacter;
    }

    void Update() {
        Vector3 target = new();
        if (Rule == CameraRule.TargetPosition) {
            target = new Vector3(TargetPosition.x, TargetPosition.y, -10);
        }
        if (Rule == CameraRule.FollowCharacter) {
            Vector2 pos = character.transform.position;
            target = new Vector3(pos.x, pos.y, -10);
        }
        if (Rule == CameraRule.Combat) {
            //...
        }

        float speed = 0;
        if (Movement == CameraMovement.SlowedTowardTarget) {
            speed = Mathf.Pow((target - transform.position).magnitude, 0.6f) * CameraSpeedMultiplier;
        }
        else if (Movement == CameraMovement.Linear) {
            speed = CameraSpeedMultiplier;
        }
        if (speed == 0) return;
        //Vector3 direction = (target - transform.position).normalized;
        //transform.position += direction * speed * Time.deltaTime;
        transform.position = Vector3.MoveTowards(transform.position, target, speed * Time.deltaTime);
    }
}
