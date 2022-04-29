using UnityEngine;

public class MenuButtonController : MonoBehaviour
{
    #region Public Fields

    public AudioSource audioSource;

    // Use this for initialization
    public int index;

    #endregion Public Fields



    #region Private Fields

    [SerializeField] private bool keyDown;
    [SerializeField] private int maxIndex;

    #endregion Private Fields



    #region Private Methods

    private void Start()
    {
        audioSource = GetComponent<AudioSource>();
    }

    // Update is called once per frame
    private void Update()
    {
        if (Input.GetAxis("Vertical") != 0)
        {
            if (!keyDown)
            {
                if (Input.GetAxis("Vertical") < 0)
                {
                    if (index < maxIndex)
                    {
                        index++;
                    }
                    else
                    {
                        index = 0;
                    }
                }
                else if (Input.GetAxis("Vertical") > 0)
                {
                    if (index > 0)
                    {
                        index--;
                    }
                    else
                    {
                        index = maxIndex;
                    }
                }
                keyDown = true;
            }
        }
        else
        {
            keyDown = false;
        }
    }

    #endregion Private Methods
}