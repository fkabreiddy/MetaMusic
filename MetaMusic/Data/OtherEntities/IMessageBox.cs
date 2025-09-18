using MudBlazor;

namespace MetaMusic.Data.OtherEntities
{
    public interface IMessageBox
    {
        string Contenido { get; set; }
        public MudMessageBox messageB { get; set; }
        string NoText { get; set; }
        string Titulo { get; set; }
        string YesText { get; set; }

        void Initialize(string titulo = "", string contenido = "", string yesText = "", string noText = "");
    }
}