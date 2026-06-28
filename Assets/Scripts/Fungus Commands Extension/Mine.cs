using Fungus;
using UnityEngine;

[CommandInfo("Adventure Action", "Mine", "Mine.")]
[AddComponentMenu("")] // Fungus convention: hide from Unity's Add Component menu
public class Mine : Command {
    [SerializeField] Character miner;
    [SerializeField] GameObject minedItem;

    public override void OnEnter() {
        miner.UseItem(typeof(Pickaxe), null);
        GameObject instantiatedChoppedItem = Instantiate(minedItem);
        miner.ReceiveItem(instantiatedChoppedItem.GetComponent<Item>());
        Continue();
    }
}
