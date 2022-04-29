public interface IEnemyDamageable
{
    #region Public Methods
    //Allows enemies with this Interface implemented to take damage
    void TakeDamage(float damage);

    #endregion Public Methods
}