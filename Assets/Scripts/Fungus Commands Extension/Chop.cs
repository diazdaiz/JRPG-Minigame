using Fungus;
using UnityEngine;

namespace FungusCommandExtension {
    [CommandInfo("Adventure Action", "Chop", "Chop.")]
    [AddComponentMenu("")] // Fungus convention: hide from Unity's Add Component menu
    public class Chop : Command {
        [SerializeField] Character chopper;
        [SerializeField] GameObject choppedItem;

        public override void OnEnter() {
            chopper.UseItem(typeof(Axe), null);
            GameObject instantiatedChoppedItem = Instantiate(choppedItem);
            chopper.ReceiveItem(instantiatedChoppedItem.GetComponent<Item>());
            Continue();
        }
    }
}