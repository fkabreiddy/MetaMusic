namespace MetaMusic.Data.Extensiones
{
    public static class RelativeDatetime
    {

        public static string FechaRelativa(this DateTime fecha)
        {
            // Obtener la fecha actual
            DateTime fechaActual = DateTime.Now;

            // Calcular la diferencia entre ambas fechas
            TimeSpan diferencia = fechaActual - fecha;

            // Si la diferencia es negativa, significa que la fecha es futura
            if (diferencia.TotalSeconds < 0)
            {
                return "Publicado en el futuro";
            }

            // Si la diferencia es menor a un minuto, devolver los segundos
            if (diferencia.TotalSeconds < 60)
            {
                return "Hace " + (int)diferencia.TotalSeconds + " s";
            }

            // Si la diferencia es menor a una hora, devolver los minutos
            if (diferencia.TotalMinutes < 60)
            {
                return "Hace " + (int)diferencia.TotalMinutes + " m";
            }

            // Si la diferencia es menor a un día, devolver las horas
            if (diferencia.TotalHours < 24)
            {
                return "Hace " + (int)diferencia.TotalHours + " h";
            }

            // Si la diferencia es menor a un mes, devolver los días
            if (diferencia.TotalDays < 7)
            {
                return "Hace " + (int)diferencia.TotalDays + " d";
            }

            if (diferencia.TotalDays < 30)
            {
                return "Hace " + (int)(diferencia.TotalDays / 7) + " W";
            }
            
            if (diferencia.TotalDays < 365)
            {
                return "Hace " + (int)(diferencia.TotalDays / 30) + " M";
            }


            return "Hace " + (int)(diferencia.TotalDays / 365) + " A";
        }
        public static string? FechaRelativa(this DateTime? fecha )
        {
            // Obtener la fecha actual
            DateTime? fechaActual = DateTime.Now;

            // Calcular la diferencia entre ambas fechas
            TimeSpan? diferencia = fechaActual - fecha;

            // Si la diferencia es negativa, significa que la fecha es futura
            if (diferencia?.TotalSeconds < 0)
            {
                return "Publicado en el futuro";
            }

            // Si la diferencia es menor a un minuto, devolver los segundos
            if (diferencia?.TotalSeconds < 60)
            {
                return "Hace " + (int?)diferencia?.TotalSeconds + " s" ; 
            }

            // Si la diferencia es menor a una hora, devolver los minutos
            if (diferencia?.TotalMinutes < 60)
            {
                return "Hace " + (int?)diferencia?.TotalMinutes + " m";
            }

            // Si la diferencia es menor a un día, devolver las horas
            if (diferencia?.TotalHours < 24)
            {
                return "Hace " + (int?)diferencia?.TotalHours + " h";
            }

            // Si la diferencia es menor a un mes, devolver los días
            if (diferencia?.TotalDays < 30)
            {
                return "Hace " + (int?)diferencia?.TotalDays + " d";
            }

            // Si la diferencia es menor a un año, devolver los meses
            if (diferencia?.TotalDays < 365)
            {
                return "Hace " + (int?)(diferencia?.TotalDays / 30) + " M";
            }

            // Si la diferencia es mayor o igual a un año, devolver los años
            return "Hace " + (int?)(diferencia?.TotalDays / 365) + " A";
        }
    }
}
