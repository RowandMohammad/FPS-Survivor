using UnityEngine;

public class Enemy : MonoBehaviour
{
    #region Public Fields

    public StateChangeEvent OnStateChange;

    #endregion Public Fields



    #region Private Fields

    [SerializeField]
    private StateEnemy _State;

    #endregion Private Fields



    #region Public Delegates

    public delegate void StateChangeEvent(StateEnemy OldState, StateEnemy NewState);

    #endregion Public Delegates



    #region Public Properties

    public StateEnemy State
    {
        get
        {
            return _State;
        }
        private set
        {
            if (_State != value)
            {
                OnStateChange?.Invoke(_State, value);
            }
            _State = value;
        }
    }

    #endregion Public Properties



    #region Public Methods

    public void ChangeState(StateEnemy NewState)
    {
        if (NewState != State)
        {
            State = NewState;
        }
    }

    #endregion Public Methods
}