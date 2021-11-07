using System.ComponentModel.DataAnnotations;

namespace Database.CatalogDb.EFCore.Entities
{
    public class ProductEntity
    {
        [Key]
        public int Id { get; set; }

        [StringLength(100)]
        public string Name { get; set; }

        [StringLength(100)]
        public string ImgUri { get; set; }

        public decimal Price { get; set; }

        [StringLength(500)]
        public string? Description { get; set; }
    }
}
