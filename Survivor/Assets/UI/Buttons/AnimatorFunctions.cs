using UnityEngine;

public class AnimatorFunctions : MonoBehaviour
{
    #region Public Fields

    public bool disableOnce;

    #endregion Public Fields



    #region Private Fields

    [SerializeField] private MenuButtonController menuButtonController;

    #endregion Private Fields



    #region Private Methods

    private void PlaySound(AudioClip whichSound)
    {
        if (!disableOnce)
        {
            menuButtonController.audioSource.PlayOneShot(whichSound);
        }
        else
        {
            disableOnce = false;
        }
    }

    #endregion Private Methods
}