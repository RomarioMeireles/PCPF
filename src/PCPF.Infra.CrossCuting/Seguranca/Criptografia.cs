using System.Security.Cryptography;
using System.Text;

namespace PCPF.Infra.CrossCuting.Seguranca
{
    public static class Criptografia
    {
        //Criptografia para a senha/password
        public static string CriptografarSenha(string Senha)
        {
            MD5 md5 = MD5.Create();
            byte[] inputBytes = System.Text.Encoding.ASCII.GetBytes(Senha);
            byte[] hash = md5.ComputeHash(inputBytes);

            StringBuilder sb = new StringBuilder();

            for (int i = 0; i < hash.Length; i++)
            {
                sb.Append(hash[i].ToString("X2"));
            }
            return sb.ToString();
        }
    }
}
