using UnityEngine;

public class PlayerBlock : MonoBehaviour
{
    private Animator animator;
    private enum BlockState { None, Enter, InBlock, Exit }
    private BlockState currentBlockState = BlockState.None;
    [SerializeField] private AudioClip block;

    private void Start()
    {
        animator = GetComponent<Animator>();
    }

    private void Update()
    {
        HandleInput();

        animator.SetBool("EnterBlock", currentBlockState == BlockState.Enter);
        animator.SetBool("InBlock", currentBlockState == BlockState.InBlock);
        animator.SetBool("ExitBlock", currentBlockState == BlockState.Exit);
    }

    private void HandleInput()
    {
        if (Input.GetKeyDown(KeyCode.U))
        {
            if (currentBlockState == BlockState.None || currentBlockState == BlockState.Exit)
            {
                currentBlockState = BlockState.Enter;
                SoundManager.instance.PlaySound(block);
            }
           
        }
        else if (Input.GetKey(KeyCode.U))
        {
            if (currentBlockState == BlockState.Enter || currentBlockState == BlockState.InBlock)
            {
                currentBlockState = BlockState.InBlock;
                
            }
        }
        else if (Input.GetKeyUp(KeyCode.U))
        {
            if (currentBlockState == BlockState.InBlock)
            {
                currentBlockState = BlockState.Exit;
            }
            else if (currentBlockState == BlockState.Enter)
            {
                // The case where the key was released quickly after pressing
                currentBlockState = BlockState.None;
            }
        }
    }

    public bool IsInBlockState()
    {
        return currentBlockState == BlockState.InBlock;
    }
}