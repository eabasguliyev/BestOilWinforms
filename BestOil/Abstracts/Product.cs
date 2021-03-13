namespace BestOil.Abstracts
{
    public abstract class Product:Id
    {
        public string Name { get; set; }
        public double Price { get; set; }
    }
}