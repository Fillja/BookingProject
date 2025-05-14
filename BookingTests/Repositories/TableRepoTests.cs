using Infrastructure.Contexts;
using Infrastructure.Entities;
using Infrastructure.Repositories;
using Microsoft.EntityFrameworkCore;

namespace BookingTests.Repositories;

public class TableRepoTests
{
    private readonly DataContext _context =
        new(new DbContextOptionsBuilder<DataContext>()
        .UseInMemoryDatabase($"{Guid.NewGuid()}")
        .Options);

    [Fact]
    public async void CreateTableShould_SaveTableToDb()
    {
        //Arrange  
        var _tableRepository = new TableRepository(_context);
        var tableEntity = new TableEntity
        {
            Id = Guid.NewGuid().ToString(),
            Name = "Table 1",
            Size = 4,
            RestaurantId = "Restaurant 1",
            LeftAlignment = 0.55F,
            TopAlignment = 0.55F
        };


        //Act
        var createResult = await _tableRepository.CreateAsync(tableEntity);
        var createdTable = createResult.Content as TableEntity;


        //Assert
        Assert.Equal(0, createResult.StatusCode);
        Assert.NotNull(createdTable);
        Assert.Equal(tableEntity.Id, createdTable?.Id);
    }

    [Fact]
    public async void GetAllTablesShould_ReturnAllTables()
    {
        //Arrange
        var _tableRepository = new TableRepository(_context);
        var tableEntity1 = new TableEntity
        {
            Id = Guid.NewGuid().ToString(),
            Name = "Table 1",
            Size = 4,
            RestaurantId = "Restaurant 1",
            LeftAlignment = 0.55F,
            TopAlignment = 0.55F,
            Bookings = new List<BookingEntity>
            {
                new BookingEntity
                {
                    Id = Guid.NewGuid().ToString(),
                    BookerName = "Test",
                    BookerEmail = "test@domain.com",
                    BookerPhone = "123456789",
                    BookingStartTime = DateTime.Now,
                    BookingEndTime = DateTime.Now.AddHours(2),
                    TableId = ""
                }
            }
        };
        var tableEntity2 = new TableEntity
        {
            Id = Guid.NewGuid().ToString(),
            Name = "Table 2",
            Size = 4,
            RestaurantId = "Restaurant 2",
            LeftAlignment = 0.55F,
            TopAlignment = 0.55F
        };

        await _tableRepository.CreateAsync(tableEntity1);
        await _tableRepository.CreateAsync(tableEntity2);


        //Act
        var getResult = await _tableRepository.GetAllAsync();
        var tableList = getResult.Content as IEnumerable<TableEntity>;
        var firstElement = tableList?.FirstOrDefault();


        //Assert
        Assert.Equal(0, getResult.StatusCode);
        Assert.NotNull(tableList);
        Assert.Equal(2, tableList?.Count());
        Assert.True(firstElement!.Bookings?.Count == 1);
    }

    [Fact]
    public async void GetAllTablesInRestaurantShould_ReturnTablesInSpecificRestaurant()
    {
        //Arrange
        var _tableRepository = new TableRepository(_context);
        var tableEntity1 = new TableEntity
        {
            Id = Guid.NewGuid().ToString(),
            Name = "Table 1",
            Size = 4,
            RestaurantId = "Restaurant 1",
            LeftAlignment = 0.55F,
            TopAlignment = 0.55F
        };
        var tableEntity2 = new TableEntity
        {
            Id = Guid.NewGuid().ToString(),
            Name = "Table 2",
            Size = 4,
            RestaurantId = "Restaurant 1",
            LeftAlignment = 0.55F,
            TopAlignment = 0.55F,
            Bookings = new List<BookingEntity>
            {
                new BookingEntity
                {
                    Id = Guid.NewGuid().ToString(),
                    BookerName = "Test",
                    BookerEmail = "test@domain.com",
                    BookerPhone = "123456789",
                    BookingStartTime = DateTime.Now,
                    BookingEndTime = DateTime.Now.AddHours(2),
                    TableId = ""
                }
            }
        };

        await _tableRepository.CreateAsync(tableEntity1);
        await _tableRepository.CreateAsync(tableEntity2);


        //Act
        var getResult = await _tableRepository.GetAllAsync("Restaurant 1");
        var tableList = getResult.Content as IEnumerable<TableEntity>;
        var secondElement = tableList?.Skip(1).FirstOrDefault();


        //Assert
        Assert.Equal(0, getResult.StatusCode);
        Assert.NotNull(tableList);
        Assert.Equal(2, tableList?.Count());
        Assert.Equal("Restaurant 1", tableList?.FirstOrDefault()?.RestaurantId);
        Assert.True(secondElement!.Bookings?.Count == 1);
    }

