namespace Pinatatane
{
    public interface IBlockable
    {
        bool IsBlocked { get; set; }

        void OnEnter();
        void OnExit();
    }
}
