-- USE master
-- alter database ONLINESHOP set single_user with rollback immediate
-- drop database ONLINESHOP

CREATE DATABASE ONLINESHOP
GO

USE ONLINESHOP 
GO

CREATE TABLE [CATEGORY]
(
    CategoryID varchar(20) PRIMARY KEY,
    CategoryName NVARCHAR(250) UNIQUE NOT NULL,
    MetaKeyword NVARCHAR(50),

    CreatedDate DATETIME DEFAULT GETDATE()
)
GO

CREATE TABLE [PRODUCT]
(
    ProductID varchar(20) PRIMARY KEY,
    ProductName NVARCHAR(250) NOT NULL,
    ProductDescription NVARCHAR(4000),
    ProductPrice DECIMAL(18, 0) NOT NULL,
    PromotionPrice DECIMAL(18, 0) DEFAULT 0,
    Rating INT CHECK (RATING >=0 AND RATING <= 5),
    ShowImage_1 NVARCHAR(4000) DEFAULT N'',
    ShowImage_2 NVARCHAR(4000) DEFAULT N'',
    ProductStock INT DEFAULT 1,
    MetaKeyword NVARCHAR(250),
    ProductStatus BIT,
    ViewCount INT DEFAULT 0,

    CreatedDate DATETIME DEFAULT GETDATE(),
    CategoryID varchar(20) CONSTRAINT fk_p_cgid FOREIGN KEY (CategoryID) REFERENCES [CATEGORY](CategoryID) ON DELETE CASCADE NOT NULL
)
GO

CREATE TABLE [PRODUCTIMAGE]
(
    ImageID INT PRIMARY KEY IDENTITY(1, 1),
    DetailImage_1 NVARCHAR(4000) DEFAULT N'',
    DetailImage_2 NVARCHAR(4000) DEFAULT N'',
    DetailImage_3 NVARCHAR(4000) DEFAULT N'',
    ProductID varchar(20) CONSTRAINT fk_pi_pdid FOREIGN KEY (ProductID) REFERENCES [PRODUCT](ProductID) ON DELETE CASCADE
)
GO

CREATE TABLE [SIZE]
(
    SizeID int PRIMARY KEY,
    Size NVARCHAR(2),
    CreatedDate DATETIME DEFAULT GETDATE()
)
GO

CREATE TABLE [PRODUCTDETAIL]
(
    ProductDetailID INT PRIMARY KEY IDENTITY(1, 1),
    ProductID varchar(20) CONSTRAINT fk_pd_pdid FOREIGN KEY (ProductID) REFERENCES [PRODUCT](ProductID) ON DELETE CASCADE,
    SizeID INT REFERENCES [SIZE](SizeID)
)
GO

CREATE TABLE [CUSTOMER]
(
    CustomerID INT PRIMARY KEY IDENTITY(1, 1),
    CustomerUsername NVARCHAR(250) UNIQUE NOT NULL,
    CustomerPassword NVARCHAR(250) NOT NULL,
    CustomerEmail NVARCHAR(250) DEFAULT N'',
    CustomerName NVARCHAR(250) NOT NULL,
    CustomerPhone NVARCHAR(20) DEFAULT N'',
    CustomerAdress NVARCHAR(250) DEFAULT N'',
    CreatedDate DATETIME DEFAULT GETDATE()
)
GO

CREATE TABLE [ORDERSTATUS]
(
    StatusID INT PRIMARY KEY IDENTITY(1, 1),
    StatusName NVARCHAR(255),
    CreatedDate DATETIME DEFAULT GETDATE()
)
GO

CREATE TABLE [ORDER]
(
    OrderID INT PRIMARY KEY IDENTITY(1, 1),
    OrderDate DATETIME DEFAULT GETDATE(),
    DeliveryDate DATETIME,
    Total DECIMAL(18, 2),

    OrderStatusID INT CONSTRAINT fk_od_stid FOREIGN KEY (OrderStatusID) REFERENCES [ORDERSTATUS](StatusID),
    CustomerID INT CONSTRAINT fk_od_csid FOREIGN KEY (CustomerID) REFERENCES [CUSTOMER](CustomerID) ON DELETE CASCADE
)
GO

CREATE TABLE [ORDERDETAIL]
(
    DetailID INT PRIMARY KEY IDENTITY(1, 1),

    Quantity INT,
    OrderID INT CONSTRAINT fk_od_odid FOREIGN KEY (OrderID) REFERENCES [ORDER](OrderID) ON DELETE CASCADE,
    SizeID INT REFERENCES [SIZE](SizeID),
    ProductID varchar(20) CONSTRAINT fk_od_pdid FOREIGN KEY (ProductID) REFERENCES [PRODUCT](ProductID) ON DELETE CASCADE
)
GO

