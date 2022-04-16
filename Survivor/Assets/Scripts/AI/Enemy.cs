using UnityEngine;

public class Enemy : MonoBehaviour
{
    [SerializeField]
    private StateEnemy _State;
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

    public delegate void StateChangeEvent(StateEnemy OldState, StateEnemy NewState);
    public StateChangeEvent OnStateChange;

    public void ChangeState(StateEnemy NewState)
    {
        if (NewState != State)
        {
            State = NewState;
        }
    }
}