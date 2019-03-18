
namespace audience.game
{

    public interface ISpell
    {
        void SetNetworkManager(NetworkManager nm);
        string GetTitle();
        string GetDescription();
        string GetSpritePath();
        void OnCastButtonClick(int targetId);
    }

}
