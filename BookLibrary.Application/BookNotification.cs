using BookLibrary.Application.Interfaces.Events;
using BookLibrary.Core.Entities;

namespace BookLibrary.Application
{
    public class BookNotification : IBookNotification
    {
        /// <summary>
        /// We could use a Message Broker like RabbitMQ and Kafka. We can also use queues like Microsoft Service Bus, and AWS SQS.
        /// </summary>
        /// <param name="newBook">The added book</param>
        public void OnBookAdded(Book newBook)
        {
            Console.WriteLine();
            Console.WriteLine("===========New Book added===========");
            Console.WriteLine($"Title: {newBook.Title}");
            Console.WriteLine($"Category: {newBook.Category}");
            Console.WriteLine("====================================");
        }
    }
}
