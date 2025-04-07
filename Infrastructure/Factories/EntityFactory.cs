using Infrastructure.Entities;
using Infrastructure.Models;

namespace Infrastructure.Factories;

public static class EntityFactory
{
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
            RestaurantId = tableEntity.RestaurantId,
            Name = tableEntity.Name,
            Size = tableEntity.Size,
            Bookings = tableEntity.Bookings?.Select(booking => new BookingSlotModel
            {
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
    //public static BookingModel PopulateBookingModel(BookingEntity bookingEntity, SeatingModel seatingModel) 
    //{
    //    return new BookingModel
    //    {
    //        CreatedDate = bookingEntity.CreatedDate,
    //        BookingStartTime = bookingEntity.BookingStartTime,
    //        BookingEndTime = bookingEntity.BookingEndTime,
    //        BookerName = bookingEntity.BookerName,
    //        BookerEmail = bookingEntity.BookerEmail,
    //        BookerPhone = bookingEntity.BookerPhone,
    //        SpecialRequests = bookingEntity.SpecialRequests,
    //        Seating = seatingModel
    //    };
    //}
}
