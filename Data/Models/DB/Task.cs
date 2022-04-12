namespace MainLABAPI.Data.Models.DB
{
    public class Task
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public int TopicId { get; set; }
        public string Description { get; set; }
        public int Price { get; set; }

        public bool IsDraft { get; set; }

        public Topic Topic { get; set; }

        public List<Solution> Solutions { get; set; }
    }
}
