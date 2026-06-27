using Fungus;
using System.Collections.Generic;
using UnityEngine;

public class StoryManager : MonoBehaviour {
    public static StoryManager Instance;
    public Flowchart Flowchart { get; private set; }

    //ch => progress tiap interacted object => 
    [SerializeField] Flowchart flowchart;
    [SerializeField] List<StoryBasedInteraction> storyBasedInteraction;

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

    Dictionary<InteractableObject, bool> interactableObjectHasBeenInteracted;

    public void ReassignInteractionToChapter(string chapter) {
        interactableObjectHasBeenInteracted = new();
        for (int i = 0; i < storyBasedInteraction.Count; i++) {
            if (storyBasedInteraction[i].chapter != chapter) {
                continue;
            }
            for (int j = 0; j < storyBasedInteraction[i].chAndCountBasedInteraction.Count; j++) {
                InteractableObject io = storyBasedInteraction[i].chAndCountBasedInteraction[j].interactableObject;
                if (!interactableObjectHasBeenInteracted.ContainsKey(io)) {
                    interactableObjectHasBeenInteracted[io] = false;
                    io.Interaction = storyBasedInteraction[i].chAndCountBasedInteraction[j].interaction;
                }
            }
        }
    }

    public void StartChapter2a() {
        ReassignInteractionToChapter("2a");
    }

    public void StartChapter2b() {
        ReassignInteractionToChapter("2b");
    }

    public void StartChapter3() {
        ReassignInteractionToChapter("3");
    }

    public void StartChapter4() {
        ReassignInteractionToChapter("4");
    }

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
        interactableObjectHasBeenInteracted = new();
    }
}
