using hogar_petfecto_api.Models.Seguridad;
using hogar_petfecto_api.Models;

public class Usuario
{
    private Usuario() { }
    public Usuario( string email, string contraseña, List<Grupo> grupos, Persona persona, List<int> hasToUpdateProfile)
    {
        Email = email;
        Contraseña = contraseña;
        Grupos = grupos;
        PersonaDni = persona.Dni;
        Persona = persona;
        HasToUpdateProfile = hasToUpdateProfile;
    }

    public int Id { get; private set; }
    public string Email { get; private set; }
    public string Contraseña { get; private set; }
    public List<Grupo> Grupos { get; private set; }
    public string PersonaDni { get; set; } // Foreign key to Persona
    public Persona Persona { get; set; }
    public List<int> HasToUpdateProfile { get; private set; }

    public void UpdateUser(string email, string password)
    {
        Email = email;
        Contraseña = password;
    }

    public void UpdateGrupos(List<Grupo> newGrupos)
    {
        Grupos = newGrupos;
    }

    public void UpdateListOfHasToUpdateProfile(List<int> newGrupos)
    {
        HasToUpdateProfile = newGrupos;
    }

}
