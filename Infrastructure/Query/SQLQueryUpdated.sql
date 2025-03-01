INSERT INTO Restaurants VALUES ('Restaurant1', 'Italli', 'Karlshamn')
INSERT INTO Restaurants VALUES ('Restaurant2', 'Michaelangelo', 'Karlskrona')
INSERT INTO Restaurants VALUES ('Restaurant3', 'Jensens', 'Malmö')


-- Italli 4st bord, 2man, 4man, 6man, 8man
INSERT INTO Tables VALUES (1, 'Bord-1-Italli', 2, 0, 'Restaurant1')
INSERT INTO Tables VALUES (2, 'Bord-2-Italli', 4, 0, 'Restaurant1')
INSERT INTO Tables VALUES (3, 'Bord-3-Italli', 6, 0, 'Restaurant1')
INSERT INTO Tables VALUES (4, 'Bord-4-Italli', 8, 0, 'Restaurant1')

-- Michaelangelo 4st bord, 2man, 4man, 6man, 8man
INSERT INTO Tables VALUES (5, 'Bord-1-Michaelangelo', 2, 0, 'Restaurant2')
INSERT INTO Tables VALUES (6, 'Bord-2-Michaelangelo', 4, 0, 'Restaurant2')
INSERT INTO Tables VALUES (7, 'Bord-3-Michaelangelo', 6, 0, 'Restaurant2')
INSERT INTO Tables VALUES (8, 'Bord-4-Michaelangelo', 8, 0, 'Restaurant2')

-- Jenses 4st bord, 2man, 4man, 6man, 8man
INSERT INTO Tables VALUES (9, 'Bord-1-Jensens', 2, 0, 'Restaurant3')
INSERT INTO Tables VALUES (91, 'Bord-2-Jensens', 4, 0, 'Restaurant3')
INSERT INTO Tables VALUES (92, 'Bord-3-Jensens', 6, 0, 'Restaurant3')
INSERT INTO Tables VALUES (93, 'Bord-4-Jensens', 8, 0, 'Restaurant3')


-- Italli 8st stolar
INSERT INTO Chairs VALUES (11, 'Stol-1-Italli', 1, 0, 0, 0, 0, 'Restaurant1')
INSERT INTO Chairs VALUES (12, 'Stol-2-Italli', 0, 1, 0, 0, 0, 'Restaurant1')
INSERT INTO Chairs VALUES (13, 'Stol-3-Italli', 0, 0, 1, 0, 0, 'Restaurant1')
INSERT INTO Chairs VALUES (14, 'Stol-4-Italli', 0, 0, 0, 1, 0, 'Restaurant1')
INSERT INTO Chairs VALUES (15, 'Stol-5-Italli', 0, 0, 0, 0, 1, 'Restaurant1')
INSERT INTO Chairs VALUES (16, 'Stol-6-Italli', 0, 0, 0, 0, 0, 'Restaurant1')
INSERT INTO Chairs VALUES (17, 'Stol-7-Italli', 0, 0, 0, 0, 0, 'Restaurant1')
INSERT INTO Chairs VALUES (18, 'Stol-8-Italli', 0, 0, 0, 0, 0, 'Restaurant1')

-- Michaelangelo 8st stolar
INSERT INTO Chairs VALUES (21, 'Stol-1-Michaelangelo', 1, 0, 0, 0, 0, 'Restaurant2')
INSERT INTO Chairs VALUES (22, 'Stol-2-Michaelangelo', 0, 1, 0, 0, 0, 'Restaurant2')
INSERT INTO Chairs VALUES (23, 'Stol-3-Michaelangelo', 0, 0, 1, 0, 0, 'Restaurant2')
INSERT INTO Chairs VALUES (24, 'Stol-4-Michaelangelo', 0, 0, 0, 1, 0, 'Restaurant2')
INSERT INTO Chairs VALUES (25, 'Stol-5-Michaelangelo', 0, 0, 0, 0, 1, 'Restaurant2')
INSERT INTO Chairs VALUES (26, 'Stol-6-Michaelangelo', 0, 0, 0, 0, 0, 'Restaurant2')
INSERT INTO Chairs VALUES (27, 'Stol-7-Michaelangelo', 0, 0, 0, 0, 0, 'Restaurant2')
INSERT INTO Chairs VALUES (28, 'Stol-8-Michaelangelo', 0, 0, 0, 0, 0, 'Restaurant2')

