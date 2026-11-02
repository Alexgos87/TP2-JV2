public class CharacterEatState : ICharacterState
{
    private readonly Character character;
    private readonly CharacterStateMachine stateMachine;
    private readonly Building foodBuilding;
    private bool isInside;

    public CharacterEatState(Character character, CharacterStateMachine stateMachine)
    {
        this.character = character;
        this.stateMachine = stateMachine;

        foodBuilding = character.Blackboard.FoodBuildings.Random();
    }

    public void Enter()
    {
        isInside = false;
    }

    public void Update()
    {
        if (!character.IsCloseTo(foodBuilding))
        {
            stateMachine.PushState(new CharacterMoveState(character, stateMachine, foodBuilding));
            return;
        }

        if (!isInside)
        {
            character.MakeInvisible();
            isInside = true;
        }

        character.Vitals.LowerHunger();
        if (character.Vitals.IsHungerBelowTarget) stateMachine.PopState();
    }

    public void Exit()
    {
        character.MakeVisible();
    }
}