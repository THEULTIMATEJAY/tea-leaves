namespace tea_leaves.Models
{
    public class UpdateProductRequest
    {
        public string Prod_name { get; set; }
        public float Price { get; set; }
        public int? Quantity { get; set; }
        public string[] Toppings { get; set; }
        public string? IceLevel { get; set; }
        public string[] Temperature { get; set; }
        public string? Description { get; set; }
        public int Stock { get; set; }
        public string? ImageFile { get; set; }
    }
}
