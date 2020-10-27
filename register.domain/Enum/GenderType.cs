using System.ComponentModel;

namespace register.domain.Enum
{
    public enum GenderType
    {
        [Description("Not informed")]
        NotInformed = 0,
        [Description("Male")]
        Male = 1,
        [Description("Female")]
        Female = 2,
        [Description("Other")]
        Other = 3
    }
}
