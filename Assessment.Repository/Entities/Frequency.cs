using Assessment.Repository.Interfaces;

namespace Assessment.Repository.Entities
{
    public class Frequency : IFrequency
    {

        public int Count { get; set; }
        public string Value { get; set; }
    }
   
}
