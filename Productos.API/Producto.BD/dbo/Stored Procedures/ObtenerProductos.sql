CREATE PROCEDURE dbo.ObtenerProductos
AS
BEGIN
    SET NOCOUNT ON;

    SELECT
        Id,
        IdSubCategoria,
        Nombre,
        Descripcion,
        Precio,
        Stock,
        CodigoBarras
    FROM dbo.Producto;
END