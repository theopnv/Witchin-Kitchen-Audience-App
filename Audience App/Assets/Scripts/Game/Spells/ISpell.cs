
namespace audience.game
{

    public interface ISpell
    {
        void SetNetworkManager(NetworkManager nm);
        string GetTitle();
        string GetDescription();
        void OnCastButtonClick();
    }

}
