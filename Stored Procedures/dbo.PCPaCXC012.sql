SET QUOTED_IDENTIFIER ON
GO
SET ANSI_NULLS OFF
GO
/*Listado de Productos Factura Nuevo Plan*/
CREATE procedure [dbo].[PCPaCXC012]
@Documento bigint, @Linea int
AS

--DATOS DE LA ORDEN A CANCELAR SEGUN LOS DATOS DE LA FACTURA
SELECT     Facturacion.dbo.CINY057.ORDEN AS Orden, ISNULL((SELECT TOP (1) VENDEDOR FROM Estadisticas.dbo.VENTAS WHERE (ORDEN = Facturacion.dbo.CINY057.ORDEN) ORDER BY CORRELATIVO DESC), Facturacion.dbo.CINY056.VENDEDOR) AS IdVendedor, 
                      CAST(ROUND(Facturacion.dbo.CINY057.VALOR_VENTA / Facturacion.dbo.CINY056.VALOR_VENTA, 4) AS decimal(18, 4)) AS PorFactura, 
                      Facturacion.dbo.CINY057.ARTICULO AS IdProducto, Facturacion.dbo.CINY057.IVA AS Iva, Maestros.dbo.CSMY036.IDSUBCATEGORIA AS IdSubCategoria
FROM         Clientes.dbo.CSMY003 INNER JOIN
                      Facturacion.dbo.CINY056 INNER JOIN
                      Facturacion.dbo.CINY057 ON Facturacion.dbo.CINY056.FACTURA = Facturacion.dbo.CINY057.FACTURA ON 
                      Clientes.dbo.CSMY003.Codigo = Facturacion.dbo.CINY056.CLIENTE LEFT OUTER JOIN
                      WSMY437 INNER JOIN
                      Maestros.dbo.CSMY036 ON WSMY437.IdSubCategoria = Maestros.dbo.CSMY036.IDSUBCATEGORIA ON 
                      Facturacion.dbo.CINY057.ARTICULO = Maestros.dbo.CSMY036.CODIGO_PRODUCT
WHERE     (Facturacion.dbo.CINY057.FACTURA = @Documento) AND (ISNULL(Maestros.dbo.CSMY036.Miscelaneo, '') <> 'X') AND 
                      (NOT (Facturacion.dbo.CINY056.Vendedor LIKE '%OI%'))

UNION ALL

SELECT     WSMY477.Orden, Planta.dbo.CPRY012.vendedor AS IdVendedor, 1.00 AS PorFactura, Planta.dbo.CPRY012.COD_PRODUCTO AS IdProducto, 
                      CAST(dbo.fnBuscatotalIvaOrden(WSMY477.Orden) / Planta.dbo.CPRY012.TOTAL_VENTA * 100 AS decimal(18, 4)) AS Iva, 
                      Maestros.dbo.CSMY036.IDSUBCATEGORIA AS IdSubCategoria
FROM         WSMY477 INNER JOIN
                      Planta.dbo.CPRY012 ON WSMY477.Orden = Planta.dbo.CPRY012.ORDEN INNER JOIN
                      Maestros.dbo.CSMY036 ON Planta.dbo.CPRY012.COD_PRODUCTO = Maestros.dbo.CSMY036.CODIGO_PRODUCT INNER JOIN
                      Clientes.dbo.CSMY003 ON Planta.dbo.CPRY012.CLIENTE = Clientes.dbo.CSMY003.Codigo
WHERE     (ISNULL(Maestros.dbo.CSMY036.Miscelaneo, '') <> 'X') AND (WSMY477.Anticipo = @Documento) AND (WSMY477.Linea = @Linea) AND 
                      (ISNULL(Planta.dbo.CPRY012.ANULADA, '') <> 'X') AND (NOT (Planta.dbo.CPRY012.vendedor LIKE '%OI%'))

UNION ALL

