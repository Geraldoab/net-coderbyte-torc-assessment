using System.Collections.Generic;

namespace BookLibrary.Core.Entities
{
    public class Publisher
    {
        public int Id { get; set; }

        public string Name { get; set; }

        public ICollection<Book> Books { get; set; } = new HashSet<Book>();
    }
}
