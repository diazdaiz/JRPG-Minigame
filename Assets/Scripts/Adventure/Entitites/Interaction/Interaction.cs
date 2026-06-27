using NaughtyAttributes;
using UnityEngine;

[CreateAssetMenu(fileName = "Interaction", menuName = "Adventure/Interaction")]
public class Interaction : ScriptableObject {
    public string BlockName => blockName;
    public InteractionType Type => type;
    public CollectType CollectInteractionType => collectInteractionType;
    public Item CollactableItem => item;
    AdventureManager Adventure => AdventureManager.Instance;
    //Note: dialogue & cutscene sama-sama di handle sama fungus, jadinya disamain aja di Type nya
    InteractionType ExposeVariableOfType {
        get {
            if (type == InteractionType.Dialogue) {
                return InteractionType.Cutscene;
            }
            return type;
        }
    }
    bool ExposeItemVariable {
        get {
            return Type == InteractionType.Collect && (CollectInteractionType == CollectType.Chop || CollectInteractionType == CollectType.Mine);
        }
    }

    [SerializeField] InteractionType type;
    [ShowIf(nameof(ExposeVariableOfType), InteractionType.Cutscene)][SerializeField] string blockName;
    [ShowIf(nameof(ExposeVariableOfType), InteractionType.Collect)][SerializeField] CollectType collectInteractionType;
    [ShowIf(nameof(ExposeItemVariable))][SerializeField] Item item;
    [ShowIf(nameof(ExposeVariableOfType), InteractionType.EnterPortal)][SerializeField] Portal portalEntrance;
    [ShowIf(nameof(ExposeVariableOfType), InteractionType.ExitPortal)][SerializeField] Portal portalExit;

    public enum InteractionType { Dialogue, Cutscene, Collect, EnterPortal, ExitPortal }
    public enum CollectType { Pick, Chop, Mine }
}
