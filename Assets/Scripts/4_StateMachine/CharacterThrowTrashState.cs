public class CharacterThrowTrashState : ICharacterState
{
    private readonly Character character;
    private readonly CharacterStateMachine stateMachine;

    public CharacterThrowTrashState(Character character, CharacterStateMachine stateMachine)
    {
        this.character = character;
        this.stateMachine = stateMachine;
    }

    public void Enter()
    {
        character.ThrowTrash();
    }

    public void Update()
    {
        character.Vitals.RaiseAll();

        if (!character.IsThrowingTrash()) stateMachine.PopState();
    }

    public void Exit()
    {
    }
}