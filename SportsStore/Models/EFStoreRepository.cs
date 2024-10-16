namespace SportsStore.Models
{
    public class EFStoreRepository : IStoreRepository
    {
        private readonly StoreDbContext context;

        public EFStoreRepository(StoreDbContext ctx)
        {
            if (ctx is null)
            {
                throw new ArgumentNullException(nameof(ctx));
            }

            this.context = ctx;
        }

        public IQueryable<Product> Products => this.context.Products;

        public void CreateProduct(Product p)
        {
            this.context.Add(p);
            this.context.SaveChanges();
        }

        public void DeleteProduct(Product p)
        {
            this.context.Remove(p);
            this.context.SaveChanges();
        }

        public void SaveProduct(Product p)
        {
            this.context.SaveChanges();
        }
    }
}
