namespace Pinatatane
{
    public interface IBlockable
    {
        bool IsBlocked { get; set; }

        void OnBlockedEnter();
        void OnBlockedExit();
    }
}
