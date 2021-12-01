using Script.Data;
using Script.Default;
using Script.Manager;
using Script.Player;

namespace Script.Util
{
    public static class Facade
    {
        public static PlayerController _PlayerController => PlayerController.Instance;
        public static MouseManager _MouseManager => MouseManager.Instance;
        public static UiManager _UiManager => UiManager.Instance;
        public static DataManager _DataManager => DataManager.Instance;
        public static ItemPool _ItemPool => ItemPool.Instance;
    }
}