CREATE TABLE [USER]
(
    UserId INT PRIMARY KEY IDENTITY(1, 1),
    UserUsername NVARCHAR(250) UNIQUE NOT NULL,
    UserPassword NVARCHAR(250) NOT NULL,
    UserName NVARCHAR(250),
    CreatedDate DATETIME
)
GO


INSERT INTO [CATEGORY]
VALUES
    ('1', N'Giày da', N'giay-da', GETDATE()),
    ('2', N'Giày thể thao', N'giay-the-thao', GETDATE()),
    ('3', N'Giày lifestyle', N'giay-lifestyle', GETDATE()),
    ('4', N'Giày boots', N'giay-boots', GETDATE())
GO

INSERT INTO [SIZE]
VALUES
    (1, 34, GETDATE()),
    (2, 35, GETDATE()),
    (3, 36, GETDATE()),
    (4, 37, GETDATE()),
    (5, 38, GETDATE()),
    (6, 39, GETDATE()),
    (7, 40, GETDATE()),
    (8, 41, GETDATE()),
    (9, 42, GETDATE()),
    (10, 43, GETDATE())
GO

INSERT INTO [ORDERSTATUS]
VALUES
    (N'Đang xử lý', GETDATE()),
    (N'Đang giao hàng', GETDATE()),
    (N'Đã giao hàng', GETDATE()),
    (N'Hàng có lỗi', GETDATE()),
    (N'Đã hủy', GETDATE())
GO

INSERT INTO [PRODUCT]
VALUES
    ('1', N'Nike', N'Chất liệu cao cấp,thiết kế nén khí làm chân tiếp đất êm hơn,
                              thích hợp cho nhiều độ tuổi,vải waffle cao cấp bền cho lực kéo nhiều mặt',
        200, 150, 3,
        N'https://res.cloudinary.com/dhi8xksch/image/upload/v1583234400/Thuc-Tap-CSDL/giay-chay-nike-2_dlrisy.jpg',
        N'https://res.cloudinary.com/dhi8xksch/image/upload/v1583234400/Thuc-Tap-CSDL/giay-chay-nike-2_dlrisy.jpg',
        50, N'giay-nike', 1, 199, GETDATE(), 1)

INSERT INTO [PRODUCT]
VALUES
    ('2', N'Adidas', N'Phong cách sắc nét,có phần trên bằng da mềm mại cho hình bóng sạch sẽ,tạo cảm giác thể thao',
        150, null, 4,
        N'https://res.cloudinary.com/dhi8xksch/image/upload/v1582983744/Thuc-Tap-CSDL/vans-old-skool-3_bqwgpx.png',
        N'https://res.cloudinary.com/dhi8xksch/image/upload/v1582983744/Thuc-Tap-CSDL/vans-old-skool-3_bqwgpx.png',
        20, N'giay-adidas', 1, 10, GETDATE(), 1)

INSERT INTO [PRODUCT]
VALUES
    ('3', N'Converse', N'giày có thiết kế đơn giản, sang trọng, gam màu pastel nhẹ nhàng- mang dấu ấn đặc biệt của Converse,giúp người dùng có được sự thoải mái tối đa khi sử dụng. Chất liệu da sang trọng, dễ vệ sinh',
        25, 20, 1,
        N'https://res.cloudinary.com/dhi8xksch/image/upload/v1582983744/Thuc-Tap-CSDL/vans-canvas-1_gk0yir.png',
        N'https://res.cloudinary.com/dhi8xksch/image/upload/v1582983744/Thuc-Tap-CSDL/vans-canvas-1_gk0yir.png',
        15, N'giay-converse', 1, 420, GETDATE(), 1)
INSERT INTO [PRODUCT]
VALUES
    ('4', N'Giày da', N'Lựa chọn một đôi giày SDROLUN đơn giản, lịch sự kết hợp với nhiều trang phục là tiêu chí lựa chọn của cánh đàn ông',
        300, 200, 4,
        N'https://res.cloudinary.com/dhi8xksch/image/upload/v1583238041/Thuc-Tap-CSDL/giay-buoc-day-cao-co_k6bx4b.jpg',
        N'https://res.cloudinary.com/dhi8xksch/image/upload/v1583238041/Thuc-Tap-CSDL/giay-buoc-day-cao-co_k6bx4b.jpg',
        25, N'giay-da', 1, 42, GETDATE(), 2)
