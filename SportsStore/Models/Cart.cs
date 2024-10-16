namespace SportsStore.Models;

public class Cart
{
#pragma warning disable CA1002 // Do not expose generic lists
#pragma warning disable CA2227 // Collection properties should be read only
    public List<CartLine> Lines { get; set; } = new List<CartLine>();
#pragma warning restore CA2227 // Collection properties should be read only
#pragma warning restore CA1002 // Do not expose generic lists

    public virtual void AddItem(Product product, int quantity)
    {
        CartLine? line = this.Lines.Find(p => p.Product.ProductID == product.ProductID);
        if (line == null)
        {
            this.Lines.Add(new CartLine
            {
                Product = product,
                Quantity = quantity,
            });
        }
        else
        {
            line.Quantity += quantity;
        }
    }

    public virtual void RemoveLine(Product product) => this.Lines.RemoveAll(l => l.Product.ProductID == product.ProductID);

    public decimal ComputeTotalValue() => this.Lines.Sum(e => e.Product.Price * e.Quantity);

    public virtual void Clear() => this.Lines.Clear();
}

public class CartLine
{
    public int CartLineID { get; set; }

    public Product Product { get; set; } = new();

    public int Quantity { get; set; }
}
