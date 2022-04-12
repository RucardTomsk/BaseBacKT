namespace MainLABAPI.Data.Models.DB
{
    public class Topic
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public int? ParentId { get; set; }

        public List<Task> Tasks { get; set; }
    }
}
