using System.Runtime.Serialization;

namespace ExaminationSystem.Models
{
    public enum ExamType
    {
        [EnumMember(Value = "Final")]
        Final,

        [EnumMember(Value = "Quiz")]
        Quiz
    }
}
