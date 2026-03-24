CREATE PROCEDURE [dbo].[AgregarProducto]
    @Id UNIQUEIDENTIFIER = NULL,
    @IdSubCategoria UNIQUEIDENTIFIER,
    @Nombre VARCHAR(MAX),
    @Descripcion VARCHAR(MAX),
    @Precio DECIMAL(18,2),
    @Stock INT,
    @CodigoBarras VARCHAR(MAX)
AS
BEGIN
    SET NOCOUNT ON;

    BEGIN TRY
        SET @Id = ISNULL(@Id, NEWID());

        BEGIN TRANSACTION;

        INSERT INTO dbo.Producto
        (
            Id,
            IdSubCategoria,
            Nombre,
            Descripcion,
            Precio,
            Stock,
            CodigoBarras
        )
        VALUES
        (
            @Id,
            @IdSubCategoria,
            @Nombre,
            @Descripcion,
            @Precio,
            @Stock,
            @CodigoBarras
        );

        COMMIT TRANSACTION;

        SELECT @Id AS Id;
    END TRY
    BEGIN CATCH
        IF @@TRANCOUNT > 0
            ROLLBACK TRANSACTION;

        THROW;
    END CATCH
END