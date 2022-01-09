namespace Server.Persistence.Models
{
    public class AssociationColumn : Question
    {
        public string[] Fields { get; set; } 
        public string Answer { get; set; }
    }
}