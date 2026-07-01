using System;
using System.Collections.Generic;
using System.Text;

namespace DomainModel
{
    public sealed class CategoryDto : IEntity
    {
        public int Id { get; set; }
        public string Name { get; set; } = string.Empty;
        public string HexColor { get; set; } = string.Empty;
    }
}
