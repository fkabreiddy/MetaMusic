namespace MetaMusic.Data.ThemeManagement
{
    public class Theme : ITheme
    {
        public bool IsDarkTheme = true;


        public void SetDarkTheme(bool isDark)
        {
            IsDarkTheme = isDark;
        }
        public bool IsDark()
        {
            return IsDarkTheme;
        }

    }
}
