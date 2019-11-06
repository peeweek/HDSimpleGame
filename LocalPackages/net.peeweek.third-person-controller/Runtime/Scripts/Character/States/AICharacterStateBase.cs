namespace ThirdPersonController
{
    public abstract class AICharacterStateBase : ICharacterState
    {
        public static readonly ICharacterState GROUNDED_STATE = new AIGroundedCharacterState();
        public static readonly ICharacterState JUMPING_STATE = new AIJumpingCharacterState();
        public static readonly ICharacterState IN_AIR_STATE = new AIInAirCharacterState();

        public virtual void OnEnter(Character character) { }

        public virtual void OnExit(Character character) { }

        public virtual void Update(Character character)
        {
            character.ApplyGravity();

            character.MoveVector = character.navMeshAgent.nextPosition - character.transform.position;
            character.navMeshAgent.Move(character.MoveVector * UnityEngine.Time.deltaTime);
        }

        public virtual void ToState(Character character, ICharacterState state)
        {
            character.CurrentState.OnExit(character);
            character.CurrentState = state;
            character.CurrentState.OnEnter(character);
        }
    }
}
