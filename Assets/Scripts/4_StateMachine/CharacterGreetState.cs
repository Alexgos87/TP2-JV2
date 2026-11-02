public class CharacterGreetState : ICharacterState
{
    private readonly Character character;
    private readonly CharacterStateMachine stateMachine;
    private readonly Character friend;

    public CharacterGreetState(Character character, CharacterStateMachine stateMachine, Character friend)
    {
        this.character = character;
        this.stateMachine = stateMachine;
        this.friend = friend;
    }

    public void Enter()
    {
        character.GreetCharacter(friend);
    }

    public void Update()
    {
        character.Vitals.LowerLoneliness();

        if (!character.IsGreetingCharacter()) stateMachine.PopState();
    }

    public void Exit()
    {
    }
}