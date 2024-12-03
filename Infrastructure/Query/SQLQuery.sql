INSERT INTO Restaurants VALUES (1, 'Italli', 'Karlshamn')
INSERT INTO Restaurants VALUES (2, 'Michaelangelo', 'Karlskrona')
INSERT INTO Restaurants VALUES (3, 'Jensens', 'Malmö')


-- Italli 4st bord, 2man, 4man, 6man, 8man
INSERT INTO Tables VALUES (1, 2, 0, 0, 0 ,0 ,0 ,0, 1)
INSERT INTO Tables VALUES (2, 4, 0, 0, 0 ,0 ,0 ,0, 1)
INSERT INTO Tables VALUES (3, 6, 0, 0, 0 ,0 ,0 ,0, 1)
INSERT INTO Tables VALUES (4, 8, 0, 0, 0 ,0 ,0 ,0, 1)

-- Michaelangelo 4st bord, 2man, 4man, 6man, 8man
INSERT INTO Tables VALUES (5, 2, 0, 0, 0 ,0 ,0 ,0, 2)
INSERT INTO Tables VALUES (6, 4, 0, 0, 0 ,0 ,0 ,0, 2)
INSERT INTO Tables VALUES (7, 6, 0, 0, 0 ,0 ,0 ,0, 2)
INSERT INTO Tables VALUES (8, 8, 0, 0, 0 ,0 ,0 ,0, 2)

-- Jenses 4st bord, 2man, 4man, 6man, 8man
INSERT INTO Tables VALUES (9, 2, 0, 0, 0 ,0 ,0 ,0, 3)
INSERT INTO Tables VALUES (10, 4, 0, 0, 0 ,0 ,0 ,0, 3)
INSERT INTO Tables VALUES (11, 6, 0, 0, 0 ,0 ,0 ,0, 3)
INSERT INTO Tables VALUES (12, 8, 0, 0, 0 ,0 ,0 ,0, 3)


-- Bokar bord 1 på Italli
INSERT INTO Orders VALUES (1, '20120618 10:34', 'Robin', 'Robin@domain.com', '0760373111', 1)

-- Plocka fram hela bokningen
SELECT 
    o.Id AS OrderId,
    o.CreatedDate,
    o.BookerName,
    o.BookerEmail,
    o.BookerPhone,
    t.Id AS TableId,
    t.Size AS TableSize,
    t.IsBooked,
    r.Id AS RestaurantId,
    r.Name AS RestaurantName,
    r.Location AS RestaurantLocation
FROM Orders o
INNER JOIN Tables t ON o.TableId = t.Id
INNER JOIN Restaurants r ON t.RestaurantId = r.Id
WHERE o.Id = '1';