    [Fact]
    public async void GetTableByIdShould_ReturnTableById()
    {
        //Arrange
        var _tableRepository = new TableRepository(_context);
        var tableEntity = new TableEntity
        {
            Id = Guid.NewGuid().ToString(),
            Name = "Table 1",
            Size = 4,
            RestaurantId = "Restaurant 1",
            LeftAlignment = 0.55F,
            TopAlignment = 0.55F,
            Bookings = new List<BookingEntity>
            {
                new BookingEntity
                {
                    Id = Guid.NewGuid().ToString(),
                    BookerName = "Test",
                    BookerEmail = "test@domain.com",
                    BookerPhone = "123456789",
                    BookingStartTime = DateTime.Now,
                    BookingEndTime = DateTime.Now.AddHours(2),
                    TableId = ""
                }
            }
        };
        await _tableRepository.CreateAsync(tableEntity);


        //Act
        var getResult = await _tableRepository.GetOneAsync(t => t.Id == tableEntity.Id);
        var returnedTable = getResult.Content as TableEntity;


        //Assert
        Assert.Equal(0, getResult.StatusCode);
        Assert.NotNull(returnedTable);
        Assert.Equal(tableEntity.Id, returnedTable?.Id);
        Assert.Equal(1, returnedTable?.Bookings?.Count);
    }

    [Fact]
    public async void UpdateTableShould__UpdateTableInDb()
    {
        //Arrange
        var _tableRepository = new TableRepository(_context);
        var tableEntity = new TableEntity
        {
            Id = Guid.NewGuid().ToString(),
            Name = "Table 1",
            Size = 4,
            RestaurantId = "Restaurant 1",
            LeftAlignment = 0.55F,
            TopAlignment = 0.55F
        };
        await _tableRepository.CreateAsync(tableEntity);


        //Act
        tableEntity.Name = "Updated Table";
        var updateResult = await _tableRepository.UpdateAsync(tableEntity);
        var updatedTable = updateResult.Content as TableEntity;


        //Assert
        Assert.Equal(0, updateResult.StatusCode);
        Assert.NotNull(updatedTable);
        Assert.Equal("Updated Table", updatedTable?.Name);
    }

    [Fact]
    public async void DeleteTableShould_DeleteTableFromDb()
    {
        //Arrange
        var _tableRepository = new TableRepository(_context);
        var tableEntity = new TableEntity
        {
            Id = Guid.NewGuid().ToString(),
            Name = "Table 1",
            Size = 4,
            RestaurantId = "Restaurant 1",
            LeftAlignment = 0.55F,
            TopAlignment = 0.55F
        };
        await _tableRepository.CreateAsync(tableEntity);

        //Act
        var deleteResult = await _tableRepository.DeleteAsync(x => x.Id == tableEntity.Id);
        var getResult = await _tableRepository.GetOneAsync(t => t.Id == tableEntity.Id);

        //Assert
        Assert.Equal(0, deleteResult.StatusCode);
        Assert.Equal(2, getResult.StatusCode);
        Assert.Null(getResult.Content);
    }
}
