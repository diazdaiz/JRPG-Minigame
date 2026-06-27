using Fungus;
using UnityEngine;

namespace FungusCommandExtension {
    [CommandInfo("Adventure Action", "Give Item", "Give item.")]
    [AddComponentMenu("")] // Fungus convention: hide from Unity's Add Component menu
    public class GiveItem : Command {
        [SerializeField] public Character giver;
        [SerializeField] public Character receiver;
        [SerializeField] public Item item;

        public override void OnEnter() {
            if (giver == null || receiver == null || item == null) {
                Continue();
                return;
            }
            giver.GiveItem(item, receiver);
            Continue();
        }
    }
}