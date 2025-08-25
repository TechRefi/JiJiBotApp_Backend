using JiJiBotApp_Backend.Data;
using JiJiBotApp_Backend.DTOs.Model.Login;
using JiJiBotApp_Backend.DTOs.SearchRequests.Login;
using JiJiBotApp_Backend.Extensions;
using Microsoft.Data.SqlClient;
using System.Data;
using System.Security.Cryptography;

namespace JiJiBotApp_Backend.Repositories.Login
{
    public class LoginRepository(IStoredProcedureExecutor spExecutor, ILogger<LoginRepository> logger) : ILoginRepository
    {
        private readonly IStoredProcedureExecutor _spExecutor = spExecutor;
        private readonly ILogger<LoginRepository> _logger = logger;

        public async Task<UserModel?> ValidateUserAsync(LoginAuthRequestModel request)
        {
            SqlParameter[] parameters =
            [
                new SqlParameter("@UserName", SqlDbType.NVarChar) { Value = request.UserName },
                new SqlParameter("@UserPassword", SqlDbType.NVarChar) { Value = request.Password },
                new SqlParameter("@Action", SqlDbType.NVarChar) { Value = request.Action }
            ];

            try
            {
                var result = await _spExecutor.ExecuteStoredProcedureAsync("uspSelectAuthenticateUser", parameters);

                // Convert result to UserDto
                var loginUser = result.ToList<UserModel>().FirstOrDefault();

                return loginUser; // null if not found
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error executing uspSelectAuthenticateUser stored procedure");
                throw;
            }
        }

        #region 🔐 Password Encryption
        private static string Encrypt(string keyToEncrypt)
        {
            string pwd = "WELCOME_TO_JiJiBotApp"; // Ideally move this to configuration
            byte[] salt = new byte[] { 0x45, 0xF1, 0x61, 0x6e, 0x20, 0x00, 0x65, 0x64, 0x76, 0x65, 0x64, 0x03, 0x76 };

            byte[] clearBytes = System.Text.Encoding.Unicode.GetBytes(keyToEncrypt);
            Rfc2898DeriveBytes pdb = new(pwd, salt);
            byte[] encryptedData = Encrypt(clearBytes, pdb.GetBytes(32), pdb.GetBytes(16));
            return Convert.ToBase64String(encryptedData);
        }

        private static byte[] Encrypt(byte[] data, byte[] key, byte[] iv)
        {
            using MemoryStream ms = new();
            using (CryptoStream cs = new(ms, Rijndael.Create().CreateEncryptor(key, iv), CryptoStreamMode.Write))
            {
                cs.Write(data, 0, data.Length);
                cs.FlushFinalBlock();
            }
            return ms.ToArray();
        }
        #endregion
    }
}
