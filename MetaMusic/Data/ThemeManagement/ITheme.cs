namespace MetaMusic.Data.ThemeManagement
{
    public interface ITheme
    {
        bool IsDarkTheme { get; set; }

        bool IsDark();
        void SetDarkTheme(bool isDark);
    }
}