INSERT INTO [PRODUCT]
VALUES
    ('5', N'Giày cao gót', N'giày được thiết kế sang trọng,kiểu dáng thời trang,phù hợp với các bạn trẻ',
        20, null, 5,
        N'https://res.cloudinary.com/dhi8xksch/image/upload/v1583238043/Thuc-Tap-CSDL/giay-buoc-day-cao-co-2_mnmkjh.jpg',
        N'https://res.cloudinary.com/dhi8xksch/image/upload/v1583238043/Thuc-Tap-CSDL/giay-buoc-day-cao-co-2_mnmkjh.jpg',
        40, N'giay-cao-got', 1, 200, GETDATE(), 3)
INSERT INTO [PRODUCT]
VALUES
    ('6', N'Giày boots', N'giày được thiết kế sang trọng,kiểu dáng thời trang,phù hợp với các bạn trẻ',
        300, null, 1,
        N'https://res.cloudinary.com/dhi8xksch/image/upload/v1583238041/Thuc-Tap-CSDL/giay-buoc-day_sotdbg.jpg',
        N'https://res.cloudinary.com/dhi8xksch/image/upload/v1583238041/Thuc-Tap-CSDL/giay-buoc-day_sotdbg.jpg',
        10, N'giay-boots', 1, 150, GETDATE(), 4)

INSERT INTO [PRODUCT]
VALUES
    (7, N'Giày lười', N'Chất liệu cao cấp,thiết kế nén khí làm chân tiếp đất êm hơn,
thích hợp cho nhiều độ tuổi,vải waffle cao cấp bền cho lực kéo nhiều mặt',
        100, null , 3,
        N'https://res.cloudinary.com/dhi8xksch/image/upload/v1582983743/Thuc-Tap-CSDL/vans-canvas-2_gmizqa.png',
        N'https://res.cloudinary.com/dhi8xksch/image/upload/v1582983743/Thuc-Tap-CSDL/vans-canvas-2_gmizqa.png',
        100, N'giay-luoi', 1, 100, GETDATE(), 2)
INSERT INTO [PRODUCT]
VALUES
    (8, N'Giày Adidas Stan Smith', N'Chất liệu cao cấp,thiết kế nén khí làm chân tiếp đất êm hơn,
                              thích hợp cho nhiều độ tuổi,vải waffle cao cấp bền cho lực kéo nhiều mặt',
        500, 150 , 4,
        N'https://res.cloudinary.com/dhi8xksch/image/upload/v1583234400/Thuc-Tap-CSDL/giay-chay-nike-2_dlrisy.jpg',
        N'https://res.cloudinary.com/dhi8xksch/image/upload/v1583234400/Thuc-Tap-CSDL/giay-chay-nike-2_dlrisy.jpg',
        70, N'giay-adidas-stan-smith', 1, 400, GETDATE(), 2)
INSERT INTO [PRODUCT]
VALUES
    (9, N'Giày Oxford', N'Chất liệu cao cấp,thiết kế nén khí làm chân tiếp đất êm hơn,
                              thích hợp cho nhiều độ tuổi,vải waffle cao cấp bền cho lực kéo nhiều mặt',
        800, 250, 3,
        N'https://res.cloudinary.com/dhi8xksch/image/upload/v1583238041/Thuc-Tap-CSDL/giay-buoc-day-cao-co_k6bx4b.jpg',
        N'https://res.cloudinary.com/dhi8xksch/image/upload/v1583238041/Thuc-Tap-CSDL/giay-buoc-day-cao-co_k6bx4b.jpg',
        80, N'giay-oxford', 1, 500, GETDATE(), 1)
INSERT INTO [PRODUCT]
VALUES
    (10, N'Giày Lucite Platform', N'Chất liệu cao cấp,thiết kế nén khí làm chân tiếp đất êm hơn,
                              thích hợp cho nhiều độ tuổi,vải waffle cao cấp bền cho lực kéo nhiều mặt',
        1400, 450, 3,
        N'https://res.cloudinary.com/dhi8xksch/image/upload/v1583238041/Thuc-Tap-CSDL/giay-buoc-day_sotdbg.jpg',
        N'https://res.cloudinary.com/dhi8xksch/image/upload/v1583238041/Thuc-Tap-CSDL/giay-buoc-day_sotdbg.jpg',
        130, N'giay-lucite-platform', 1, 900, GETDATE(), 3)
