using Infrastructure.Entities;
using Infrastructure.Models;

namespace Infrastructure.Factories;

public static class EntityFactory
{
    //Used when creating a chair
    public static ChairEntity PopulateChairEntity(RestaurantEntity restaurantEntity, ChairModel chairModel)
    {
        return (new ChairEntity
        {
            Name = chairModel.Name,
            Restaurant = restaurantEntity,
            RestaurantId = restaurantEntity.Id,
            Vegan = chairModel.Vegan,
            Vegetarian = chairModel.Vegetarian,
            Gluten = chairModel.Gluten,
            Milk = chairModel.Milk,
            Eggs = chairModel.Egg,
        });
    }

    //Used when updating a chair
    public static ChairEntity PopulateChairEntity(ChairEntity chairEntity, ChairUpdateModel chairUpdateModel)
    {
        chairEntity.Name = chairUpdateModel.Name;
        chairEntity.Vegan = chairUpdateModel.Vegan;
        chairEntity.Vegetarian = chairUpdateModel.Vegetarian;
        chairEntity.Gluten = chairUpdateModel.Gluten;
        chairEntity.Milk = chairUpdateModel.Milk;
        chairEntity.Eggs = chairUpdateModel.Egg;

        return chairEntity;
    }

    //Used when creating a table
    public static TableEntity PopulateTableEntity(RestaurantEntity restaurantEntity, TableModel tableModel)
    {
        return (new TableEntity
        {
            Name = tableModel.Name,
            Size = tableModel.Size,
            IsBooked = tableModel.IsBooked,
            Restaurant = restaurantEntity,
            RestaurantId = restaurantEntity.Id,
        });
    }

    //Used when updating a table
    public static TableEntity PopulateTableEntity(TableEntity tableEntity, TableUpdateModel tableUpdateModel)
    {
        tableEntity.Name = tableUpdateModel.Name;
        tableEntity.Size = tableUpdateModel.Size;
        tableEntity.IsBooked = tableUpdateModel.IsBooked;

        return tableEntity;
    }

    //Used when creating a booking
    public static BookingEntity PopulateBookingEntity(SeatingEntity seatingEntity, BookingModel bookingModel)
    {
        return (new BookingEntity
        {
            BookingStartTime = bookingModel.BookingStartTime,
            BookingEndTime = bookingModel.BookingStartTime.AddHours(6),
            BookerName = bookingModel.BookerName,
            BookerEmail = bookingModel.BookerEmail,
            BookerPhone = bookingModel.BookerPhone,
            SpecialRequests = bookingModel.SpecialRequests,
            SeatingId = seatingEntity.Id,
            Seating = seatingEntity
        });
    }
}
