using Infrastructure.Entidades;
using Infrastructure.Interfaces;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Reflection;
using System.Reflection.PortableExecutable;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace Infrastructure.Repositories
{
    public class VehiculoRepository : IVehiculoRepository
    {

        private SqlConnection _context;
        private SqlTransaction _transaction;

        public VehiculoRepository(SqlConnection context, SqlTransaction transaction) 
        {
            this._context = context;
            this._transaction = transaction;
        }

        VehiculoEntity IRepository<VehiculoEntity>.GetById(int id)
        {
            VehiculoEntity vehiculo = new VehiculoEntity();

            using (SqlCommand command = new SqlCommand("GetCarId", _context))
            {
                command.CommandType = CommandType.StoredProcedure;
                command.Parameters.AddWithValue("@vehiculoId", id);

                if (_context.State != ConnectionState.Open)
                {
                    _context.Open();
                }
                else
                {
                    throw new Exception("No se pudo conectar a la base");
                }

                
                using SqlDataReader reader = command.ExecuteReader();
                if (reader.Read())
                {
                    vehiculo = MapToEntity(reader); // Implementa este método para mapear datos de SqlDataReader a TEntity
                }
            }

            return vehiculo;

        }

        IEnumerable<VehiculoEntity> IRepository<VehiculoEntity>.GetAll()
        {
            DataTable listVehiculo = new DataTable();

            using (SqlCommand command = new SqlCommand("GetAllCar", _context))
            {
                command.CommandType = CommandType.StoredProcedure;
                if (_context.State != ConnectionState.Open)
                {
                    _context.Open();
                }
                else
                {
                    throw new Exception("No se pudo conectar a la base");
                }


                using SqlDataAdapter adapter = new SqlDataAdapter(command);
                adapter.Fill(listVehiculo);
                _context.Close();
            }

            return ToEntities(listVehiculo);
        }

        void IRepository<VehiculoEntity>.Add(VehiculoEntity entity)
        {
            try
            {
                using (SqlCommand command = new SqlCommand("AddVehiculo", _context, _transaction))
                {
                    command.CommandType = CommandType.StoredProcedure;
                    command.Parameters.AddWithValue("@cod", entity.codigo);
                    command.Parameters.AddWithValue("@chasis", entity.chasis);
                    command.Parameters.AddWithValue("@marca", entity.marca);
                    command.Parameters.AddWithValue("@modelo", entity.modelo);
                    command.Parameters.AddWithValue("@anio", entity.anio_modelo);
                    command.Parameters.AddWithValue("@color", entity.color);
                    command.Parameters.AddWithValue("@estado", entity.estado);
                    command.Parameters.AddWithValue("@fecha", entity.fecha_registro);

                    int resultado = command.ExecuteNonQuery();
                    if (resultado == 1)
                    {
                        Console.WriteLine("insercion exitora");
                    }
                }
            }
            catch (SqlException ex)
            {
                Console.WriteLine(ex.Message);
            }

        }

        void IRepository<VehiculoEntity>.Update(VehiculoEntity entity)
        {
            try
            {
                using (SqlCommand command = new SqlCommand("UpdateVehiculo", _context, _transaction))
                {
                    command.CommandType = CommandType.StoredProcedure;
                    command.Parameters.AddWithValue("@Id", entity.id);
                    command.Parameters.AddWithValue("@cod", entity.codigo);
                    if (entity.chasis != null)
                    {
                        command.Parameters.AddWithValue("@chasis", entity.chasis);
                    }
                    if (entity.marca != null)
                    {
                        command.Parameters.AddWithValue("@marca", entity.marca);
                    }
                    if (entity.modelo != null)
                    {
                        command.Parameters.AddWithValue("@modelo", entity.modelo);
                    }
                    if (entity.anio_modelo != 0)
                    {
                        command.Parameters.AddWithValue("@anio", entity.anio_modelo);
                    }
                    if (entity.color != null)
                    {
                        command.Parameters.AddWithValue("@color", entity.color);
                    }
                    if (entity.estado != null)
                    {
                        command.Parameters.AddWithValue("@estado", entity.estado);
                    }
                    if (entity.fecha_registro != DateTime.MinValue)
                    {
                        command.Parameters.AddWithValue("@fecha", entity.fecha_registro);
                    }
                    

                    int resultado = command.ExecuteNonQuery();
                    if (resultado == 1)
                    {
                        Console.WriteLine("insercion exitora");
                    }
                }
            }
            catch (SqlException ex)
            {
                Console.WriteLine(ex.Message);
            }
        }
        void IRepository<VehiculoEntity>.Delete(int id)
        {
            using (SqlCommand command = new SqlCommand("DeleteVehiculo", _context, _transaction))
            {
                command.CommandType = CommandType.StoredProcedure;
                command.Parameters.AddWithValue("@Id", id);

                command.ExecuteNonQuery();
            }
        }

        private VehiculoEntity MapToEntity(SqlDataReader reader)
        {
            VehiculoEntity entity = new VehiculoEntity
            {
                id = reader.GetInt32(reader.GetOrdinal("id")),
                codigo = reader.GetString(reader.GetOrdinal("codigo")),
                chasis = reader.GetString(reader.GetOrdinal("chasis")),
                marca = reader.GetString(reader.GetOrdinal("marca")),
                modelo = reader.GetString(reader.GetOrdinal("modelo")),
                anio_modelo = reader.GetInt32(reader.GetOrdinal("anio_modelo")),
                color = reader.GetString(reader.GetOrdinal("color")),
                estado = reader.GetString(reader.GetOrdinal("estado")),
                fecha_registro = reader.GetDateTime(reader.GetOrdinal("fecha_registro"))
            };

            return entity;
        }

        private IEnumerable<VehiculoEntity> ToEntities(DataTable dataTable)
        {
            return dataTable.AsEnumerable().Select(row => new VehiculoEntity
            {
                id = row.Field<int>("id"),
                codigo = row.Field<string>("codigo"),
                chasis = row.Field<string>("chasis"),
                marca = row.Field<string>("marca"),
                modelo = row.Field<string>("modelo"),
                anio_modelo = row.Field<int>("anio_modelo"),
                color = row.Field<string>("color"),
                estado = row.Field<string>("estado"),
                fecha_registro = row.Field<DateTime>("fecha_registro"),
                // Agrega más propiedades según sea necesario
            });
        }

    }
}


