using System;
using UnityEngine;
using UnityEngine.InputSystem;

public class ChangeCharacter : MonoBehaviour
{
    [Header("Inputs")]
    [SerializeField] private InputActionReference next;
    [SerializeField] private InputActionReference previous;

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
        if (next.action.IsPressed())
        {
            nextCharchter();
        }
        
        if(previous.action.IsPressed())
        {
            previousCharchter();
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


    private void UpdateUi()
    {
        // Update the UI to reflect the current character.
        // This could involve updating text, images, or other UI elements.
    }




}
