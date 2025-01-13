INSERT INTO Restaurants VALUES ('Restaurant1', 'Italli', 'Karlshamn')
INSERT INTO Restaurants VALUES ('Restaurant2', 'Michaelangelo', 'Karlskrona')
INSERT INTO Restaurants VALUES ('Restaurant3', 'Jensens', 'Malmö')


-- Italli 4st bord, 2man, 4man, 6man, 8man
INSERT INTO Tables VALUES (1, 2, 0, 'Restaurant1')
INSERT INTO Tables VALUES (2, 4, 0, 'Restaurant1')
INSERT INTO Tables VALUES (3, 6, 0, 'Restaurant1')
INSERT INTO Tables VALUES (4, 8, 0, 'Restaurant1')

-- Michaelangelo 4st bord, 2man, 4man, 6man, 8man
INSERT INTO Tables VALUES (5, 2, 0, 'Restaurant2')
INSERT INTO Tables VALUES (6, 4, 0, 'Restaurant2')
INSERT INTO Tables VALUES (7, 6, 0, 'Restaurant2')
INSERT INTO Tables VALUES (8, 8, 0, 'Restaurant2')

-- Jenses 4st bord, 2man, 4man, 6man, 8man
INSERT INTO Tables VALUES (9, 2, 0, 'Restaurant3')
INSERT INTO Tables VALUES (91, 4, 0, 'Restaurant3')
INSERT INTO Tables VALUES (92, 6, 0, 'Restaurant3')
INSERT INTO Tables VALUES (93, 8, 0, 'Restaurant3')

-- Italli 8st stolar, 3 standard, 1 vegan, 1 vegetarian, 1 eggs, 1 milk, 1 gluten
INSERT INTO Chairs VALUES (11, 1, 0, 0, 0, 0, 'Restaurant1')
INSERT INTO Chairs VALUES (12, 0, 1, 0, 0, 0, 'Restaurant1')
INSERT INTO Chairs VALUES (13, 0, 0, 1, 0, 0, 'Restaurant1')
INSERT INTO Chairs VALUES (14, 0, 0, 0, 1, 0, 'Restaurant1')
INSERT INTO Chairs VALUES (15, 0, 0, 0, 0, 1, 'Restaurant1')
INSERT INTO Chairs VALUES (16, 0, 0, 0, 0, 0, 'Restaurant1')
INSERT INTO Chairs VALUES (17, 0, 0, 0, 0, 0, 'Restaurant1')
INSERT INTO Chairs VALUES (18, 0, 0, 0, 0, 0, 'Restaurant1')

-- Michaelangelo 8st stolar, 3 standard, 1 vegan, 1 vegetarian, 1 eggs, 1 milk, 1 gluten
INSERT INTO Chairs VALUES (21, 1, 0, 0, 0, 0, 'Restaurant2')
INSERT INTO Chairs VALUES (22, 0, 1, 0, 0, 0, 'Restaurant2')
INSERT INTO Chairs VALUES (23, 0, 0, 1, 0, 0, 'Restaurant2')
INSERT INTO Chairs VALUES (24, 0, 0, 0, 1, 0, 'Restaurant2')
INSERT INTO Chairs VALUES (25, 0, 0, 0, 0, 1, 'Restaurant2')
INSERT INTO Chairs VALUES (26, 0, 0, 0, 0, 0, 'Restaurant2')
INSERT INTO Chairs VALUES (27, 0, 0, 0, 0, 0, 'Restaurant2')
INSERT INTO Chairs VALUES (28, 0, 0, 0, 0, 0, 'Restaurant2')

-- Jensens 8st stolar, 3 standard, 1 vegan, 1 vegetarian, 1 eggs, 1 milk, 1 gluten
INSERT INTO Chairs VALUES (31, 1, 0, 0, 0, 0, 'Restaurant3')
INSERT INTO Chairs VALUES (32, 0, 1, 0, 0, 0, 'Restaurant3')
INSERT INTO Chairs VALUES (33, 0, 0, 1, 0, 0, 'Restaurant3')
INSERT INTO Chairs VALUES (34, 0, 0, 0, 1, 0, 'Restaurant3')
INSERT INTO Chairs VALUES (35, 0, 0, 0, 0, 1, 'Restaurant3')
INSERT INTO Chairs VALUES (36, 0, 0, 0, 0, 0, 'Restaurant3')
INSERT INTO Chairs VALUES (37, 0, 0, 0, 0, 0, 'Restaurant3')
INSERT INTO Chairs VALUES (38, 0, 0, 0, 0, 0, 'Restaurant3')

-- Italli Bord #4, 8st stolar
INSERT INTO TablesChairs VALUES ('Italli-8man-1', 4, 11)
INSERT INTO TablesChairs VALUES ('Italli-8man-2', 4, 12)
INSERT INTO TablesChairs VALUES ('Italli-8man-3', 4, 13)
INSERT INTO TablesChairs VALUES ('Italli-8man-4', 4, 14)
INSERT INTO TablesChairs VALUES ('Italli-8man-5', 4, 15)
INSERT INTO TablesChairs VALUES ('Italli-8man-6', 4, 16)
INSERT INTO TablesChairs VALUES ('Italli-8man-7', 4, 17)
INSERT INTO TablesChairs VALUES ('Italli-8man-8', 4, 18)

-- Michaelangelo Bord #8, 8st stolar
INSERT INTO TablesChairs VALUES ('Michaelangelo-8man-1', 8, 21)
INSERT INTO TablesChairs VALUES ('Michaelangelo-8man-2', 8, 22)
INSERT INTO TablesChairs VALUES ('Michaelangelo-8man-3', 8, 23)
INSERT INTO TablesChairs VALUES ('Michaelangelo-8man-4', 8, 24)
INSERT INTO TablesChairs VALUES ('Michaelangelo-8man-5', 8, 25)
INSERT INTO TablesChairs VALUES ('Michaelangelo-8man-6', 8, 26)
INSERT INTO TablesChairs VALUES ('Michaelangelo-8man-7', 8, 27)
INSERT INTO TablesChairs VALUES ('Michaelangelo-8man-8', 8, 28)

-- Jensens Bord #93, 8st stolar
INSERT INTO TablesChairs VALUES ('Jensens-8man-1', 93, 31)
INSERT INTO TablesChairs VALUES ('Jensens-8man-2', 93, 32)
INSERT INTO TablesChairs VALUES ('Jensens-8man-3', 93, 33)
INSERT INTO TablesChairs VALUES ('Jensens-8man-4', 93, 34)
INSERT INTO TablesChairs VALUES ('Jensens-8man-5', 93, 35)
INSERT INTO TablesChairs VALUES ('Jensens-8man-6', 93, 36)
INSERT INTO TablesChairs VALUES ('Jensens-8man-7', 93, 37)
INSERT INTO TablesChairs VALUES ('Jensens-8man-8', 93, 38)


-- Bokar 8man på Italli
INSERT INTO Bookings VALUES (1, '20120618 10:34', 'Robin', 'Robin@domain.com', '0760373111', 'Italli-8man-1','20120718 18:00','20120718 23:00','Mycket mat tack')

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
    TablesChairs tcBooked ON b.TableChairId = tcBooked.Id
JOIN 
    Tables t ON tcBooked.TableId = t.Id
JOIN 
    Restaurants r ON t.RestaurantId = r.Id
JOIN 
    TablesChairs tc ON tc.TableId = t.Id
JOIN 
    Chairs c ON tc.ChairId = c.Id
WHERE 
    b.Id = 1;