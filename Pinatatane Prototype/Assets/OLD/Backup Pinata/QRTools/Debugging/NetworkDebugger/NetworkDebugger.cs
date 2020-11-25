using Photon.Pun;
using Sirenix.OdinInspector;
using System.Collections.Generic;

using TMPro;

using UnityEngine;

using GameplayFramework.Singletons;

namespace GameplayFramework.Network
{
    public class NetworkDebugger : MonobehaviourSingleton<NetworkDebugger>
    {
        [BoxGroup("References")]
        [SerializeField] TextMeshProUGUI text;
        [BoxGroup("References")]
        public PhotonView photonView;

        List<object> logs = new List<object>();

        [BoxGroup("Options")]
        [SerializeField] int maxLogs = 20;

        public void Debug(object message, DebugType debugType = DebugType.NETWORK)
        {
            switch (debugType)
            {
                case DebugType.LOCAL:
                    logs.Add("<color=green>" + message + "</color>");

                    if (logs.Count > maxLogs)
                        logs.RemoveAt(0);

                    text.text = "";

                    for (int i = 0; i < logs.Count; i++)
                    {
                        text.text += "\n" + logs[i].ToString();
                    }
                    break;
                case DebugType.NETWORK:
                    photonView.RPC("DebuggerNetwork", RpcTarget.All, message);
                    break;
            }
        }

        [PunRPC]
        public void DebuggerNetwork(object message)
        {
            logs.Add("<color=#00ffffff>" + PhotonNetwork.LocalPlayer.NickName + ": " + message + "</color>");

            if (logs.Count > maxLogs)
                logs.RemoveAt(0);

            text.text = "";

            for (int i = 0; i < logs.Count; i++)
            {
                text.text += "\n" + logs[i].ToString();
            }
        }
    }

    public enum DebugType
    {
        LOCAL,
        NETWORK
    }

}