
namespace DatabaseTest.Database.Entities
{
    public class ProductEntity
    {
        public long Id { get; set; }
        public string Name { get; set; }
        public double Price { get; set; }

        public CheckEntity Check { get; set; }
        public long? CheckFK { get; set; }
    }
}
