INSERT INTO Restaurants VALUES ('Restaurant1', 'Italli', 'Karlshamn')
INSERT INTO Restaurants VALUES ('Restaurant2', 'Michaelangelo', 'Karlskrona')
INSERT INTO Restaurants VALUES ('Restaurant3', 'Jensens', 'Malmö')


-- Italli 10st bord
INSERT INTO Tables VALUES (1, 'Table-1', 2, 'Restaurant1')
INSERT INTO Tables VALUES (2, 'Table-2', 4, 'Restaurant1')
INSERT INTO Tables VALUES (3, 'Table-3', 6, 'Restaurant1')
INSERT INTO Tables VALUES (4, 'Table-4', 8, 'Restaurant1')
INSERT INTO Tables VALUES (5, 'Table-5', 10, 'Restaurant1')
INSERT INTO Tables VALUES (6, 'Table-6', 12, 'Restaurant1')
INSERT INTO Tables VALUES (7, 'Table-7', 14, 'Restaurant1')
INSERT INTO Tables VALUES (8, 'Table-8', 16, 'Restaurant1')
INSERT INTO Tables VALUES (9, 'Table-9', 16, 'Restaurant1')
INSERT INTO Tables VALUES (91, 'Table-10', 16, 'Restaurant1')


-- Jensens 4st bord
INSERT INTO Tables VALUES (92, 'Bord-1-Jensens', 2, 'Restaurant3')
INSERT INTO Tables VALUES (93, 'Bord-2-Jensens', 4, 'Restaurant3')
INSERT INTO Tables VALUES (94, 'Bord-3-Jensens', 6, 'Restaurant3')
INSERT INTO Tables VALUES (95, 'Bord-4-Jensens', 8, 'Restaurant3')

-- Bokningar Italli
INSERT INTO Bookings VALUES (1, '20250618 10:34','20250718 18:00','20250718 23:00', 'Robin', 'Robin@domain.com', '0760373111', 'Mycket mat tack','4', 0, 0, 0, 0, 4, 2)
INSERT INTO Bookings VALUES (2, '20250216 10:00','20250218 11:00','20250218 13:00', 'Test', 'test@domain.com', '0760373111', 'Mycket mat tack','4', 1, 1, 1, 0, 0, 0)
INSERT INTO Bookings VALUES (3, '20250302 15:34','20250102 15:00','20250102 18:00', 'Rasmus', 'Rasmus@domain.com', '0760373111', 'Mycket mat tack','2', 0, 0, 0, 0, 4, 2)
INSERT INTO Bookings VALUES (4, '20250918 20:42','20250919 19:00','20250919 22:00', 'Micke', 'Micke@domain.com', '0760373111', 'Mycket mat tack','2', 1, 1, 1, 2, 2, 2)
INSERT INTO Bookings VALUES (5, '20250418 11:20','20250418 14:00','20250418 15:00', 'Linus', 'Linus@domain.com', '0760373111', 'Mycket mat tack','6', 0, 0, 0, 0, 4, 2)
INSERT INTO Bookings VALUES (6, '20251108 23:34','20251110 18:00','20251110 20:00', 'Alfons', 'Alfons@domain.com', '0760373111', 'Mycket mat tack','7', 0, 0, 0, 0, 0, 0)



-- Plocka fram en hel bokning
SELECT
    b.Id,
    b.BookerName,
    b.BookerEmail,
    b.BookerPhone,
    b.SpecialRequests,
    b.BookingStartTime,
    b.BookingEndTime,
    b.Eggs,
    b.Gluten,
    b.Milk,
    b.Lactose,
    b.Vegan,
    b.Vegetarian,
    t.Name,
    t.RestaurantId
FROM 
    Bookings b
JOIN 
    Tables t ON  b.TableId = t.Id
WHERE 
    b.Id = 1;