INSERT INTO [PRODUCT]
VALUES
    (11, N'Giày Mary Jane', N'Chất liệu cao cấp,thiết kế nén khí làm chân tiếp đất êm hơn,
                              thích hợp cho nhiều độ tuổi,vải waffle cao cấp bền cho lực kéo nhiều mặt',
        400, null , 4,
        N'https://res.cloudinary.com/dhi8xksch/image/upload/v1582983743/Thuc-Tap-CSDL/vans-canvas-2_gmizqa.png',
        N'https://res.cloudinary.com/dhi8xksch/image/upload/v1582983743/Thuc-Tap-CSDL/vans-canvas-2_gmizqa.png',
        430, N'giay-mary-jane', 1, 300, GETDATE(), 3)
INSERT INTO [PRODUCT]
VALUES
    (12, N'Giày Ankle Boots', N'Chất liệu cao cấp,thiết kế nén khí làm chân tiếp đất êm hơn,
                              thích hợp cho nhiều độ tuổi,vải waffle cao cấp bền cho lực kéo nhiều mặt',
        400, 100, 2,
        N'https://res.cloudinary.com/dhi8xksch/image/upload/v1583234398/Thuc-Tap-CSDL/giay-chay-nike_aoknzb.jpg',
        N'https://res.cloudinary.com/dhi8xksch/image/upload/v1583234398/Thuc-Tap-CSDL/giay-chay-nike_aoknzb.jpg',
        30, N'giay-ankle-boots', 1, 500, GETDATE(), 4)
INSERT INTO [PRODUCT]
VALUES
    (13, N'Giày Nike Air Force', N'Chất liệu cao cấp,thiết kế nén khí làm chân tiếp đất êm hơn,
                              thích hợp cho nhiều độ tuổi,vải waffle cao cấp bền cho lực kéo nhiều mặt',
        1200, null, 4,
        N'https://res.cloudinary.com/dhi8xksch/image/upload/v1582983744/Thuc-Tap-CSDL/vans-old-skool-3_bqwgpx.png',
        N'https://res.cloudinary.com/dhi8xksch/image/upload/v1582983744/Thuc-Tap-CSDL/vans-old-skool-3_bqwgpx.png',
        30, N'giay-nike-air-force', 1, 1500, GETDATE(), 2)
INSERT INTO [PRODUCT]
VALUES
    (14, N'Giày Nike SP', N'Chất liệu cao cấp,thiết kế nén khí làm chân tiếp đất êm hơn,
                              thích hợp cho nhiều độ tuổi,vải waffle cao cấp bền cho lực kéo nhiều mặt',
        2000, 800 , 4,
        N'https://res.cloudinary.com/dhi8xksch/image/upload/v1582983744/Thuc-Tap-CSDL/vans-canvas-1_gk0yir.png',
        N'https://res.cloudinary.com/dhi8xksch/image/upload/v1582983744/Thuc-Tap-CSDL/vans-canvas-1_gk0yir.png',
        85, N'giay-nike-sp', 1 , 1800, GETDATE(), 2)
INSERT INTO [PRODUCT]
VALUES
    (15, N'Giày Nike Metcon', N'Chất liệu cao cấp,thiết kế nén khí làm chân tiếp đất êm hơn,
                              thích hợp cho nhiều độ tuổi,vải waffle cao cấp bền cho lực kéo nhiều mặt',
        1800, 700 , 2,
        N'https://res.cloudinary.com/dhi8xksch/image/upload/v1583234400/Thuc-Tap-CSDL/giay-chay-nike-2_dlrisy.jpg',
        N'https://res.cloudinary.com/dhi8xksch/image/upload/v1583234400/Thuc-Tap-CSDL/giay-chay-nike-2_dlrisy.jpg',
        150, N'giay-nike-sp', 1 , 900, GETDATE(), 2)

INSERT INTO [PRODUCTDETAIL]
VALUES
    (1, 1),
    (1, 2),
    (1, 3),
    (1, 4),
    (1, 5),
    (2, 1),
    (2, 2),
    (2, 3),
    (2, 4),
    (2, 5),
    (3, 1),
    (3, 2),
    (3, 3),
    (3, 4),
    (3, 5),
    (4, 1),
    (4, 2),
    (4, 3),
    (4, 4),
    (4, 5),
    (5, 1),
    (5, 2),
    (5, 3),
    (5, 4),
    (5, 5),
    (6, 1),
    (6, 2),
    (6, 3),
    (6, 4),
    (6, 5),
    (7, 6),
    (7, 7),
    (7, 8),
    (7, 9),
    (7, 10),
    (8, 4),
    (8, 5),
    (8, 6),
    (8, 7),
    (9, 5),
    (9, 4),
    (9, 3),
    (9, 6),
    (9, 8),
    (10, 4),
    (10, 5),
    (10, 7 ),
    (10, 10),
    (10, 8)


