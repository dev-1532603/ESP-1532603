namespace SuperCchicAPI
{
    using System.Text;
    public sealed class DbIniSettings
    {
        public string Server { get; set; } = "";
        public string Port { get; set; } = "";
        public string Database { get; set; } = "";
        public string User { get; set; } = "";
        public string Password { get; set; } = "";
    }
    public static class IniReader
    {
        public static DbIniSettings LoadDatabaseSettings(string path)
        {
            if (!File.Exists(path))
                throw new FileNotFoundException("config.ini introuvable", path);

            var settings = new DbIniSettings();
            var lines = File.ReadAllLines(path, Encoding.UTF8);

            string currentSection = "";

            foreach (var rawLine in lines)
            {
                var line = rawLine.Trim();

                if (string.IsNullOrWhiteSpace(line) || line.StartsWith(";"))
                    continue;

                if (line.StartsWith("[") && line.EndsWith("]"))
                {
                    currentSection = line[1..^1];
                    continue;
                }

                if (currentSection != "Database")
                    continue;

                var idx = line.IndexOf('=');
                if (idx <= 0)
                    continue;

                var key = line[..idx].Trim();
                var value = line[(idx + 1)..].Trim();

                switch (key)
                {
                    case "Server": settings.Server = value; break;
                    case "Port": settings.Port = value; break;
                    case "Database": settings.Database = value; break;
                    case "User": settings.User = value; break;
                    case "Password": settings.Password = value; break;
                }
            }

            return settings;
        }
    }
}
