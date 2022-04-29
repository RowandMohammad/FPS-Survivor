using UnityEngine;

public class MenuButton : MonoBehaviour
{
    #region Private Fields

    [SerializeField] private Animator animator;
    [SerializeField] private AnimatorFunctions animatorFunctions;
    [SerializeField] private MenuButtonController menuButtonController;
    [SerializeField] private int thisIndex;

    #endregion Private Fields



    #region Private Methods

    // Update is called once per frame
    private void Update()
    {
        if (menuButtonController.index == thisIndex)
        {
            animator.SetBool("Selected", true);
            if (Input.GetAxis("Submit") == 1)
            {
                animator.SetBool("Pressed", true);
            }
            else if (animator.GetBool("Pressed"))
            {
                animator.SetBool("Pressed", false);
                animatorFunctions.disableOnce = true;
            }
        }
        else
        {
            animator.SetBool("Selected", false);
        }
    }

    #endregion Private Methods
}