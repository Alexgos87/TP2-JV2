using System.Collections.Generic;
using UnityEngine;

public class CharacterStateMachine : MonoBehaviour
{
    
    private Stack<ICharacterState> stateStack = new();
    private Character character;
    
    private void Awake()
    {
        character = GetComponent<Character>();
    }
    
    private void Start()
    {
        stateStack.Push(new CharacterWorkState(character, this));
        stateStack.Peek().Enter();
    }
    private void Update()
    {
        stateStack.Peek().Update();
    }
    
    public void PushState(ICharacterState newState)
    {
        stateStack.Peek().Exit();
        stateStack.Push(newState);
        stateStack.Peek().Enter();
    }

    public void PopState()
    {
        stateStack.Peek().Exit();
        stateStack.Pop();
        stateStack.Peek().Enter();
    }

    public string CurrentStateName()
    {
        return stateStack.Peek().GetType().Name;
    }
}
