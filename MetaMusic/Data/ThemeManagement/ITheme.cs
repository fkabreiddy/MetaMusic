namespace MetaMusic.Data.ThemeManagement
{
    public interface ITheme
    {
        void SetDarkTheme(bool isDark);
        bool IsDark();
    }
}