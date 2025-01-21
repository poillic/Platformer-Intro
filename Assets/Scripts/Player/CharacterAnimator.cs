using UnityEngine;

public class CharacterAnimator : MonoBehaviour
{
    #region Public

    #endregion

    #region Unity Lifecycle

    private void Start()
    {
        characterMovement = GetComponent<CharacterMovement>();
        groundChecker = GetComponent<CharacterGround>();
        Transform graphics = transform.Find( "Graphics" );
        _animator = graphics.GetComponent<Animator>();
        spriteRenderer = graphics.GetComponent<SpriteRenderer>();
    }

    private void Update()
    {
        _animator.SetBool( "IsJumping", !groundChecker.GetOnGround() );
        _animator.SetBool( "IsMoving", characterMovement.IsMoving() );

        if( characterMovement.MoveDirection() != 0 )
        {
            spriteRenderer.flipX = characterMovement.MoveDirection() > 0;
        }
    }

    #endregion

    #region Main Methods

    #endregion

    #region Private & Protected

    private CharacterGround groundChecker;
    private CharacterMovement characterMovement;
    private Animator _animator;
    private SpriteRenderer spriteRenderer;
    #endregion
}