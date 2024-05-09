using CodeHub.OtherUtilities;
using UnityEngine;
using Zenject;

namespace Game.Installers
{
    [CreateAssetMenu(fileName = "PlayerDataInstaller", menuName = "Installers/PlayerDataInstaller")]
    public class PlayerDataInstaller : ScriptableObjectInstaller<PlayerDataInstaller>
    {
        public PlayerDatabase PlayerDatabase;
        public override void InstallBindings()
        {
            BindPlayerDatabase();
        }

        private void BindPlayerDatabase() => 
            Container.Bind<PlayerDatabase>().FromInstance(PlayerDatabase);
    }
}