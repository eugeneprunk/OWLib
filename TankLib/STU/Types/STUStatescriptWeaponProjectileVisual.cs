// Instance generated by TankLibHelper.InstanceBuilder

// ReSharper disable All
namespace TankLib.STU.Types {
    [STUAttribute(0xF18160D1, "STUStatescriptWeaponProjectileVisual")]
    public class STUStatescriptWeaponProjectileVisual : STUInstance {
        [STUFieldAttribute(0xC9D22FAA, "m_fixedDataFlow", ReaderType = typeof(InlineInstanceFieldReader))]
        public STUStatescriptWeaponProjectileDataFlow[] m_fixedDataFlow;

        [STUFieldAttribute(0xF3E60FB5, "m_animRecoilImpulseFactor", ReaderType = typeof(EmbeddedInstanceFieldReader))]
        public STUConfigVar m_animRecoilImpulseFactor;

        [STUFieldAttribute(0xADDACE26, "m_showHUDWarningIndicator", ReaderType = typeof(EmbeddedInstanceFieldReader))]
        public STUConfigVar m_showHUDWarningIndicator;
    }
}
