//using Infrastructure.Contexts;
//using Microsoft.EntityFrameworkCore;

//namespace Infrastructure.Helpers;

//public class SqlHelper(DataContext context)
//{
//    private readonly DataContext _context = context;

//    public async Task DeleteTableAsync(string tableId)
//    {
//        using var transaction = await _context.Database.BeginTransactionAsync();

//        try
//        {
//            // Step 1: Get all related entities for the table
//            var tableChairs = await _context.TablesChairs
//                .Where(tc => tc.TableId == tableId)
//                .ToListAsync();

//            var seatingIds = await _context.Seatings
//                .Where(s => tableChairs.Select(tc => tc.Id).Contains(s.TableChairId))
//                .Select(s => s.Id)
//                .ToListAsync();

//            var bookings = await _context.Bookings
//                .Where(b => seatingIds.Contains(b.SeatingId))
//                .ToListAsync();

//            // Step 2: Delete dependent entities
//            _context.Bookings.RemoveRange(bookings);
//            await _context.SaveChangesAsync();

//            var seatings = await _context.Seatings
//                .Where(s => seatingIds.Contains(s.Id))
//                .ToListAsync();
//            _context.Seatings.RemoveRange(seatings);
//            await _context.SaveChangesAsync();

//            _context.TablesChairs.RemoveRange(tableChairs);
//            await _context.SaveChangesAsync();

//            // Step 3: Delete the table itself
//            var table = await _context.Tables.FirstOrDefaultAsync(t => t.Id == tableId);
//            if (table != null)
//            {
//                _context.Tables.Remove(table);
//                await _context.SaveChangesAsync();
//            }

//            // Step 4: Commit the transaction
//            await transaction.CommitAsync();
//        }
//        catch (Exception ex)
//        {
//            // Rollback the transaction in case of any failure
//            await transaction.RollbackAsync();
//            throw new Exception("Failed to delete table", ex);
//        }
//    }
//}
