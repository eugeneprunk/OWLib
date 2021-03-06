// Instance generated by TankLibHelper.InstanceBuilder

// ReSharper disable All
namespace TankLib.STU.Types {
    [STUAttribute(0xD19ACE00)]
    public class STU_D19ACE00 : STUInstance {
        [STUFieldAttribute(0x4D2DB658, "m_identifier")]
        public teStructuredDataAssetRef<STUIdentifier> m_identifier;

        [STUFieldAttribute(0x07DD813E, "m_value", ReaderType = typeof(EmbeddedInstanceFieldReader))]
        public STUConfigVar m_value;
    }
}
