using TMPro;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.UI;

public class ChangeCharacter : MonoBehaviour
{
    [Header("Inputs")]
    [SerializeField] private InputActionReference nextPc;
    [SerializeField] private InputActionReference previousPc;

    [Header("UI Elements")]
    [SerializeField] private Image avatarImage;
    [SerializeField] private TMP_Text nameText;
    [SerializeField] private Scrollbar hunger;
    [SerializeField] private Scrollbar sleep;
    [SerializeField] private Scrollbar social;

    [Header("Bar Colors")]
    [SerializeField] private Color lowColor = Color.red;
    [SerializeField] private Color midColor = Color.yellow;
    [SerializeField] private Color highColor = Color.green;

    private Character[] characters;

    private Character actualCharacter;
    private int index;
    
    private void Awake()
    {
        characters = Finder.GameController.CityObjects.Characters;
        actualCharacter = characters[index];
        actualCharacter.ShowPointer();
    }


    private void Update()
    {
        if (nextPc.action.WasPressedThisFrame())
        {
            actualCharacter.HidePointer();
            NextCharacter();
            actualCharacter.ShowPointer();
        }

        if (previousPc.action.WasPressedThisFrame())
        {
            actualCharacter.HidePointer();
            PreviousCharacter();
            actualCharacter.ShowPointer();
        }

        RefreshUI();
    }

    private void RefreshUI()
    {
        UpdateName();
        UpdateAvatar();
        UpdateScrollbar();
    }

    private void PreviousCharacter()
    {
        if (characters == null || characters.Length == 0)
        {
            return;
        }

        index = (index - 1 + characters.Length) % characters.Length;
        actualCharacter = characters[index];
    }

    private void NextCharacter()
    {
        if (characters == null || characters.Length == 0)
        {
            return;
        }

        index = (index + 1) % characters.Length;
        actualCharacter = characters[index];
    }

    private void UpdateName()
    {
        nameText.text = actualCharacter.FullName + "\n ("+actualCharacter.StateMachine.CurrentStateName+")";
        
    }

    private void UpdateAvatar()
    {
        avatarImage.sprite = actualCharacter.Avatar;
    }

    private void UpdateScrollbar()
    {
        UpdateHungerBar();
        UpdateSleepBar();
        UpdateSocialBar();
    }

    private void UpdateHungerBar()
    {
        float value = 1f - actualCharacter.Vitals.Hunger / 100f;
        hunger.size = value;
        hunger.targetGraphic.color = GetColor(value);
    }

    private void UpdateSleepBar()
    {
        float value = 1f - actualCharacter.Vitals.Sleepiness / 100f;
        sleep.size = value;
        sleep.targetGraphic.color = GetColor(value);
    }

    private void UpdateSocialBar()
    {
        float value = 1f - actualCharacter.Vitals.Loneliness / 100f;
        social.size = value;
        social.targetGraphic.color = GetColor(value);
    }

    private Color GetColor(float value)
    {
        if (value < 0.5f)
            return Color.Lerp(lowColor, midColor, value * 2f);

        return Color.Lerp(midColor, highColor, (value - 0.5f) * 2f);
    }
}