namespace MetaMusic.Data.Extensiones
{
    public static class emailNormalizer
    {
        public static string Normalizar(this string cadena)
        {
            cadena.Replace(".", "_");
            // Encuentra la posición del primer arroba
            int indiceArroba = cadena.IndexOf('@');

            if (indiceArroba != -1) // Verifica si se encontró el arroba
            {
                // Extrae la subcadena hasta el arroba
                return cadena.Substring(0, indiceArroba);
            }
            else
            {
                // Si no se encontró el arroba, devuelve la cadena completa
                return cadena;
            }
        }
    }
}
