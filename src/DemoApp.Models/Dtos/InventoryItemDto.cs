using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DemoApp.Models.Dtos
{
    public class InventoryItemDto
    {
        public Guid Id { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public int RemaningCount { get; set; }
        public DateTime ExpirationDate { get; set; }
    }
}
