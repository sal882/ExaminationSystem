using System.Runtime.Serialization;

namespace ExaminationSystem.Models
{
    public enum QuestionLevel
    {
        [EnumMember(Value ="Simple")]
        Simple,

        [EnumMember(Value = "Medium")]
        Medium,

        [EnumMember(Value = "Hard")]
        Hard
    }
}
