using Fungus;
using System.Collections.Generic;
using UnityEngine;

public class StoryManager : MonoBehaviour {
    public static StoryManager Instance;
    public Flowchart Flowchart { get; private set; }

    [SerializeField] Character dangalf;
    [SerializeField] Flowchart flowchart;
    [SerializeField] List<StoryBasedInteraction> storyBasedInteraction;

    //Interaction ditentukan dari: ch => progress tiap interacted object => Interaction
    [System.Serializable]
    public class StoryBasedInteraction {
        [SerializeField] public string chapter;
        [SerializeField] public List<ChapterAndInteractionCountBasedInteraction> chAndCountBasedInteraction;

        [System.Serializable]
        public class ChapterAndInteractionCountBasedInteraction {
            [SerializeField] public InteractableObject interactableObject;
            [SerializeField] public int count; //-1 untuk terus2an
            [SerializeField] public Interaction interaction;
        }
    }

    string chapter;
    Dictionary<InteractableObject, List<Interaction>> usableInteraction;

    public void ReassignInteractionToChapter(string chapter) {
        this.chapter = chapter;
        usableInteraction = new();
        for (int i = 0; i < storyBasedInteraction.Count; i++) {
            if (storyBasedInteraction[i].chapter != chapter) {
                continue;
            }
            for (int j = 0; j < storyBasedInteraction[i].chAndCountBasedInteraction.Count; j++) {
                InteractableObject io = storyBasedInteraction[i].chAndCountBasedInteraction[j].interactableObject;
                if (!usableInteraction.ContainsKey(io)) {
                    usableInteraction[io] = new() { storyBasedInteraction[i].chAndCountBasedInteraction[j].interaction };
                    io.Interaction = storyBasedInteraction[i].chAndCountBasedInteraction[j].interaction;
                }
            }
        }
    }

    public void ProgressInteraction(InteractableObject interactableObject) {
        for (int i = 0; i < storyBasedInteraction.Count; i++) {
            if (storyBasedInteraction[i].chapter != chapter) {
                continue;
            }
            for (int j = 0; j < storyBasedInteraction[i].chAndCountBasedInteraction.Count; j++) {
                InteractableObject io = storyBasedInteraction[i].chAndCountBasedInteraction[j].interactableObject;
                if (io == interactableObject && !usableInteraction[io].Contains(storyBasedInteraction[i].chAndCountBasedInteraction[j].interaction)) {
                    usableInteraction[io].Add(storyBasedInteraction[i].chAndCountBasedInteraction[j].interaction);
                    io.Interaction = storyBasedInteraction[i].chAndCountBasedInteraction[j].interaction;
                    return;
                }
            }
        }
    }

    #region special case (hard coded berdasarkan story)
    public void Update2aToolsProgression() {
        if (Flowchart.GetBooleanVariable("Tools2aFound")) {
            return;
        }

        bool axeFound = false;
        bool pickaxeFound = false;
        for (int i = 0; i < dangalf.Inventory.Items.Count; i++) {
            if (dangalf.Inventory.Items[i].GetType() == typeof(Pickaxe)) {
                pickaxeFound = true;
            }
            if (dangalf.Inventory.Items[i].GetType() == typeof(Axe)) {
                axeFound = true;
            }
        }
        if (pickaxeFound && axeFound) {
            Flowchart.SetBooleanVariable("Tools2aFound", true);
        }
    }

    public void Update2bResourcesProgression() {
        if (Flowchart.GetBooleanVariable("Resources2bFound")) {
            return;
        }

        bool manaShardFound = false;
        bool specialLogFound = false;
        for (int i = 0; i < dangalf.Inventory.Items.Count; i++) {
            if (dangalf.Inventory.Items[i].GetType() == typeof(SpecialLog)) {
                specialLogFound = true;
            }
            if (dangalf.Inventory.Items[i].GetType() == typeof(ManaShard)) {
                manaShardFound = true;
            }
        }
        if (specialLogFound && manaShardFound) {
            Flowchart.SetBooleanVariable("Resources2bFound", true);
        }
    }
    #endregion

    private void Awake() {
        if (Instance == null) {
            Instance = this;
        }
        else {
            Destroy(this);
        }
    }

    private void Start() {
        Flowchart = flowchart;
        Flowchart.ExecuteBlock("1. Introduction");
    }

    private void Update() {
        if (chapter == "2a") {
            Update2aToolsProgression();
        }
        if (chapter == "2b") {
            Update2bResourcesProgression();
        }
    }
}
