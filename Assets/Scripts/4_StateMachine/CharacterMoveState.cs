public class CharacterMoveState : ICharacterState
{
    private readonly Character character;
    private readonly CharacterStateMachine stateMachine;
    private readonly IDestination destination;

    public CharacterMoveState(Character character, CharacterStateMachine stateMachine, IDestination destination)
    {
        this.character = character;
        this.stateMachine = stateMachine;
        this.destination = destination;
    }

    public void Enter()
    {
        character.NavigateTo(destination);
    }

    public void Update()
    {
        character.Vitals.RaiseAll();

        if (!destination.IsAvailable || character.IsCloseTo(destination))
        {
            stateMachine.PopState();
            return;
        }

        if (destination is not Building) return;

        var blackboard = character.Blackboard;
        if (blackboard.LastSeenTrash != null)
            stateMachine.PushState(new CharacterPickUpTrashState(character, stateMachine, blackboard.LastSeenTrash));
        else if (character.ShouldThrowTrash)
            stateMachine.PushState(new CharacterThrowTrashState(character, stateMachine));
        else if (blackboard.LastSeenFriend != null)
            stateMachine.PushState(new CharacterGreetState(character, stateMachine, blackboard.LastSeenFriend));
    }

    public void Exit()
    {
        character.CancelNavigate();
    }
}