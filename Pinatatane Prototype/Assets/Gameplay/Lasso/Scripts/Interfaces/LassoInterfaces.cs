using System.Collections;
using System.Collections.Generic;

using Photon.Pun;

using UnityEngine;

using GameplayFramework.Network;

namespace Pinatatane
{
    public interface IGrabbable //Signifier que l'on peut se faire grab.
    {
        Transform Transform { get;}

        Rigidbody Rigidbody { get;}

        Lasso GrabbedBy { get; set; }

        /// <summary>
        /// Callback when the object grabbed at first frame
        /// </summary>
        void OnStartGrabbed();

        /// <summary>
        /// Callback when the object is grabbed on update
        /// </summary>
        void OnCurrentGrabbed();

        /// <summary>
        /// Callback when the object is grabbed at last frame
        /// </summary>
        void OnEndGrabbed();
    }    

    //public interface IPlayerGrabable : INetWorkGrabable //IGrabable spécifique au joueur
    //{

    //}

    public interface INetWorkGrabbable : IGrabbable
    {
        NetworkSharedTransform NetworkSharedTransform { get;}

        PhotonView PhotonView { get; }
    }
}
