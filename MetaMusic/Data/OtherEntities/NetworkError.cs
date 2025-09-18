using Microsoft.AspNetCore.Components;

namespace MetaMusic.Data.OtherEntities
{
    public class NetworkError 
    {

        private readonly NavigationManager navManager;

        public NetworkError(NavigationManager navManager)
        {
            this.navManager = navManager;
        }

        public static bool IsNetworkError(int errorCode)
        {

            return errorCode == -1 || errorCode == 53;
        }

        public void GoToNetworkError()
        {
            navManager.NavigateTo("/netwrok-error");

        }
    }
}
