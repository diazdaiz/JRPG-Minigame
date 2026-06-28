using Fungus;
using UnityEngine;

namespace FungusCommandExtension {
    [CommandInfo("Adventure Action", "Reassign Interaction To Chapter", "Reassign Interaction To Chapter.")]
    [AddComponentMenu("")] // Fungus convention: hide from Unity's Add Component menu
    public class ReassignInteractionToChapter : Command {
        [SerializeField] public string chapter;

        public override void OnEnter() {
            StoryManager.Instance.ReassignInteractionToChapter(chapter);
            Continue();
        }
    }
}