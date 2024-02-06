using FrontEndApp.View;

namespace FrontEndApp.Utilites
{
    public static class Reset
    {
        public static void ClearValuesOfUserRegisterWindow()
        {
            RegisterWindow.c.RegisterFirstName.Clear();
            RegisterWindow.c.RegisterLastName.Clear();
            RegisterWindow.c.RegisterEmail.Clear();
            RegisterWindow.c.RegisterPassword.Clear();
            RegisterWindow.c.RegisterConfirmPassword.Clear();
            RegisterWindow.c.RegisterNationality.Clear();
            RegisterWindow.c.RegisterDateOfBirth.Clear();
        }
    }
}