INSERT INTO [USER]
VALUES(N'hoang', N'4297f44b13955235245b2497399d7a93', N'Hoàng', GETDATE())

INSERT INTO [USER]
VALUES(N'hung', N'4297f44b13955235245b2497399d7a93', N'Mạnh Hùng', GETDATE())

INSERT INTO [USER]
VALUES(N'huong', N'4297f44b13955235245b2497399d7a93', N'Bùi Hương', GETDATE())

INSERT INTO [USER]
VALUES(N'hoanganh', N'4297f44b13955235245b2497399d7a93', N'Phạm Hoàng Anh', GETDATE())


--------------------------------------------------
GO
CREATE PROC SelectOrderID
    --lọc đơn hàng theo trạng thái của khách hàng
    @CustomerID INT,
    @StatusID INT
AS
BEGIN
    IF @StatusID = 0
    BEGIN
        SELECT [ORDER].OrderID
        FROM [ORDER]
        WHERE [ORDER].CustomerID = @CustomerID
    END
        ELSE
        BEGIN
        SELECT [ORDER].OrderID
        FROM [ORDER]
        WHERE [ORDER].CustomerID = @CustomerID
            AND [OrderStatusID] = @StatusID
    END
END
GO

CREATE PROC SelectOrder
    -- lấy các đơn hàng của 1 khách hàng
    @CustomerID INT
AS
BEGIN
    SELECT [ORDER].OrderID,
        [ORDER].Total,
        [ORDER].OrderStatusID,
        [ORDERSTATUS].StatusName,
        [ORDER].OrderDate,
        [ORDER].DeliveryDate
    FROM [ORDER] JOIN [ORDERSTATUS] ON [ORDER].OrderStatusID = [ORDERSTATUS].StatusID
    WHERE [ORDER].CustomerID = @CustomerID
END
GO

CREATE PROC SelectOrderProduct
    -- lấy các sản phẩm của đơn hàng
    @OrderID INT
AS
BEGIN
    SELECT
        [PRODUCT].ProductID,
        [PRODUCT].ProductName,
        [CATEGORY].CategoryName,
        [SIZE].Size,
        [ORDERDETAIL].Quantity
    FROM [ORDERDETAIL]
        JOIN [ORDER] ON [ORDERDETAIL].OrderID = [ORDER].OrderID
        JOIN [ORDERSTATUS] ON [ORDER].OrderStatusID = [ORDERSTATUS].StatusID
        JOIN [SIZE] ON [ORDERDETAIL].SizeID = [SIZE].SizeID
        JOIN [PRODUCT] ON [ORDERDETAIL].[ProductID] = [PRODUCT].ProductID
        JOIN [CATEGORY] ON [PRODUCT].CategoryID = [CATEGORY].CategoryID
    WHERE [ORDER].OrderID = @OrderID
END
GO

CREATE PROC SelectAllProduct
AS
BEGIN
    SELECT *
    FROM PRODUCT

END

 
--------------------------------------------

 --public void FixEfProviderServicesProblem()
 --        {
 --            var instance = System.Data.Entity.SqlServer.SqlProviderServices.Instance;
 --        }
GO


--create read update delete
--read -> tham số truyền vào -> 1 danh sách		-> 0 có		-> trả về 1 list
--							-> lấy chi tiết 1 sp -> id		-> trả về 1 đối tượng
--delete ->					-> truyền id					-> trả về null
--create ->				-> truyền 1 đối tượng			-> trả về id
--update					-> truyền id và 1 đối tượng		-> trả về đối tượng

--crud

-- Thêm sp
CREATE PROC Create_Product
    @id varchar(20),
    @name nvarchar(250),
    @description nvarchar(250),
    @price decimal(18,0),
    @promotionprice decimal(18,2),
    @img1 nvarchar(250),
    @img2 nvarchar(250),
    @stock int,
    @meta nvarchar(250),
    @status bit,
    @cate int
AS
BEGIN
    INSERT dbo.PRODUCT
    VALUES
        (
            @id, @name, @description,
            @price, @promotionprice , 5,
            @img1, @img2,
            @stock, @meta, @status, 0, getdate(), @cate  
)
END 
GO
-- Update sp
CREATE PROC Update_Product
    @id varchar(20),
    @name nvarchar(250),
    @description nvarchar(250),
    @price decimal(18,0),
    @promotionprice decimal(18,2),
    @img1 nvarchar(250),
    @img2 nvarchar(250),
    @stock int,
    @meta nvarchar(250),
    @status bit,
    @cate int
