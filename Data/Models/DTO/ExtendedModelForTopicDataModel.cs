﻿namespace MainLABAPI.Data.Models.DTO
{
    public class ExtendedModelForTopicDataModel
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public int? ParentId { get; set; }
        public ModelForTopicDataInIndexMethodModel? Childs { get; set; }
    }
}
