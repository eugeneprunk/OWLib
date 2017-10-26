// File auto generated by STUHashTool
using STULib.Types.Gamemodes.Unknown;
using STULib.Types.Gamemodes.Unknown.Enums;
using static STULib.Types.Generic.Common;

namespace STULib.Types.Gamemodes {
    [STU(0x494C33C4)]
    public class STUGamemode : STUInstance {
        [STUField(0xA43573F4)]
        public STUGUID[] Virtual01CArray;  // STU_9CADF2EC

        [STUField(0xCF63B633)]
        public STUGUID StateScriptA;  // STU_6BE90C5C

        [STUField(0x3CE93B76)]
        public STU_9783E116[] m_3CE93B76;

        [STUField(0xF88BA3B9)]
        public STUGUID StateScriptB;  // STU_6BE90C5C

        [STUField(0xAD4BF17F)]
        public STU_9783E116[] m_AD4BF17F;

        [STUField(0x5DB91CE2)]
        public STUGUID Name;

        [STUField(0x6EB38130)]
        public STUGUID Image;

        [STUField(0xE04197AF)]
        public STUGUID[] Parameters;  // STUCustomGameOptionsSection

        [STUField(0xD440A0F7)]
        public STUGamemodeTeam[] Teams;

        [STUField(0xDA642982)]
        public STUGamemodeHeroOverride[] HeroOverrides;

        [STUField(0xA8957901)]
        public STUGUID Virtual01C;  // STU_ACCDE63F

        [STUField(0xCBAE46D4)]
        public float m_CBAE46D4; // 3 for Snowball, 0 everything else

        [STUField(0x37D4F9CD)]
        public int m_37D4F9CD; // 1 for Elimination/Deathmatch/Lucioball/Junkenstein/CTF, 0 everything else

        [STUField(0x8A5415B9)]
        public STUEnum_1964FED7 UnknownEnum;

        [STUField(0xF3E24B6F)]
        public byte m_F3E24B6F; // 0 for Elimination/Deathmatch/Lucioball/Junkenstein/Snowball/Uprising/CTF, 1 everything else

        [STUField(0x040601B2)]
        public byte m_040601B2; // 0 for Elimination/Deathmatch/Lucioball/Junkenstein/Snowball/Uprising/CTF, 1 everything else

        [STUField(0x0FC17230)]
        public byte m_0FC17230; // 1 for Snowball/Uprising, 0 everything else

        [STUField(0x372E20EB)]
        public byte m_372E20EB; // 1 for Uprising, 0 everything else
    }
}
