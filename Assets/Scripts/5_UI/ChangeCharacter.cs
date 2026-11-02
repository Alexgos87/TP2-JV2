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
    [SerializeField] private InputActionReference previousControler;
    [SerializeField] private InputActionReference nextControler;

    private Character[] characters; // list des personnages dans la scène qui vien du gameControler

    private Character ActualCharchter;
    private int index = 0;

    private TMP_Text Name;
    private Image Avatar;

    private void Awake()
    {
        characters = Finder.GameController.CityObjects.Characters;
        ActualCharchter = characters[index];

        Name = GetComponentInChildren<TMP_Text>();
        Avatar = GetComponentInChildren<Image>();
    }


    private void Update()
    {

        if (nextPc.action.IsPressed() || nextControler.action.IsPressed())
        {
            ActualCharchter.HidePointer();
            nextCharchter();
            ActualCharchter.ShowPointer();
        }
        
        if(previousPc.action.IsPressed() || previousControler.action.IsPressed())
        {
            ActualCharchter.HidePointer();
            previousCharchter();
            ActualCharchter.ShowPointer();
        } 

    }

    private void previousCharchter()
    {
        if (index-- <= 0)
        {
            index = characters.Length - 1;
            ActualCharchter = characters[index];
        }
        else
        {
            index--;
            ActualCharchter = characters[index];
        }
    }

    private void nextCharchter()
    {
        if (index++ >= characters.Length)
        {
            index = 0;
            ActualCharchter = characters[index];
        }
        else
        {
            index++;
            ActualCharchter = characters[index];
        }
    }

    private void UpdateName()
    {
        Name.text = ActualCharchter.name + "\n le state est afficher ici";
    }

    private void UpdateAvatar()
    {
        // Update the avatar image based on the ActualCharchter
    }
}
