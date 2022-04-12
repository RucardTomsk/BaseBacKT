using MainLABAPI.Enum;

namespace MainLABAPI.Data.Models.DTO
{
    public class ExtendedModelForSolutionData
    {
        public int Id { get; set; }
        public string SourceCode { get; set; }
        public EnumProgrammingLanguage ProgrammingLanguage { get; set; }
        public EnumVerdictsForSolutions Verdict { get; set; }
    }
}
