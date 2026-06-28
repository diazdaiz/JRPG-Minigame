using UnityEngine;

public class Pickaxe : Item {
    float initialAngle = 60f;
    float speed = 400f;
    float targetAngle = -60f;
    float angle = 0;
    public bool swinging;

    public override void Use(InteractableObject on) {
        base.Use(on);
        Swing();
    }


    void Swing() {
        angle = initialAngle;
        swinging = true;
        gameObject.SetActive(true);
        transform.localPosition = new Vector3(0, -0.06f, 0);
    }

    private void Update() {
        if (swinging) {
            if (angle != targetAngle) {
                angle = Mathf.MoveTowards(angle, targetAngle, speed * Time.deltaTime);
                transform.rotation = Quaternion.Euler(0, 0, angle);
            }
            else {
                gameObject.SetActive(false);
                swinging = false;
            }
        }
    }
}
