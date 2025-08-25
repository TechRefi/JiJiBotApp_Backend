using Microsoft.Data.SqlClient;
using System.Data;

namespace JiJiBotApp_Backend.Data
{
    public interface IStoredProcedureExecutor
    {
        Task<DataTable> ExecuteStoredProcedureAsync(string storedProcedureName, params SqlParameter[] parameters);
        Task<int> ExecuteNonQueryStoredProcedureAsync(string storedProcedureName, params SqlParameter[] parameters);
        Task<object> ExecuteScalarStoredProcedureAsync(string storedProcedureName, params SqlParameter[] parameters);

        // Transaction-specific method versions
        Task<DataTable> ExecuteStoredProcedureWithTransactionAsync(SqlConnection connection, SqlTransaction transaction, string storedProcedureName, params SqlParameter[] parameters);
        Task<int> ExecuteNonQueryStoredProcedureWithTransactionAsync(SqlConnection connection, SqlTransaction transaction, string storedProcedureName, params SqlParameter[] parameters);
        Task<object> ExecuteScalarStoredProcedureWithTransactionAsync(SqlConnection connection, SqlTransaction transaction, string storedProcedureName, params SqlParameter[] parameters);

        // Transaction wrapper method
        Task<T> ExecuteInTransactionAsync<T>(Func<SqlConnection, SqlTransaction, Task<T>> operation);
    }

    public class StoredProcedureExecutor(IConfiguration configuration, ILogger<StoredProcedureExecutor> logger) : IStoredProcedureExecutor
    {
        private readonly string _connectionString = configuration.GetConnectionString("DefaultConnection")
                ?? throw new ArgumentNullException("DefaultConnection string is missing in configuration");

        public async Task<DataTable> ExecuteStoredProcedureAsync(string storedProcedureName, params SqlParameter[] parameters)
        {
            using var connection = new SqlConnection(_connectionString);
            using var command = CreateSqlCommand(connection, storedProcedureName, parameters);

            var dataTable = new DataTable();

            try
            {

                await connection.OpenAsync();
                using var reader = await command.ExecuteReaderAsync();
                dataTable.Load(reader);
                return dataTable;
            }
            catch (Exception ex)
            {
                logger.LogError(ex, "Error executing stored procedure {ProcedureName}", storedProcedureName);
                throw;
            }
        }



        public async Task<int> ExecuteNonQueryStoredProcedureAsync(string storedProcedureName, params SqlParameter[] parameters)
        {
            using var connection = new SqlConnection(_connectionString);
            using var command = CreateSqlCommand(connection, storedProcedureName, parameters);

            try
            {
                await connection.OpenAsync();
                return await command.ExecuteNonQueryAsync();
            }
            catch (Exception ex)
            {
                logger.LogError(ex, "Error executing non-query stored procedure {ProcedureName}", storedProcedureName);
                throw;
            }
        }

        public async Task<object> ExecuteScalarStoredProcedureAsync(string storedProcedureName, params SqlParameter[] parameters)
        {
            using var connection = new SqlConnection(_connectionString);
            using var command = CreateSqlCommand(connection, storedProcedureName, parameters);

            try
            {
                await connection.OpenAsync();
                return await command.ExecuteScalarAsync();
            }
            catch (Exception ex)
            {
                logger.LogError(ex, "Error executing scalar stored procedure {ProcedureName}", storedProcedureName);
                throw;
            }
        }

        // Transaction-aware version of ExecuteStoredProcedureAsync
        public async Task<DataTable> ExecuteStoredProcedureWithTransactionAsync(
            SqlConnection connection,
            SqlTransaction transaction,
            string storedProcedureName,
            params SqlParameter[] parameters)
        {
            using var command = CreateSqlCommand(connection, storedProcedureName, parameters, transaction);
            var dataTable = new DataTable();

            try
            {
                using var reader = await command.ExecuteReaderAsync();
                dataTable.Load(reader);
                return dataTable;
            }
            catch (Exception ex)
            {
                logger.LogError(ex, "Error executing stored procedure {ProcedureName} within transaction", storedProcedureName);
                throw;
            }
        }

        // Transaction-aware version of ExecuteNonQueryStoredProcedureAsync
        public async Task<int> ExecuteNonQueryStoredProcedureWithTransactionAsync(
            SqlConnection connection,
            SqlTransaction transaction,
            string storedProcedureName,
            params SqlParameter[] parameters)
        {
            using var command = CreateSqlCommand(connection, storedProcedureName, parameters, transaction);

            try
            {
                return await command.ExecuteNonQueryAsync();
            }
            catch (Exception ex)
            {
                logger.LogError(ex, "Error executing non-query stored procedure {ProcedureName} within transaction", storedProcedureName);
                throw;
            }
        }

        // Transaction-aware version of ExecuteScalarStoredProcedureAsync
        public async Task<object> ExecuteScalarStoredProcedureWithTransactionAsync(
            SqlConnection connection,
            SqlTransaction transaction,
            string storedProcedureName,
            params SqlParameter[] parameters)
        {
            using var command = CreateSqlCommand(connection, storedProcedureName, parameters, transaction);

            try
            {
                return await command.ExecuteScalarAsync();
            }
            catch (Exception ex)
            {
                logger.LogError(ex, "Error executing scalar stored procedure {ProcedureName} within transaction", storedProcedureName);
                throw;
            }
        }

        // Method to execute operations within a transaction
        public async Task<T> ExecuteInTransactionAsync<T>(Func<SqlConnection, SqlTransaction, Task<T>> operation)
        {
            using var connection = new SqlConnection(_connectionString);
            await connection.OpenAsync();

            using var transaction = connection.BeginTransaction();

            try
            {
                var result = await operation(connection, transaction);
                transaction.Commit();
                return result;
            }
            catch (Exception ex)
            {
                logger.LogError(ex, "Error executing transaction operations");
                transaction.Rollback();
                throw;
            }
        }

        // Helper method to create SqlCommand with transaction support
        private SqlCommand CreateSqlCommand(
            SqlConnection connection,
            string storedProcedureName,
            SqlParameter[] parameters,
            SqlTransaction? transaction = null)
        {
            var command = new SqlCommand(storedProcedureName, connection)
            {
                CommandType = CommandType.StoredProcedure
            };

            if (transaction != null)
            {
                command.Transaction = transaction;
            }

            // Add parameters safely to prevent SQL injection
            if (parameters != null && parameters.Length > 0)
            {
                command.Parameters.AddRange(parameters);
            }

            return command;
        }
    }
}