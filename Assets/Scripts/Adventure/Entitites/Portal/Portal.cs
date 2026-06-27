using UnityEngine;

public class Portal : MonoBehaviour {
    [SerializeField] PortalConnector connector;
    Transform PortalssObjectsContainer => AdventureManager.Instance.PortalssObjectsContainer.transform;

    public void Enter(GameObject gameObject) {
        gameObject.transform.parent = PortalssObjectsContainer;
    }
}
