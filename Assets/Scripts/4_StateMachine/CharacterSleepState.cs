public class CharacterSleepState : ICharacterState
{
    private readonly Character character;
    private readonly CharacterStateMachine stateMachine;
    private bool isInside;

    public CharacterSleepState(Character character, CharacterStateMachine stateMachine)
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
        var house = character.Blackboard.House;

        if (!character.IsCloseTo(house))
        {
            stateMachine.PushState(new CharacterMoveState(character, stateMachine, house));
            return;
        }

        if (!isInside)
        {
            character.MakeInvisible();
            isInside = true;
        }

        character.Vitals.LowerSleepiness();
        if (character.Vitals.IsSleepinessBelowTarget) stateMachine.PopState();
    }

    public void Exit()
    {
        character.MakeVisible();
    }
}