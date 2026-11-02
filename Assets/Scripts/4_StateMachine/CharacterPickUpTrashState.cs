public class CharacterPickUpTrashState : ICharacterState
{
    private readonly Character character;
    private readonly CharacterStateMachine stateMachine;
    private readonly Trash trash;
    private bool hasStartedPickUp;

    public CharacterPickUpTrashState(Character character, CharacterStateMachine stateMachine, Trash trash)
    {
        this.character = character;
        this.stateMachine = stateMachine;
        this.trash = trash;
    }

    public void Enter()
    {
    }

    public void Update()
    {
        character.Vitals.RaiseAll();

        if (hasStartedPickUp)
        {
            if (!character.IsPickingTrash()) stateMachine.PopState();
            return;
        }

        if (!trash.IsAvailable)
        {
            stateMachine.PopState();
            return;
        }

        if (!character.IsCloseTo(trash))
        {
            stateMachine.PushState(new CharacterMoveState(character, stateMachine, trash));
            return;
        }

        character.PickUpTrash(trash);
        hasStartedPickUp = true;
    }

    public void Exit()
    {
    }
}