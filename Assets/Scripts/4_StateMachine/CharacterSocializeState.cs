public class CharacterSocializeState : ICharacterState
{
    private readonly Character character;
    private readonly CharacterStateMachine stateMachine;
    private readonly Building socialBuilding;
    private bool isInside;

    public CharacterSocializeState(Character character, CharacterStateMachine stateMachine)
    {
        this.character = character;
        this.stateMachine = stateMachine;

        socialBuilding = character.Blackboard.SocialBuildings.Random();
    }

    public void Enter()
    {
        isInside = false;
    }

    public void Update()
    {
        if (!character.IsCloseTo(socialBuilding))
        {
            stateMachine.PushState(new CharacterMoveState(character, stateMachine, socialBuilding));
            return;
        }

        if (!isInside)
        {
            character.MakeInvisible();
            isInside = true;
        }

        character.Vitals.LowerLoneliness();
        if (character.Vitals.IsLonelinessBelowTarget) stateMachine.PopState();
    }

    public void Exit()
    {
        character.MakeVisible();
    }
}