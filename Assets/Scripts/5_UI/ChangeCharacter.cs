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
        hunger.size = ActualCharchter.Vitals.Hunger / 100f;
    }

    private void updateSleepBar()
    {
        sleep.size = ActualCharchter.Vitals.Sleepiness / 100f;
    }

    private void updateSocialBar()
    {
        Scocial.size = ActualCharchter.Vitals.Loneliness / 100f;
    }
}