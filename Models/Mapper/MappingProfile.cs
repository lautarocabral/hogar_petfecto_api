﻿using AutoMapper;
using hogar_petfecto_api.Models.Dtos;
using hogar_petfecto_api.Models.Perfiles;
using hogar_petfecto_api.Models.Seguridad;

namespace hogar_petfecto_api.Models.Mapper
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            CreateMap<Usuario, UsuarioDto>();

            CreateMap<Persona, PersonaDto>();

            CreateMap<Localidad, LocalidadDto>();

            CreateMap<Provincia, ProvinciaDto>();

            CreateMap<Grupo, GrupoDto>();

            CreateMap<Permiso, PermisoDto>();

            CreateMap<Perfil, PerfilDto>()
                .ForMember(dest => dest.TipoPerfil, opt => opt.MapFrom(src => src.TipoPerfil));

            CreateMap<TipoPerfil, TipoPerfilDto>();

            CreateMap<Mascota, MascotaDto>();

            CreateMap<TipoMascota, TipoMascotaDto>();

            CreateMap<Postulacion, PostulacionDto>();

            CreateMap<Adoptante, AdoptanteDto>();

            CreateMap<EstadoPostulacion, EstadoPostulacion>();

            CreateMap<Producto, ProductoDto>();

            CreateMap<Categoria, CategoriaDto>();

            CreateMap<Adopcion, AdopcionDto>();


            // CreateMap<Producto, ProductoDto>()
            //.ForMember(dest => dest.Categoria, opt => opt.MapFrom(src => src.Categoria)) // Si es necesario mapear manualmente
            //.ForMember(dest => dest.Protectora, opt => opt.MapFrom(src => src.Protectora))
            //.ReverseMap();

            CreateMap<Protectora, ProtectoraDto>();

            CreateMap<Pedido, PedidoDto>();

            CreateMap<LineaPedido, LineaPedidoDto>();

            CreateMap<Cliente, ClienteDto>();

            CreateMap<Suscripcion, SuscripcionDto>();

            CreateMap<Veterinaria, VeterinariaDto>();

            CreateMap<Oferta, OfertaDto>();


        }
    }
}
