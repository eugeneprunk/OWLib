// Instance generated by TankLibHelper.InstanceBuilder

// ReSharper disable All
namespace TankLib.STU.Types {
    [STUAttribute(0x57AEDFA2, "STUGameModeBodyVars")]
    public class STUGameModeBodyVars : STUInstance {
        [STUFieldAttribute(0x5C307091, "m_vars", ReaderType = typeof(InlineInstanceFieldReader))]
        public STUGameModeVarValuePair[] m_vars;

        [STUFieldAttribute(0x37AB13D3, "m_hero", ReaderType = typeof(EmbeddedInstanceFieldReader))]
        public STU_21CE58F6 m_hero;
    }
}