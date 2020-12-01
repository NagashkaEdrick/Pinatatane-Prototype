using Photon.Pun;

public interface IGrabable
{
    PhotonView PhotonView { get; set; }

    bool CanBeGrabbed { get; set; }

    void StartGrab(int _cible);

    void OnGrab(int _cible, int _attaquant);

    void EndGrab(int _cible);
}
