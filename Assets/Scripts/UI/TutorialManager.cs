using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class TutorialManager : MonoBehaviour
{
    [SerializeField] private GameObject tutorialPanel;
    [SerializeField] private TextMeshProUGUI tutorialText;
    [SerializeField] private Button nextButton;
    [SerializeField] private Button closeButton;
    [SerializeField] private TileManager tileManager;

    private int currentStep = 0;
    private List<string> tutorialSteps = new List<string>();

    private void Start()
    {
        tutorialSteps.Add("Kia ora! Welcome to Kiwi Grove. Today you're going to learn how to build your own amazing garden!");
        tutorialSteps.Add("Spring is the season associated with kowhai, as it is one of the first trees to flower after winter..");
        tutorialSteps.Add("It so happens that spring has sprung in Kiwi Grove so let’s plant it!");
        tutorialSteps.Add("Use your arrow keys to move around the map.");
        tutorialSteps.Add("You’ll notice a gardening hoe nearby, walk through it to add it to your inventory.");
        tutorialSteps.Add("Now, look around for a seed packet with a yellow flower on it…");
        tutorialSteps.Add("In real gardens, Kowhai seeds typically need to be scratched or soaked in warm water before planting to help it grow.");
        tutorialSteps.Add("Collect the seeds by walking through it. Click on the chest icon at the top of your screen to access your inventory.");
        tutorialSteps.Add("Drag your gardening hoe and seed packets to your toolbar slots to use them within the map");
        tutorialSteps.Add("Don’t forget: Items must be highlighted in order to use them.");
        tutorialSteps.Add("Close your inventory and walk to the sandy lot by the riverside.");
        tutorialSteps.Add("In Kiwi Grove we have many different types of soil lots and not all plants will thrive in the same conditions as others");
        tutorialSteps.Add("Did you know Kowhai is a vital food source for native birds in Aotearoa?");
        tutorialSteps.Add("WELL, NOW YA DO.");
        tutorialSteps.Add("With the gardening tool selected in your toolbar, press space to dig a hole…");
        tutorialSteps.Add("Now select your seeds and press space to plant them.");
        tutorialSteps.Add("Each plant has a specific amount it needs to be watered and the water bar above the plant will tell you its needs");
        tutorialSteps.Add("Press spacebar when nearby to water it and go through the stages of growth");
        tutorialSteps.Add("Once its’ finished growing click on the plant to harvest it");
        tutorialSteps.Add("Well done! You’ve just harvested your first Kowhai plant");
        tutorialSteps.Add("Fun Fact: Māori traditionally used kōwhai bark for its medicinal properties");
        tutorialSteps.Add("They used it to treat cuts, bruises, sprains, and inflammation");
        tutorialSteps.Add("Now check your plant book up next to the chest icon to view your rewards");
        tutorialSteps.Add("If you look inside you'll notice frame gets filled with a special artwork upon each plants successful harvest");
        tutorialSteps.Add("Happy Gardening!");

        nextButton.onClick.AddListener(NextStep);
        closeButton.onClick.AddListener(CloseTutorial);

        ShowTutorial();
    }

    private void ShowTutorial()
    {
        tutorialPanel.SetActive(true);
        currentStep = 0;
        UpdateText();
    }

    private void NextStep()
    {
        currentStep++;
        if (currentStep < tutorialSteps.Count)
        {
            UpdateText();
        }
        else
        {
            CloseTutorial();
        }
    }

    private void UpdateText()
    {
        tutorialText.text = tutorialSteps[currentStep];
    }

    private void CloseTutorial()
    {
        tutorialPanel.SetActive(false);
    }

public bool IsTileInInteractableMap(Vector3Int position)
{
    if (tileManager != null && tileManager.IsTileInInteractableMap(position))
    {
        return true;
    }
    Debug.LogWarning("TileManager is not assigned or tile is not interactable.");
    return false;
}

}
