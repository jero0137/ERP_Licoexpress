﻿using Dapper;
using ERP_LicoExpress_API.DbContexts;
using ERP_LicoExpress_API.Interfaces;
using ERP_LicoExpress_API.Models;
using ERP_LicoExpress_API.Helpers;
using Npgsql;
using System.Data;

namespace ERP_LicoExpress_API.Repositories
{
    public class ProductRepository : IProductRepository
    {
        private readonly PgsqlDbContext contextoDB;


        public ProductRepository(PgsqlDbContext unContexto)
        {
            contextoDB = unContexto;
        }


        public async Task<IEnumerable<ProductDetailed>> GetAllAsync()
        {
            var conexion = contextoDB.CreateConnection();

            string sentenciaSQL = "SELECT p.id , nombre, tipo_id, t.descripcion tipo_name, tamaño, imagen, precio_base, precio_venta, proveedor_id, pr.nombre_empresa proveedor_name " +
                        "FROM productos p " +
                        "JOIN tipos t on t.id = p.tipo_id " +
                        "JOIN proveedores pr on pr.id = p.proveedor_id " +
                        "ORDER BY id DESC";

            var resultadoProducts = await conexion.QueryAsync<ProductDetailed>(sentenciaSQL,
                                        new DynamicParameters());

            return resultadoProducts;
        }

        public async Task<IEnumerable<Tipo>> GetTiposAsync()
        {
            var conexion = contextoDB.CreateConnection();

            string sentenciaSQL = "SELECT id, descripcion " +
                        "FROM tipos t " ;

            var resultadoProducts = await conexion.QueryAsync<Tipo>(sentenciaSQL,
                                        new DynamicParameters());

            return resultadoProducts;
        }
        public async Task<Product> GetByIdAsync(int id)
        {
            Product unProduct = new();

            var conexion = contextoDB.CreateConnection();

            DynamicParameters parametrosSentencia = new();
            parametrosSentencia.Add("@product_id", id,
                                    DbType.Int32, ParameterDirection.Input);

            string sentenciaSQL = "SELECT id, nombre, tipo_id, tamaño, imagen, precio_base, precio_venta, proveedor_id  " +
                        "FROM productos " +
                        "WHERE id=@product_id";

            var resultado = await conexion.QueryAsync<Product>(sentenciaSQL,
                                parametrosSentencia);

            if (resultado.Any())
                unProduct = resultado.First();

            return unProduct;
        }


        public async Task<bool> DeleteAsync(Product unProduct)
        {
            bool resultadoAccion = false;

            try
            {
                var conexion = contextoDB.CreateConnection();

                string procedimiento = "p_elimina_product";
                var parametros = new
                {
                    p_id = unProduct.Id
                };

                var cantidad_filas = await conexion.ExecuteAsync(
                    procedimiento,
                    parametros,
                    commandType: CommandType.StoredProcedure);

                if (cantidad_filas != 0)
                    resultadoAccion = true;
            }
            catch (NpgsqlException error)
            {
                throw new DbOperationException(error.Message);
            }

            return resultadoAccion;
        }


        public async Task<bool> CreateAsync(Product unProduct)
        {
            bool resultadoAccion = false;

            try
            {
                var conexion = contextoDB.CreateConnection();

                string procedimiento = "p_inserta_product";
                var parametros = new
                {
                    p_nombre = unProduct.Nombre,
                    p_tipo_id = unProduct.Tipo_id,
                    p_tamaño = unProduct.Tamaño,
                    p_imagen = unProduct.Imagen,
                    p_precio_base = unProduct.Precio_base,
                    p_precio_venta = unProduct.Precio_venta,
                    p_proveedor_id = unProduct.Proveedor_id,
                };

                var cantidad_filas = await conexion.ExecuteAsync(
                    procedimiento,
                    parametros,
                    commandType: CommandType.StoredProcedure);

                if (cantidad_filas != 0)
                    resultadoAccion = true;
            }
            catch (NpgsqlException error)
            {
                throw new DbOperationException(error.Message);
            }

            return resultadoAccion;
        }



        public async Task<bool> UpdateAsync(int producto_id, Product unProducto)
        {
            bool resultadoAccion = false;

            try
            {
                var conexion = contextoDB.CreateConnection();

                string procedimiento = "p_actualiza_producto";
                var parametros = new
                {
                    p_id = producto_id,
                    p_nombre = unProducto.Nombre,
                    p_tipo_id = unProducto.Tipo_id,
                    p_tamaño = unProducto.Tamaño,
                    p_proveedor_id = unProducto.Proveedor_id,
                    p_imagen = unProducto.Imagen,
                    p_precio_venta = unProducto.Precio_venta,
                    p_precio_base = unProducto.Precio_base,

                };

                var cantidad_filas = await conexion.ExecuteAsync(
                    procedimiento,
                    parametros,
                    commandType: CommandType.StoredProcedure);

                if (cantidad_filas != 0)
                    resultadoAccion = true;
            }
            catch (NpgsqlException error)
            {
                throw new DbOperationException(error.Message);
            }

            return resultadoAccion;
        }
    }
}
