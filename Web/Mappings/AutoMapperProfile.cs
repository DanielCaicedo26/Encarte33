
using AutoMapper;
using Entity.Dto;
using Entity.models;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace Web.Mappings
{
    public class AutoMapperProfile : Profile
    {
        public AutoMapperProfile()
        {
            // Mapeos para Factura
            CreateMap<Factura, FacturaDto>()
                .ForMember(dest => dest.DetallesFactura, opt => opt.MapFrom(src => src.DetallesFactura));

            CreateMap<CrearFacturaDto, Factura>()
                .ForMember(dest => dest.FacturaId, opt => opt.Ignore())
                .ForMember(dest => dest.Subtotal, opt => opt.Ignore())
                .ForMember(dest => dest.Impuesto, opt => opt.Ignore())
                .ForMember(dest => dest.Total, opt => opt.Ignore())
                .ForMember(dest => dest.Estado, opt => opt.MapFrom(src => "Pendiente"))
                .ForMember(dest => dest.DetallesFactura, opt => opt.MapFrom(src => src.DetallesFactura));

            CreateMap<ActualizarFacturaDto, Factura>()
                .ForMember(dest => dest.FacturaId, opt => opt.Ignore())
                .ForMember(dest => dest.NumeroFactura, opt => opt.Ignore())
                .ForMember(dest => dest.Subtotal, opt => opt.Ignore())
                .ForMember(dest => dest.Impuesto, opt => opt.Ignore())
                .ForMember(dest => dest.Total, opt => opt.Ignore())
                .ForMember(dest => dest.DetallesFactura, opt => opt.MapFrom(src => src.DetallesFactura));

            // Mapeos para DetalleFactura
            CreateMap<DetalleFactura, DetalleFacturaDto>();

            CreateMap<CrearDetalleFacturaDto, DetalleFactura>()
                .ForMember(dest => dest.DetalleFacturaId, opt => opt.Ignore())
                .ForMember(dest => dest.FacturaId, opt => opt.Ignore())
                .ForMember(dest => dest.Subtotal, opt => opt.Ignore())
                .ForMember(dest => dest.Factura, opt => opt.Ignore());

            CreateMap<ActualizarDetalleFacturaDto, DetalleFactura>()
                .ForMember(dest => dest.FacturaId, opt => opt.Ignore())
                .ForMember(dest => dest.Subtotal, opt => opt.Ignore())
                .ForMember(dest => dest.Factura, opt => opt.Ignore());
        }
    }
}