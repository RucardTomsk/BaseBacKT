namespace MainLABAPI.Data.Models.DTO
{
    public class ExtendedModelForTaskData
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public int TopicId { get; set; }
        public string Description { get; set; }
        public bool? IsDraft { get; set; }
    }
}
