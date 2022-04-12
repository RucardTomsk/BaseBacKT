using MainLABAPI.Enum;

namespace MainLABAPI.Data.Models.DB
{
    public class Solution
    {
        public int Id { get; set; }
        public string SourseCode { get; set; }
        public EnumProgrammingLanguage ProgrammingLanguage { get; set; }
        public EnumVerdictsForSolutions Verdict { get; set; }
        public int AuthorId { get; set; }
        public int TaskId { get; set; }
        public Task Task { get; set; }
    }
}
