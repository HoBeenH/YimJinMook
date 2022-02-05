using Script.Data;
using Script.Default;
using Script.Manager;
using Script.Player;

namespace Script.Util
{
    public static class Facade
    {
        public static PlayerController _PlayerController => PlayerController.Instance;
        public static MainMenu _MainMenu => MainMenu.Instance;
        public static UiManager _UiManager => UiManager.Instance;
        public static DataManager _DataManager => DataManager.Instance;
        public static ItemPool _ItemPool => ItemPool.Instance;
        public static Input_Manager _Input_Manager => Input_Manager.Instance;
        public static LoadSceneManager _LoadSceneManager => LoadSceneManager.Instance;
        public static EventSystem _EventSystem => EventSystem.Instance;
    }
}
