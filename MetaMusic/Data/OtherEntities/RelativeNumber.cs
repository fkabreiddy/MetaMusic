namespace Diurnal_Post.Data.Classes
{
    public class RelativeNumber
    {

        public string NumeroRelativo(int? numero)
        {
            // Si el número es menor que mil, devolver el número sin cambios
            if (numero < 1000)
            {
                return numero.ToString();
            }

            // Si el número es menor que un millón, devolver el número dividido por mil y la letra k
            if (numero < 1000000)
            {
                return (numero / 1000) + "k";
            }

            // Si el número es menor que un billón, devolver el número dividido por un millón y la letra M
            if (numero < 1000000000)
            {
                return (numero / 1000000) + "M";
            }

            // Si el número es mayor o igual que un billón, devolver el número dividido por un billón y la letra B
            return (numero / 1000000000) + "B";
        }

    }
}
