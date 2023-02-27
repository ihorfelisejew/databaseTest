
namespace DatabaseTest.Database.Entities
{
    public class BuyerEntity
    {
        public BuyerEntity()
        {
            Checks = new List<CheckEntity>();
        }

        public long Id { get; set; }
        public string Name { get; set; }
        public string Surname { get; set; }
        public string Phone { get; set; }
        public DateTime BirthDate { get; set; }

        public ICollection<CheckEntity> Checks { get; set; }
    }
}
