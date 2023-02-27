
namespace DatabaseTest.Database.Entities
{
    public class CheckEntity
    {
        public CheckEntity()
        {
            Products = new List<ProductEntity>();
        }
        public long Id { get; set; }
        public DateTime DateBuy { get; set; }
      
        public ICollection<ProductEntity> Products { get; set; }

        public BuyerEntity Buyer { get; set; }
        public long BuyerFK { get; set; }
    }
}