AS
BEGIN
    UPDATE dbo.PRODUCT
SET
    dbo.PRODUCT.ProductName = @name, 
    dbo.PRODUCT.ProductDescription = @description, 
    dbo.PRODUCT.ProductPrice = @price,
    dbo.PRODUCT.PromotionPrice = @promotionprice,
    dbo.PRODUCT.ShowImage_1 = @img1,
    dbo.PRODUCT.ShowImage_2 = @img2, 
    dbo.PRODUCT.ProductStock = @stock, 
    dbo.PRODUCT.MetaKeyword = @meta, 
    dbo.PRODUCT.ProductStatus = @status, 
    dbo.PRODUCT.CreatedDate = getdate(), -- DATETIME
    dbo.PRODUCT.CategoryID = @cate
	WHERE dbo.PRODUCT.ProductID = @id
END
GO
-- Delete sp
CREATE PROC Delete_Product
    @id varchar(20)
AS
BEGIN
    DELETE dbo.PRODUCT WHERE dbo.PRODUCT.ProductID=@id
END
GO
CREATE PROC LoadProd_ByID
    @id varchar(20)
AS
BEGIN
    SELECT *
    FROM dbo.PRODUCT p
    WHERE p.ProductID=@id
END
GO
CREATE PROC ProductList
AS
BEGIN
    SELECT *
    FROM dbo.PRODUCT p
END 
GO

CREATE PROC Add_ProductDetail
    @prodID varchar(20),
    @sizeID int
AS
BEGIN
    INSERT dbo.PRODUCTDETAIL
        (
        --ProductDetailID - column value is auto-generated
        ProductID,
        SizeID
        )
    VALUES
        (
            -- ProductDetailID - INT
            @prodID, -- ProductID - INT
            @sizeID -- SizeID - INT
)
END
GO

CREATE PROC Update_ProductDetail
    @prodID varchar(20),
    @sizeID int
AS
BEGIN
    UPDATE dbo.PRODUCTDETAIL
SET
    dbo.PRODUCTDETAIL.SizeID = @sizeID-- INT
	WHERE dbo.PRODUCTDETAIL.ProductID=@prodID
END
GO

CREATE PROC Delete_ProductDetail
    @prodID varchar(20)
AS
BEGIN
    DELETE FROM dbo.PRODUCTDETAIL
WHERE dbo.PRODUCTDETAIL.ProductID=@prodID
END
GO

CREATE PROC LoadSize_ByProdID
    @prodID varchar(20)
AS
BEGIN
    SELECT *
    FROM dbo.PRODUCTDETAIL p
    WHERE p.ProductID=@prodID
END
GO

CREATE PROC CategoryList
AS
BEGIN
    SELECT *
    FROM dbo.CATEGORY c
END
GO
CREATE PROC SizeList
AS
BEGIN
    SELECT *
    FROM dbo.SIZE s
END 
GO

CREATE PROC Add_Order
    @cusID int,
    @total decimal(18,2),
    @ReturnID INT OUTPUT
AS
BEGIN
    SET NOCOUNT ON;
    INSERT dbo.[ORDER]
    VALUES
        (
            GETDATE(),
            GETDATE(),
            @total,
            1,
            @cusID
        )
    SET @ReturnID = SCOPE_IDENTITY()
    SELECT @ReturnID
END 
GO

CREATE PROC Add_OrderDetail
    @orderID int,
    @prodID varchar(20),
    @sizeID int,
    @quantity int
AS
BEGIN
    INSERT dbo.ORDERDETAIL
        (
        --DetailID - column value is auto-generated
        Quantity,
        OrderID,
        SizeID,
        ProductID
        )
    VALUES
        (
            -- DetailID - int
            @quantity, -- Quantity - int
            @orderID, -- OrderID - int
            @sizeID, -- SizeID - int
            @prodID -- ProductID - int
)
END
GO
CREATE PROC LoadOrderDetail
    @orderID int
AS
BEGIN
    SELECT *
    FROM dbo.ORDERDETAIL o
    WHERE o.OrderID=@orderID
END
GO
CREATE PROC Create_Size
    @id int,
    @sizeName nvarchar(250)
AS
BEGIN
    INSERT dbo.SIZE
        (
        SizeID,
        Size,
        CreatedDate
        )
    VALUES
        (
            @id,
            @sizeName, -- Size - NVARCHAR
            GETDATE() -- CreatedDate - DATETIME
)
END 
GO
CREATE PROC Delete_Size
    @sizeID int
