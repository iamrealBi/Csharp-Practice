using System.ComponentModel.DataAnnotations;

namespace project_1_taskflow.Models
{
    public class Category
    {
        public int Id { get; set; }

        [Required(ErrorMessage = "Tên category không được để trống")]
        [StringLength(50)]
        public string Name { get; set; }

        public string Color { get; set; }

        public List<TodoItem> TodoItems { get; set; }
    }
}