SELECT     WSMY477.Orden, PEDIDOS.dbo.CTMY045.Vendedor AS IdVendedor, 
                      CAST(PEDIDOS.dbo.CTMY046.TOTAL_VENTA / PEDIDOS.dbo.CTMY045.TOTAL_VENTA AS decimal(18, 4)) AS PorFactura, 
                      PEDIDOS.dbo.CTMY046.COD_PRODUCTO AS IdProducto, CAST(dbo.fnBuscatotalIvaOrden(WSMY477.Orden) 
                      / PEDIDOS.dbo.CTMY045.TOTAL_VENTA * 100 AS decimal(18, 2)) AS Iva, Maestros.dbo.CSMY036.IDSUBCATEGORIA AS IdSubCategoria
FROM         WSMY477 INNER JOIN
                      PEDIDOS.dbo.CTMY046 ON WSMY477.Orden = PEDIDOS.dbo.CTMY046.ORDEN INNER JOIN
                      Maestros.dbo.CSMY036 ON PEDIDOS.dbo.CTMY046.COD_PRODUCTO = Maestros.dbo.CSMY036.CODIGO_PRODUCT INNER JOIN
                      PEDIDOS.dbo.CTMY045 ON PEDIDOS.dbo.CTMY046.NUMERO_PEDIDO = PEDIDOS.dbo.CTMY045.NUMERO_PEDIDO INNER JOIN
                      Clientes.dbo.CSMY003 ON PEDIDOS.dbo.CTMY045.CLIENTE = Clientes.dbo.CSMY003.Codigo
WHERE     (ISNULL(Maestros.dbo.CSMY036.Miscelaneo, '') <> 'X') AND (WSMY477.Anticipo = @Documento) AND (WSMY477.Linea = @Linea) AND 
                      (ISNULL(PEDIDOS.dbo.CTMY046.ANULADO, '') <> 'X') AND (NOT ( PEDIDOS.dbo.CTMY045.Vendedor LIKE '%OI%'))

UNION ALL

SELECT        Facturacion.dbo.CINY057.ORDEN AS Orden, Facturacion.dbo.CINY056.Vendedor AS IdVendedor, 
                         CAST(ROUND(Facturacion.dbo.CINY057.VALOR_VENTA / Facturacion.dbo.CINY056.VALOR_VENTA, 4) AS decimal(18, 4)) AS PorFactura, 
                         Facturacion.dbo.CINY057.ARTICULO AS IdProducto, Facturacion.dbo.CINY057.IVA AS Iva, Maestros.dbo.CSMY036.IDSUBCATEGORIA AS IdSubCategoria
FROM            CXC.dbo.WARY183 INNER JOIN
                         Facturacion.dbo.CINY057 ON CXC.dbo.WARY183.FACTURA = Facturacion.dbo.CINY057.FACTURA INNER JOIN
                         Facturacion.dbo.CINY056 ON CXC.dbo.WARY183.FACTURA = Facturacion.dbo.CINY056.FACTURA INNER JOIN
                         Maestros.dbo.CSMY036 ON Facturacion.dbo.CINY057.ARTICULO = Maestros.dbo.CSMY036.CODIGO_PRODUCT INNER JOIN
                         CXC.dbo.WARY182 ON CXC.dbo.WARY183.NOTA = CXC.dbo.WARY182.NOTA INNER JOIN
                         Clientes.dbo.CSMY003 ON Facturacion.dbo.CINY056.CLIENTE = Clientes.dbo.CSMY003.Codigo
WHERE        (ISNULL(Maestros.dbo.CSMY036.Miscelaneo, '') <> 'X') AND (CXC.dbo.WARY183.NOTA = @Documento)


GROUP BY Facturacion.dbo.CINY057.ORDEN, Facturacion.dbo.CINY057.ARTICULO, Facturacion.dbo.CINY057.IVA, Maestros.dbo.CSMY036.IDSUBCATEGORIA, 
                         Facturacion.dbo.CINY057.VALOR_VENTA, Facturacion.dbo.CINY056.VALOR_VENTA, Facturacion.dbo.CINY056.Vendedor
HAVING        (NOT (Facturacion.dbo.CINY056.Vendedor LIKE '%OI%')) OR
                         (NOT (Facturacion.dbo.CINY056.Vendedor LIKE '%OI%'))
GO