AS
BEGIN
    DELETE dbo.SIZE WHERE dbo.SIZE.SizeID=@sizeID
END
GO
CREATE PROC Update_Size
    @sizeID int,
    @sizeName varchar(2)
AS
BEGIN
    UPDATE dbo.SIZE
SET
    --SizeID - column value is auto-generated
    dbo.SIZE.Size =@sizeName, -- NVARCHAR
    dbo.SIZE.CreatedDate = GETDATE() -- DATETIME
	WHERE dbo.SIZE.SizeID=@sizeID
END
GO

CREATE PROC LoadSize_ByID
    @id int
AS
BEGIN
    SELECT *
    FROM dbo.SIZE s
    WHERE s.SizeID=@id
END
GO


CREATE PROC Create_Customer
    @username nvarchar(250),
    @pass nchar(250),
    @name nvarchar(250),
    @phone nvarchar(20),
    @mail nvarchar(250)
AS
BEGIN
    INSERT dbo.CUSTOMER
        (
        --CustomerID - column value is auto-generated
        CustomerUsername,
        CustomerPassword,
        CustomerEmail,
        CustomerName,
        CustomerPhone,
        CreatedDate
        )
    VALUES
        (
            -- CustomerID - INT
            @username, -- CustomerUsername - NVARCHAR
            @pass, -- CustomerPassword - NVARCHAR
            @mail, -- CustomerEmail - NVARCHAR
            @name, -- CustomerName - NVARCHAR
            @phone, -- CustomerPhone - NVARCHAR
            GETDATE() -- CreatedDate - DATETIME
)
END 
GO

CREATE PROC Delete_Customer
    @id int
AS
BEGIN
    DELETE FROM dbo.CUSTOMER 
WHERE dbo.CUSTOMER.CustomerID=@id
END
go

CREATE PROC LoadByUserName
    @username nvarchar(250)
AS
BEGIN
    SELECT *
    FROM dbo.CUSTOMER c
    WHERE c.CustomerUsername=@username
END 
GO

CREATE PROC	Load_Customer
AS
BEGIN
    SELECT *
    FROM dbo.CUSTOMER c
END
GO

CREATE PROC LoadCustomer_ByID
    @id int
AS
BEGIN
    SELECT *
    FROM dbo.CUSTOMER c
    WHERE c.CustomerID =@id
END
GO

CREATE PROC Create_User
    @username nvarchar(200),
    @pass nvarchar(200),
    @name nvarchar(200)
AS
BEGIN
    INSERT INTO dbo.[USER]
    VALUES
        (
            -- UserId - INT
            @username, -- UserUsername - NVARCHAR
            @pass, -- UserPassword - NVARCHAR
            @name, -- UserName - NVARCHAR
            getdate() -- CreatedDate - DATETIME
	)
END 
GO
CREATE PROC Login_Admin
    @username nvarchar(250),
    @pass nvarchar(250)
AS
BEGIN
    DECLARE @count int
    DECLARE @res bit
    SELECT @count = count(*)
    FROM dbo.[USER] u
    WHERE u.UserUsername=@username AND u.UserPassword=@pass
    IF @count > 0 
		SET @res=1
	ELSE
		SET @res=0
    SELECT @res
END
GO

CREATE PROC Create_Category
    @id varchar(20),
    @name nvarchar(250),
    @cate nvarchar(250)
AS
BEGIN
    INSERT dbo.CATEGORY
        (
        CategoryID,
        CategoryName,
        MetaKeyword,
        CreatedDate
        )
    VALUES
        (
            @id,
            @name, -- CategoryName - NVARCHAR
            @cate, -- MetaKeyword - NVARCHAR
            GETDATE() -- CreatedDate - DATETIME
)
END
GO

CREATE PROC Delete_Category
    @id varchar(20)
AS
BEGIN
    DELETE dbo.CATEGORY WHERE dbo.CATEGORY.CategoryID=@id
END
GO

CREATE PROC Edit_Category
    @id varchar(20),
    @name nvarchar(250),
    @meta nvarchar(250)
AS
BEGIN
    UPDATE dbo.CATEGORY
SET
    --CategoryID - column value is auto-generated
    dbo.CATEGORY.CategoryName = @name, -- NVARCHAR
    dbo.CATEGORY.MetaKeyword = @meta, -- NVARCHAR
    dbo.CATEGORY.CreatedDate = GETDATE() -- DATETIME
	WHERE dbo.CATEGORY.CategoryID=@id
END
GO

CREATE PROC LoadMeta_ByID
    @id varchar(20)
