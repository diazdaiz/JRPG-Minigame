using Fungus;
using UnityEngine;

[CreateAssetMenu(fileName = "Cutscene", menuName = "Adventure Action/Cutscene")]
public class Cutscene : AdventureAction {
    [SerializeField] public Flowchart flowchart;
    [SerializeField] public string blockName;
    AdventureManager Adventure => AdventureManager.Instance;
    AdventureManager.AdventureState previousAdventureState;

    public override void Start() {
        previousAdventureState = Adventure.State;
        flowchart.ExecuteBlock(blockName);
        Adventure.ChangeState(AdventureManager.AdventureState.Cutscene);
    }

    public override void Finish() {
        Adventure.ChangeState(previousAdventureState);
    }
}
