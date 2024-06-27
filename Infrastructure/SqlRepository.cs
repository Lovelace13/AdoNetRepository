using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using Infrastructure.Entidades;
using Infrastructure.Interfaces;
using Infrastructure.Repositories;
using Microsoft.Extensions.Configuration;

namespace Infrastructure
{
    public interface ISqlRepository
    {
        IVehiculoRepository VehiculoRepository { get; }
    }

    public class SqlRepository: ISqlRepository
    {
        public IVehiculoRepository VehiculoRepository { get; }

        public SqlRepository(SqlConnection context, SqlTransaction tran)
        {
            VehiculoRepository = new VehiculoRepository(context, tran); 
        }

    }

    public interface ISqlIndusurAdapter : IDisposable 
    {
        ISqlRepository repositorioVehiculo { get; }
        void SaveChanges();
    }

    public class SqlIndusurAdapter : ISqlIndusurAdapter
    {
        private SqlConnection _Context { get; set; }
        private SqlTransaction _tran { get; set; }
        public ISqlRepository repositorioVehiculo { get; set; }

        public SqlIndusurAdapter(string cadenaConexion)
        {
            _Context = new SqlConnection(cadenaConexion);
            _Context.Open();
            _tran = _Context.BeginTransaction();
            repositorioVehiculo = new SqlRepository(_Context, _tran);
        }

        public void Dispose()
        {
            if (_tran != null)
            {
                _tran.Dispose();
            }

            if (_Context != null)
            {
                _Context.Close();
                _Context.Dispose();
            }

            repositorioVehiculo = null;
        }

        public void SaveChanges()
        {
            _tran.Commit();
        }

    }

    public interface ISQLServerIndusur
    {
        ISqlIndusurAdapter Create();
    }
    public class SQLServerIndusur : ISQLServerIndusur
    {
        private readonly IConfiguration _configuration;
        private string cadenaConexion = "";
        
        
        public SqlRepository repositorio { get; set; }

        public SQLServerIndusur(IConfiguration configuration = null)
        {
            _configuration = configuration;

        }

        public ISqlIndusurAdapter Create()
        {
            cadenaConexion = _configuration.GetConnectionString("DefaultConnection");
            return new SqlIndusurAdapter(cadenaConexion); 
        }

    }

}
