using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RepositoryLayer.Entities
{
    public class Book
    {
        public int Id { get; set; }

        public string Title { get; set; }

        public long Price { get; set; }

        public string Author { get; set; }

        public string Description { get; set; }

        public int Quantity { get; set; }

        public string Image { get; set; }
    }
}
