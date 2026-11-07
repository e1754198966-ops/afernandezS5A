using afernandezS5A.Repositories;
using afernandezS5A.Utils; // Asegúrate de importar el namespace de tu FileAccessHelper

namespace afernandezS5A
{
    public static class MauiProgram
    {
        public static MauiApp CreateMauiApp()
        {
            var builder = MauiApp.CreateBuilder();
            builder
                .UseMauiApp<App>()
                .ConfigureFonts(fonts =>
                {
                    fonts.AddFont("OpenSans-Regular.ttf", "OpenSansRegular");
                    fonts.AddFont("OpenSans-Semibold.ttf", "OpenSansSemibold");
                });

            // Usamos tu FileAccessHelper para obtener la ruta de la base de datos
            string dbPath = FileAccessHelper.GetLocalFilePath("people.db3");

            // Registramos el repositorio y la vista
            builder.Services.AddSingleton(s => new PersonRepository(dbPath));
            builder.Services.AddTransient<Views.vPersona>();

            return builder.Build();
        }
    }
}