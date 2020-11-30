namespace Pinatatane
{
    public interface IReceiveDamages
    {
        float Velocity { get;}
        float Health { get; set; }
        void ReceivedDamage(float damageTaken);
        void OnDeath();
    }
}
