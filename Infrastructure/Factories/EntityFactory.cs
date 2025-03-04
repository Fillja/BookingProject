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
    public static ChairEntity PopulateChairEntity(ChairEntity chairEntity, ChairModel chairModel)
    {
        chairEntity.Name = chairModel.Name;
        chairEntity.Vegan = chairModel.Vegan;
        chairEntity.Vegetarian = chairModel.Vegetarian;
        chairEntity.Gluten = chairModel.Gluten;
        chairEntity.Milk = chairModel.Milk;
        chairEntity.Eggs = chairModel.Egg;

        return chairEntity;
    }

    //Used when booking a chair
    public static ChairEntity PopulateChairEntity(ChairEntity chairEntity, ChairBookingModel chairUpdateModel)
    {
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
    public static TableEntity PopulateTableEntity(TableEntity tableEntity, TableModel tableModel)
    {
        tableEntity.Name = tableModel.Name;
        tableEntity.Size = tableModel.Size;
        tableEntity.IsBooked = tableModel.IsBooked;

        return tableEntity;
    }

    //Used when creating a booking
    public static BookingEntity PopulateBookingEntity(SeatingEntity seatingEntity, BookingCreateModel bookingCreateModel)
    {
        return (new BookingEntity
        {
            BookingStartTime = bookingCreateModel.BookingStartTime,
            BookingEndTime = bookingCreateModel.BookingStartTime.AddHours(6),
            BookerName = bookingCreateModel.BookerName,
            BookerEmail = bookingCreateModel.BookerEmail,
            BookerPhone = bookingCreateModel.BookerPhone,
            SpecialRequests = bookingCreateModel.SpecialRequests,
            SeatingId = seatingEntity.Id,
            Seating = seatingEntity
        });
    }

    public static BookingModel PopulateBookingModel(BookingEntity bookingEntity, SeatingModel seatingModel) 
    {
        return new BookingModel
        {
            CreatedDate = bookingEntity.CreatedDate,
            BookingStartTime = bookingEntity.BookingStartTime,
            BookingEndTime = bookingEntity.BookingEndTime,
            BookerName = bookingEntity.BookerName,
            BookerEmail = bookingEntity.BookerEmail,
            BookerPhone = bookingEntity.BookerPhone,
            SpecialRequests = bookingEntity.SpecialRequests,
            Seating = seatingModel
        };
    }
}
