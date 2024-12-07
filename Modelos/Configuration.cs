using System.Reflection;

namespace Cadastro_de_maquinas.Modelos
{
    public static class Configuration
    {
        public static string GetListDataPath() => $"{Path.GetFullPath(Path.Combine(Path.GetDirectoryName(Assembly.GetEntryAssembly().Location), "..", "..", ".."))}\\ListData.json";
        public static string GetRootDirectory() => Path.GetFullPath(Path.Combine(Path.GetDirectoryName(Assembly.GetEntryAssembly().Location), "..", "..", ".."));
    }
}
