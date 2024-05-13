
namespace Game.Scripts.Game.Menu
{
    public static class GameDataHolder
    {
        private static GameData _gameData;
        
        public static GameData GameData => _gameData;
        
        public static void SetGameData(GameData gameData)
        {
            _gameData = gameData;
        }
    }
}