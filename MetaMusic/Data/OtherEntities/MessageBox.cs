using MudBlazor;

namespace MetaMusic.Data.OtherEntities
{
    public class MessageBox : IMessageBox
    {
        public void Initialize(string titulo = "", string contenido = "", string yesText = "", string noText = "")
        {
            Titulo = titulo;
            Contenido = contenido;
            YesText = yesText;
            NoText = noText;
            messageB = new MudMessageBox();
            
        }

        public MudMessageBox messageB { get; set; } = new MudMessageBox();
        public string Titulo { get; set; } = "";
        public string Contenido { get; set; } = "";
        public string YesText { get; set; } = "";
        public string NoText { get; set; } = "";


    }
}
