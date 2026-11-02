public class CharacterWorkState : ICharacterState
{
    private Character character;
    private CharacterStateMachine stateMachine;
    private bool isInside;

    public CharacterWorkState(Character character, CharacterStateMachine stateMachine)
    {
        this.character = character;
        this.stateMachine = stateMachine;
    }
    
    public void Enter()
    {
        isInside = false;
    }

    public void Update()
    {
        var vitals = character.Vitals;
        character.Vitals.RaiseAll();
        
        if (vitals.IsHungerAboveThreshold)
            stateMachine.PushState(new CharacterEatState(character, stateMachine));
        else if (vitals.IsLonelinessAboveThreshold)
            stateMachine.PushState(new CharacterSocializeState(character, stateMachine));
        else if (vitals.IsSleepinessAboveThreshold)
            stateMachine.PushState(new CharacterSleepState(character, stateMachine));
        else if (!character.IsCloseTo(character.Blackboard.Workplace))
            stateMachine.PushState(new CharacterMoveState(character, stateMachine, character.Blackboard.Workplace));
        else if (!isInside)
        {
            character.MakeInvisible();
            isInside = true;
        }
    }

    public void Exit()
    {
        character.MakeVisible();
    }
}