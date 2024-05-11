using BookLibrary.Core.Entities;

namespace BookLibrary.Application.Interfaces.Events
{
    public interface IBookNotification
    {
        void OnBookAdded(Book newBook);
    }
}
