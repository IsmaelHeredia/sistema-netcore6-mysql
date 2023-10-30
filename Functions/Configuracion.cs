namespace Sistema.Functions
{
    public class Configuracion
    {
        public string conexion_string { get; set; }
        public string jwt_key { get; set; }
        public string jwt_issuer { get; set;}
        public string jwt_audience { get; set; }
        public string nombre_sesion { get; set; }
        public Configuracion()
        {
            IConfigurationRoot datos = new ConfigurationBuilder().AddJsonFile("appsettings.json").Build();
            conexion_string = datos.GetSection("ConnectionStrings")["DefaultConnection"];
            jwt_key = datos.GetSection("Jwt")["Key"];
            jwt_issuer = datos.GetSection("Jwt")["Issuer"];
            jwt_audience = datos.GetSection("Jwt")["Audience"];
            nombre_sesion = datos.GetSection("Variables")["SesionNombre"];
        }
    }
}