AS
BEGIN
    SELECT *
    FROM dbo.CATEGORY c
    WHERE c.CategoryID=@id
END 
GO

CREATE PROC Load_Order
AS
BEGIN
    SELECT *
    FROM dbo.[ORDER] o
END 
GO

CREATE PROC LoadOrderStatus
AS
BEGIN
    SELECT *
    FROM dbo.ORDERSTATUS o
END
GO

CREATE PROC Cancel_Order
    @orderID int
AS
BEGIN
    UPDATE dbo.[ORDER]
SET
    dbo.[ORDER].OrderStatusID = 5
WHERE dbo.[ORDER].OrderID=@orderID
END 
GO

CREATE PROC Change_Order
    @orderID int,
    @statusID int
AS
BEGIN
    UPDATE dbo.[ORDER]
SET
    dbo.[ORDER].OrderStatusID = @statusID
	WHERE dbo.[ORDER].OrderID=@orderID
END
GO

CREATE PROC Load_CustomerOrder
    @cusID int
AS
BEGIN
    SELECT *
    FROM dbo.[ORDER] o
    WHERE o.CustomerID=@cusID
END
GO

CREATE PROC LoadByMeta_Cate
    @meta nvarchar(250)
AS
BEGIN
    SELECT *
    FROM dbo.CATEGORY c
    WHERE c.MetaKeyword=@meta
END
GO

CREATE PROC LoadByMeta_Prod
    @meta nvarchar(250)
AS
BEGIN
    SELECT *
    FROM dbo.PRODUCT p
    WHERE p.MetaKeyword=@meta
END
GO

CREATE PROC SelectPaging
    @PageSize INT,
    @PageIndex INT,
    @Sort nvarchar(10),
    @Search nvarchar(255) = NULL,
    @Cate INT = NULL
AS
BEGIN
    IF(@Cate IS NULL)
    BEGIN
        SELECT *
        FROM [PRODUCT]
        WHERE(@Search IS NULL OR [PRODUCT].[ProductName] LIKE '%'+@Search+'%')
        ORDER BY
		CASE
			WHEN @Sort = '' THEN ProductID END ASC,
        CASE 
            WHEN @Sort = 'name_desc' THEN ProductName END DESC,
        CASE 
            WHEN @Sort = 'name_asc' THEN ProductName END ASC,
        CASE 
            WHEN @Sort = 'price_desc' THEN ProductPrice END DESC,
        CASE 
            WHEN @Sort = 'price_asc' THEN ProductPrice END ASC,
        CASE 
            WHEN @Sort = 'top_view' THEN ViewCount END DESC
        OFFSET @PageSize * @PageIndex ROWS
        FETCH NEXT @PageSize ROWS ONLY
    END
    ELSE
    BEGIN
        SELECT *
        FROM [PRODUCT]
        WHERE [PRODUCT].CategoryID = @Cate
        ORDER BY
		CASE
			WHEN @Sort = '' THEN ProductID END ASC,
        CASE 
            WHEN @Sort = 'name_desc' THEN ProductName END DESC,
        CASE 
            WHEN @Sort = 'name_asc' THEN ProductName END ASC,
        CASE 
            WHEN @Sort = 'price_desc' THEN ProductPrice END DESC,
        CASE 
            WHEN @Sort = 'price_asc' THEN ProductPrice END ASC,
        CASE 
            WHEN @Sort = 'top_view' THEN ViewCount END DESC
        OFFSET @PageSize * @PageIndex ROWS
        FETCH NEXT @PageSize ROWS ONLY
    END
END
GO

CREATE PROC Update_ViewCount
    @id varchar(20)
AS
BEGIN
    UPDATE dbo.PRODUCT
SET dbo.PRODUCT.ViewCount = dbo.PRODUCT.ViewCount + 1
WHERE dbo.PRODUCT.ProductID=@id

    SELECT p.ViewCount
    FROM dbo.PRODUCT p
    WHERE p.ProductID=@id
END
GO

CREATE PROC Top_View
    @topcount int
AS
BEGIN
    SELECT TOP (@topcount)
        *
    FROM dbo.PRODUCT p
    ORDER BY p.ViewCount DESC
END
GO

CREATE PROC Top_View_Desc
AS
BEGIN
    SELECT *
    FROM dbo.PRODUCT p
    ORDER BY p.ViewCount DESC
END
GO


CREATE PROC Top_Purchase
AS
BEGIN
    SELECT count(o.ProductID) AS ProductCount, o.ProductID
    from dbo.ORDERDETAIL o
    GROUP BY o.ProductID
    ORDER BY count(o.ProductID) DESC
END


