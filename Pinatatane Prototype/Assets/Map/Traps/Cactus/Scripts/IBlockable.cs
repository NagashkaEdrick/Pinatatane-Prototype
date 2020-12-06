using UnityEngine;

namespace Pinatatane
{
    public interface IBlockable
    {
        bool IsBlocked { get; set; }

        Rigidbody Rigidbody { get;}

        void OnBlockedEnter();
        void OnBlockedExit();
    }
}
