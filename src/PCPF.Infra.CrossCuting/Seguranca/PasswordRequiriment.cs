using System.Linq;

namespace PCPF.Infra.CrossCuting.Seguranca
{
    public static class PasswordRequiriment
    {
        private static int _minSize { get; } = 6;
        private static bool _upperCase { get; } = true;
        private static bool _lowerCase { get; } = false;

        public static (bool, string) ValidatePassword(string password)
        {
            if(!ValidateSize(password))
            {
                return (false, "A password deve ter no mínimo 6 caracteres");
            }
            if (!UpperCase(password))
            {
                return (false, "A password deve ter pelo menos uma letra maiscula");
            }
            if (!LowerCase(password))
            {
                return (false, "A password deve ter pelo menos uma letra minuscula");
            }
            return (true, "Password válida");
        }
        internal static bool ValidateSize(string password)
        {
            if (password.Length < 6)
            {
                return false;
            }
            return true;
        }
        internal static bool UpperCase(string password)
        {
            if (password.Any(char.IsUpper))
            {
                return true;
            }
            return false;
        }
        internal static bool LowerCase(string password)
        {
            if (password.Any(char.IsLower))
            {
                return true;
            }
            return false;
        }
    }
}
