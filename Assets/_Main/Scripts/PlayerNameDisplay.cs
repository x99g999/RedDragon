using Photon.Pun;
using TMPro;
public class PlayerNameDisplay : MonoBehaviourPunCallbacks
{
    private void Start()
    {
        var nameLabel = GetComponent<TextMeshPro>();
        nameLabel.text = $"{photonView.Owner.NickName}";
    }
}