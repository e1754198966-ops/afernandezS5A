using System.Xml.Linq;

namespace afernandezS5A.Views;

public partial class vPersona : ContentPage
{
    public vPersona()
    {
        InitializeComponent();
    }

    private void btnMostar_Clicked(object sender, EventArgs e)
    {
        List<Models.Persona> people = App._personRepo.GetAllPerson();
        listaPersonas.ItemsSource = people;
    }

    private void btnAgregar_Clicked(object sender, EventArgs e)
    {
        lblStatusMessage.Text = "";
        App._personRepo.AddNewPerson(txtName.Text);
        lblStatusMessage.Text = App._personRepo.StatusMessage;

        // Opcional: Actualizar la lista automáticamente al agregar
        btnMostar_Clicked(sender, e);
        txtName.Text = "";
    }

    // --- NUEVO MÉTODO PARA ELIMINAR ---
    private void btnEliminar_Clicked(object sender, EventArgs e)
    {
        lblStatusMessage.Text = "";

        Button button = sender as Button;
        if (button != null)
        {
            // Obtenemos el ID del registro a través del CommandParameter
            int idPersona = (int)button.CommandParameter;

            // Llamamos al método de eliminar de tu repositorio
            App._personRepo.DeletePerson(idPersona);

            lblStatusMessage.Text = App._personRepo.StatusMessage;

            // Refrescamos la lista automáticamente para que se refleje el cambio
            btnMostar_Clicked(sender, e);
        }
    }

    private void btnActualizar_Clicked(object sender, EventArgs e)
    {
        lblStatusMessage.Text = "";

        if (sender is Button button && button.CommandParameter is int idPersona)
        {
            // Tomamos el texto del Entry actual como el nuevo nombre
            string nuevoNombre = txtName.Text;

            // Llamamos al método de actualizar del repositorio
            App._personRepo.UpdatePerson(idPersona, nuevoNombre);

            lblStatusMessage.Text = App._personRepo.StatusMessage;

            // Refrescamos la lista y limpiamos la caja de texto
            btnMostar_Clicked(sender, e);
            txtName.Text = "";
        }
    }
}