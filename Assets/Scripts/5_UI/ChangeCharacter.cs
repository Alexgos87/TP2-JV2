using System;
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
    [SerializeField] private Scrollbar Scocial;

    [Header("Bar Colors")]
    [SerializeField] private Color lowColor = Color.red;
    [SerializeField] private Color midColor = Color.yellow;
    [SerializeField] private Color highColor = Color.green;

    private Character[] characters; // list des personnages dans la scène qui vien du gameControler

    private Character ActualCharchter;
    private int index = 0;



    private void Awake()
    {
        characters = Finder.GameController.CityObjects.Characters;
        ActualCharchter = characters[index];
    }


    private void Update()
    {
        if (nextPc.action.WasPressedThisFrame())
        {
            ActualCharchter.HidePointer();
            nextCharchter();
            ActualCharchter.ShowPointer();
        }

        if (previousPc.action.WasPressedThisFrame())
        {
            ActualCharchter.HidePointer();
            previousCharchter();
            ActualCharchter.ShowPointer();
        }

        refreshUI();
    }

    private void refreshUI()
    {
        UpdateName();
        UpdateAvatar();
        updateScrollbar();
    }

    private void previousCharchter()
    {
        if (characters == null || characters.Length == 0)
        {
            return;
        }

        index = (index - 1 + characters.Length) % characters.Length;
        ActualCharchter = characters[index];
    }

    private void nextCharchter()
    {
        if (characters == null || characters.Length == 0)
        {
            return;
        }

        index = (index + 1) % characters.Length;
        ActualCharchter = characters[index];
    }

    private void UpdateName()
    {
        nameText.text = ActualCharchter.FullName + "\n le state est afficher ici";
    }

    private void UpdateAvatar()
    {
        avatarImage.sprite = ActualCharchter.Avatar;
    }

    private void updateScrollbar()
    {
        updateHungerBar();
        updateSleepBar();
        updateSocialBar();
    }

    private void updateHungerBar()
    {
        float value = ActualCharchter.Vitals.Hunger / 100f;
        hunger.size = value;
        hunger.targetGraphic.color = GetColor(value);
    }

    private void updateSleepBar()
    {
        float value = ActualCharchter.Vitals.Sleepiness / 100f;
        sleep.size = value;
        sleep.targetGraphic.color = GetColor(value);
    }

    private void updateSocialBar()
    {
        float value = ActualCharchter.Vitals.Loneliness / 100f;
        Scocial.size = value;
        Scocial.targetGraphic.color = GetColor(value);
    }

    private Color GetColor(float value)
    {
        if (value < 0.5f)
            return Color.Lerp(lowColor, midColor, value * 2f);

        return Color.Lerp(midColor, highColor, (value - 0.5f) * 2f);
    }
}