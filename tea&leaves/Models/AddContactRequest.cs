namespace tea_leaves.Models
{
    public class AddContactRequest
    {
        public string Name { get; set; }
        public string Email { get; set; }
        public string? Number { get; set; }
        public string Message { get; set; }
        public string? Location { get; set; }
    }
}
