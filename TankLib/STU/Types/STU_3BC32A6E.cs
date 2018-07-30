// Instance generated by TankLibHelper.InstanceBuilder

// ReSharper disable All
namespace TankLib.STU.Types {
    [STUAttribute(0x3BC32A6E)]
    public class STU_3BC32A6E : STUInstance {
        [STUFieldAttribute(0x25B2A624, "m_cellData", ReaderType = typeof(InlineInstanceFieldReader))]
        public STUAICellData[] m_cellData;

        [STUFieldAttribute(0xD7C9DAAC, "m_cellBytes", ReaderType = typeof(InlineInstanceFieldReader))]
        public STUAICellBytes[] m_cellBytes;

        [STUFieldAttribute(0x64893F8F, "m_collisionCrc")]
        public ulong m_collisionCrc;

        [STUFieldAttribute(0xD4E3DEAB)]
        public uint m_D4E3DEAB;
    }
}