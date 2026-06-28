using Fungus;
using UnityEngine;

namespace FungusCommandExtension {
    [CommandInfo("Adventure Action", "Progress Interaction", "Progress Interaction.")]
    [AddComponentMenu("")] // Fungus convention: hide from Unity's Add Component menu
    public class ProgressInteraction : Command {
        [SerializeField] public InteractableObject interactableObject;

        public override void OnEnter() {
            StoryManager.Instance.ProgressInteraction(interactableObject);
            Continue();
        }
    }
}