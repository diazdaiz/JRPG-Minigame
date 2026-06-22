using NaughtyAttributes;
using UnityEngine;

[CreateAssetMenu(fileName = "Cutscene", menuName = "Exploration/Cutscene")]
public class CutsceneSequence : ScriptableObject {
    public enum CutsceneSequenceType { Dialogue, CharactersMovement, WaitSecond }
    [SerializeField] CutsceneSequenceType type;

    [ShowIf("type", CutsceneSequenceType.Dialogue)][SerializeField] string fungusBoxName;
    [ShowIf("type", CutsceneSequenceType.CharactersMovement)][SerializeField] string test2;
}
