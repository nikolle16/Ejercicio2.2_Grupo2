using Ejercicio2._2_Grupo2.Controllers;

namespace Ejercicio2._2_Grupo2
{
    public partial class App : Application
    {
        static Controllers.FirmasController dbfirma;

        public static Controllers.FirmasController DataBase
        {
            get
            {
                if (dbfirma == null)
                {
                    dbfirma = new Controllers.FirmasController();
                }
                return dbfirma;
            }
        }

        public App()
        {
            InitializeComponent();
            MainPage = new NavigationPage(new Views.PageInit());
        }
    }
}
