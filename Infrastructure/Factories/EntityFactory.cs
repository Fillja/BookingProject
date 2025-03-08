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
    public static BookingEntity PopulateBookingEntity(SeatingEntity seatingEntity, BookingMinimalModel bookingMinimalModel)
    {
        return (new BookingEntity
        {
            BookingStartTime = bookingMinimalModel.BookingStartTime,
            BookingEndTime = bookingMinimalModel.BookingStartTime.AddHours(6),
            BookerName = bookingMinimalModel.BookerName,
            BookerEmail = bookingMinimalModel.BookerEmail,
            BookerPhone = bookingMinimalModel.BookerPhone,
            SpecialRequests = bookingMinimalModel.SpecialRequests,
            SeatingId = seatingEntity.Id,
            Seating = seatingEntity
        });
    }

    //Used when updating a booking
    public static BookingEntity PopulateBookingEntity(BookingEntity bookingEntity, BookingMinimalModel bookingMinimalModel) 
    {
        bookingEntity.BookingStartTime = bookingMinimalModel.BookingStartTime;
        bookingEntity.BookingEndTime = bookingMinimalModel.BookingStartTime.AddHours(6);
        bookingEntity.BookerName = bookingMinimalModel.BookerName;
        bookingEntity.BookerEmail = bookingMinimalModel.BookerEmail;
        bookingEntity.BookerPhone = bookingMinimalModel.BookerPhone;
        bookingEntity.SpecialRequests = bookingMinimalModel.SpecialRequests;

        return bookingEntity;
    }

    //Used when return one complete booking
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
