namespace ThirdPersonController
{
    /// <summary>
    /// The character is on the ground
    /// </summary>
    public class AIGroundedCharacterState : AICharacterStateBase
    {
        public override void Update(Character character)
        {
            base.Update(character);

            character.ApplyGravity(true); // Apply extra gravity

            //character.IsSprinting = PlayerInput.GetSprintInput();

            if (!character.IsGrounded)
            {
                this.ToState(character, AICharacterStateBase.IN_AIR_STATE);
            }
        }
    }
}