-- Jensens 8st stolar
INSERT INTO Chairs VALUES (31, 'Stol-1-Jensens', 1, 0, 0, 0, 0, 'Restaurant3')
INSERT INTO Chairs VALUES (32, 'Stol-2-Jensens', 0, 1, 0, 0, 0, 'Restaurant3')
INSERT INTO Chairs VALUES (33, 'Stol-3-Jensens', 0, 0, 1, 0, 0, 'Restaurant3')
INSERT INTO Chairs VALUES (34, 'Stol-4-Jensens', 0, 0, 0, 1, 0, 'Restaurant3')
INSERT INTO Chairs VALUES (35, 'Stol-5-Jensens', 0, 0, 0, 0, 1, 'Restaurant3')
INSERT INTO Chairs VALUES (36, 'Stol-6-Jensens', 0, 0, 0, 0, 0, 'Restaurant3')
INSERT INTO Chairs VALUES (37, 'Stol-7-Jensens', 0, 0, 0, 0, 0, 'Restaurant3')
INSERT INTO Chairs VALUES (38, 'Stol-8-Jensens', 0, 0, 0, 0, 0, 'Restaurant3')

-- Italli Bord #4, 8st stolar
INSERT INTO Seatings VALUES ('Italli-8man-1', 'Italli-8man', 4, 11)
INSERT INTO Seatings VALUES ('Italli-8man-2', 'Italli-8man', 4, 12)
INSERT INTO Seatings VALUES ('Italli-8man-3', 'Italli-8man', 4, 13)
INSERT INTO Seatings VALUES ('Italli-8man-4', 'Italli-8man', 4, 14)
INSERT INTO Seatings VALUES ('Italli-8man-5', 'Italli-8man', 4, 15)
INSERT INTO Seatings VALUES ('Italli-8man-6', 'Italli-8man', 4, 16)
INSERT INTO Seatings VALUES ('Italli-8man-7', 'Italli-8man', 4, 17)
INSERT INTO Seatings VALUES ('Italli-8man-8', 'Italli-8man', 4, 18)

-- Michaelangelo Bord #8, 8st stolar
INSERT INTO Seatings VALUES ('Michaelangelo-8man-1', 'Michaelangelo-8man', 8, 21)
INSERT INTO Seatings VALUES ('Michaelangelo-8man-2', 'Michaelangelo-8man', 8, 22)
INSERT INTO Seatings VALUES ('Michaelangelo-8man-3', 'Michaelangelo-8man', 8, 23)
INSERT INTO Seatings VALUES ('Michaelangelo-8man-4', 'Michaelangelo-8man', 8, 24)
INSERT INTO Seatings VALUES ('Michaelangelo-8man-5', 'Michaelangelo-8man', 8, 25)
INSERT INTO Seatings VALUES ('Michaelangelo-8man-6', 'Michaelangelo-8man', 8, 26)
INSERT INTO Seatings VALUES ('Michaelangelo-8man-7', 'Michaelangelo-8man', 8, 27)
INSERT INTO Seatings VALUES ('Michaelangelo-8man-8', 'Michaelangelo-8man', 8, 28)

-- Jensens Bord #93, 8st stolar
INSERT INTO Seatings VALUES ('Jensens-8man-1', 'Jensens-8man', 93, 31)
INSERT INTO Seatings VALUES ('Jensens-8man-2', 'Jensens-8man', 93, 32)
INSERT INTO Seatings VALUES ('Jensens-8man-3', 'Jensens-8man', 93, 33)
INSERT INTO Seatings VALUES ('Jensens-8man-4', 'Jensens-8man', 93, 34)
INSERT INTO Seatings VALUES ('Jensens-8man-5', 'Jensens-8man', 93, 35)
INSERT INTO Seatings VALUES ('Jensens-8man-6', 'Jensens-8man', 93, 36)
INSERT INTO Seatings VALUES ('Jensens-8man-7', 'Jensens-8man', 93, 37)
INSERT INTO Seatings VALUES ('Jensens-8man-8', 'Jensens-8man', 93, 38)


-- Bokar 8man på Italli
INSERT INTO Bookings VALUES (1, '20120618 10:34','20120718 18:00','20120718 23:00', 'Robin', 'Robin@domain.com', '0760373111', 'Mycket mat tack','Italli-8man-1')

-- Plocka fram hela bokningen
SELECT 
    b.Id AS BookingId,
    b.BookerName,
    b.BookerEmail,
    b.BookerPhone,
    b.SpecialRequests,
    b.CreatedDate AS BookingCreatedDate,
    t.Id AS TableId,
    t.Size AS TableSize,
    t.IsBooked,
    r.Id AS RestaurantId,
    r.Name AS RestaurantName,
    r.Location AS RestaurantLocation,
    c.Id AS ChairId,
    c.Vegan,
    c.Vegetarian,
    c.Milk,
    c.Eggs,
    c.Gluten
FROM 
    Bookings b
JOIN 
    Seatings sBooked ON b.SeatingId = sBooked.Id
JOIN 
    Tables t ON sBooked.TableId = t.Id
JOIN 
    Restaurants r ON t.RestaurantId = r.Id
JOIN 
    Seatings s ON s.TableId = t.Id
JOIN 
    Chairs c ON s.ChairId = c.Id
WHERE 
    b.Id = 1;