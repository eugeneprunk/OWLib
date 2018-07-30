// Instance generated by TankLibHelper.InstanceBuilder

// ReSharper disable All
namespace TankLib.STU.Types {
    [STUAttribute(0x456C3C15, "STUGameModeLoadoutOverride")]
    public class STUGameModeLoadoutOverride : STUInstance {
        [STUFieldAttribute(0x37AB13D3, "m_hero")]
        public teStructuredDataAssetRef<STUHero> m_hero;

        [STUFieldAttribute(0x7B95AB93, "m_loadouts")]
        public teStructuredDataAssetRef<STULoadout>[] m_loadouts;
    }
}