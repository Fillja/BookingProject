using Infrastructure.Entities;
using Infrastructure.Models;

namespace Infrastructure.Factories;

public static class EntityFactory
{
    /* RESTAURANTS */

    //Used when getting a restaurant or restaurants
    public static RestaurantModel PopulateRestaurantModel(RestaurantEntity restaurantEntity)
    {
        return new RestaurantModel
        {
            Id = restaurantEntity.Id,
            Name = restaurantEntity.Name,
            Location = restaurantEntity.Location
        };
    }


    /* TABLES */

    //Used when creating a table
    public static TableEntity PopulateTableEntity(RestaurantEntity restaurantEntity, TableModel tableModel)
    {
        return (new TableEntity
        {
            Name = tableModel.Name,
            Size = tableModel.Size,
            Restaurant = restaurantEntity,
            RestaurantId = restaurantEntity.Id,
            Bookings = []
        });
    }

    //Used when getting a table or tables
    public static TableModel PopulateTableModel(TableEntity tableEntity)
    {
        return new TableModel
        {
            Id = tableEntity.Id,
            RestaurantId = tableEntity.RestaurantId,
            Name = tableEntity.Name,
            Size = tableEntity.Size,
            Bookings = tableEntity.Bookings?.Select(booking => new BookingSlotModel
            {
                Id = booking.Id,
                BookerName = booking.BookerName,
                BookerEmail = booking.BookerEmail,
                BookingStartTime = booking.BookingStartTime,
                BookingEndTime = booking.BookingEndTime,
            }).ToList() ?? new List<BookingSlotModel>()
        };
    }

    //Used when updating a table
    public static TableEntity PopulateTableEntity(TableEntity tableEntity, TableModel tableModel)
    {
        tableEntity.Name = tableModel.Name;
        tableEntity.Size = tableModel.Size;

        return tableEntity;
    }


    /* BOOKINGS */

    //Used when creating a booking
    public static BookingEntity PopulateBookingEntity(BookingModel bookingModel, TableEntity tableEntity)
    {
        return (new BookingEntity
        {
            CreatedDate = bookingModel.CreatedDate,
            BookingStartTime = bookingModel.BookingStartTime,
            BookingEndTime = bookingModel.BookingEndTime,
            BookerName = bookingModel.BookerName,
            BookerEmail = bookingModel.BookerEmail,
            BookerPhone = bookingModel.BookerPhone,
            Vegan = bookingModel.Vegan,
            Vegetarian = bookingModel.Vegetarian,
            Lactose = bookingModel.Lactose,
            Milk = bookingModel.Milk,
            Eggs = bookingModel.Eggs,
            Gluten = bookingModel.Gluten,
            SpecialRequests = bookingModel.SpecialRequests,
            TableId = tableEntity.Id,
            Table = tableEntity
        });
    }

    //Used when getting a booking or bookings
    public static BookingModel PopulateBookingModel(BookingEntity bookingEntity)
    {
        return new BookingModel
        {
            Id = bookingEntity.Id,
            CreatedDate = bookingEntity.CreatedDate,
            BookingStartTime = bookingEntity.BookingStartTime,
            BookingEndTime = bookingEntity.BookingEndTime,
            BookerName = bookingEntity.BookerName,
            BookerEmail = bookingEntity.BookerEmail,
            BookerPhone = bookingEntity.BookerPhone,
            Vegan = bookingEntity.Vegan,
            Vegetarian = bookingEntity.Vegetarian,
            Lactose = bookingEntity.Lactose,
            Milk = bookingEntity.Milk,
            Eggs = bookingEntity.Eggs,
            Gluten = bookingEntity.Gluten,
            SpecialRequests = bookingEntity.SpecialRequests,
            TableId = bookingEntity.TableId,
        };
    }

    //Used when updating a booking
    public static BookingEntity PopulateBookingEntity(BookingEntity bookingEntity, BookingModel bookingModel)
    {
        bookingEntity.BookingStartTime = bookingModel.BookingStartTime;
        bookingEntity.BookingEndTime = bookingModel.BookingEndTime;
        bookingEntity.BookerName = bookingModel.BookerName;
        bookingEntity.BookerEmail = bookingModel.BookerEmail;
        bookingEntity.BookerPhone = bookingModel.BookerPhone;
        bookingEntity.Vegan = bookingModel.Vegan;
        bookingEntity.Vegetarian = bookingModel.Vegetarian;
        bookingEntity.Lactose = bookingModel.Lactose;
        bookingEntity.Milk = bookingModel.Milk;
        bookingEntity.Eggs = bookingModel.Eggs;
        bookingEntity.Gluten = bookingModel.Gluten;
        bookingEntity.SpecialRequests = bookingModel.SpecialRequests;
        return bookingEntity;
    }
}
