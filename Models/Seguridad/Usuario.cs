using hogar_petfecto_api.Models.Seguridad;
using hogar_petfecto_api.Models;

public class Usuario
{
    private Usuario() { }
    public Usuario( string email, string contraseña, List<Grupo> grupos, Persona persona, List<int> hasToUpdateProfile, bool userActivo)
    {
        Email = email;
        Contraseña = contraseña;
        Grupos = grupos;
        PersonaDni = persona.Dni;
        Persona = persona;
        HasToUpdateProfile = hasToUpdateProfile;
        UserActivo = userActivo;
    }

    public int Id { get; private set; }
    public string Email { get; private set; }
    public string Contraseña { get; private set; }
    public List<Grupo> Grupos { get; private set; }
    public string PersonaDni { get; private set; } // Foreign key to Persona
    public Persona Persona { get; private set; }
    public List<int> HasToUpdateProfile { get; private set; }
    public bool UserActivo { get; private set; }

    public void UpdateUser(string email, string password)
    {
        Email = email;
        Contraseña = password;
    }

    public void UpdateUserActiveState(bool userActivo)
    {
        UserActivo = userActivo;
    }

    public void UpdateGrupos(List<Grupo> nuevosGrupos)
    {
        Grupos = nuevosGrupos;
    }

    public void UpdateListOfHasToUpdateProfile(List<int> nuevosPermisos)
    {
        HasToUpdateProfile = nuevosPermisos;
    }


}
