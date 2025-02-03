using AutoMapper;
using tea_leaves.Models;
namespace tea_leaves
{
    public class MappingProfile:Profile
    {
        public MappingProfile()
        {
            CreateMap<Products, ProductDTO>();
            CreateMap<Contact, ContactDTO>();
        }
    }

}
