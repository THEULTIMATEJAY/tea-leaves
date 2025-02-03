namespace tea_leaves.Models
{
    public class ProductDTO
    {
        public int Prod_id { get; set; }
        public string Prod_name { get; set; }
        public float Price { get; set; }
        public int Stock { get; set; }
        public string Temperature { get; set; }
        public string Toppings { get; set; }
    }
}
