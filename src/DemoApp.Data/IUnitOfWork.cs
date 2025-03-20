using DemoApp.Data.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DemoApp.Data
{
    public interface IUnitOfWork : IDisposable
    {
        IMembersRepository Members { get; }
        IInventoryRepository Inventory { get; }

        IBookingsRepository Bookings { get; }

        Task SaveChangesAsync();
    }
}
