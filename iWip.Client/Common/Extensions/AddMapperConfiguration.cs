/*****************************************************************************

* Copyright (c) 2024 iRobot Corporation. All Rights Reserved.
*****************************************************************************/

using AutoMapper;
using iWip.Client.Models.Auth;
using iWip.Client.Models.Containers;
using iWip.Client.Models.PurchaseOrders;
using iWip.Client.Models.Shipment;
using iWip.Client.Models.Tenants;
using iWip.Client.Models.Users;
using iWip.Infrastructure.Models.Auth;
using iWip.Infrastructure.Models.Containers;
using iWip.Infrastructure.Models.Containers.Requests;
using iWip.Infrastructure.Models.Orders;
using iWip.Infrastructure.Models.Orders.Requests;
using iWip.Infrastructure.Models.PurchaseOrders.Requests;
using iWip.Infrastructure.Models.Shipment;
using iWip.Infrastructure.Models.Shipment.Request;
using iWip.Infrastructure.Models.Shipment.Response;
using iWip.Infrastructure.Models.Tenants;
using iWip.Infrastructure.Models.Users;
using Microsoft.AspNetCore.Components.WebAssembly.Hosting;

namespace iWip.Client.Common.Extensions;

internal static class AutoMapperExtension
{
    internal static IServiceCollection AddAutoMapperConfiguration(this IServiceCollection services, WebAssemblyHostConfiguration config)
    {
        var mapperConfiguration = new MapperConfiguration(configuration =>
        {
            configuration.AddProfile(new MappingProfile());
        });
        services.AddSingleton(mapperConfiguration.CreateMapper());

        return services;
    }
}

public class MappingProfile : Profile
{
    public MappingProfile()
    {
        // Container
        CreateMap<ContainerModel, Container>().ReverseMap();
        CreateMap<ContainerCreateUpdate, ContainerModel>().ReverseMap();
        CreateMap<ContainerContent, POLine>().ReverseMap(); // creating new container content
        CreateMap<ContainerContentCreateUpdate, POLineUpdate>().ReverseMap(); // creating new container content
        CreateMap<ContainerContentCreateUpdate, ContainerContent>().ReverseMap(); // submit request partially
        

        //CreateMap<ContainerContentCreateUpdate, POLineCreateUpdate>().ReverseMap();
        //CreateMap<ContainerContent, ContainerContentCreateUpdate>().ReverseMap();

        // PO
        CreateMap<POLineUpdate, POLine>().ReverseMap();
        CreateMap<Container, POContainerContentRequest>().ReverseMap();
        CreateMap<POHeaderRequest, POContainerContentRequest>().ReverseMap();
        CreateMap<Container, POContainer>().ReverseMap();
        CreateMap<CreateNewContainerWithPOModel, CreateNewContainerWithPORequest>().ReverseMap();
        CreateMap<POHeaderRequest, CreateNewContainerWithPORequest>();
        CreateMap<POContainer, POContainerContentRequest>();

        // Shipment
        CreateMap<ShippingHeader, CreateShippingModel>();
        CreateMap<POLinesResponse, ShippedOrders>();
        CreateMap<ShippedOrders, POLinesRequest>();
        CreateMap<ShippedOrdersResponse, ShippedOrders>();
        CreateMap<POLinesResponse, POLinesRequest>();
        CreateMap<CreateShippingModel, ShipmentCreateUpdate>();
        CreateMap<Invoice, UpdateInvoiceRequest>();
        CreateMap<CreateFileModel, ShippingDocument>().ReverseMap();
        CreateMap<ShippingListResponse, CreateShippingModel>().ReverseMap();

        // Auth
        CreateMap<LoginRequest, LoginFormModel>().ReverseMap();
        CreateMap<CreateUpdateUserDto, CreateUpdateUser>().ReverseMap();
        CreateMap<SetNewPasswordModel, SetNewPasswordRequest>().ReverseMap();
        CreateMap<ChangePasswordModel, ChangePasswordRequest>();

        // Tenants
        CreateMap<CreateUpdateTenantDto, CreateUpdateTenant>().ReverseMap();
        CreateMap<Tenant, CreateUpdateTenant>().ReverseMap();

        // Users
        CreateMap<User, CreateUpdateUser>().ReverseMap();
        CreateMap<Role, CreateUpdateRole>().ReverseMap();
        CreateMap<CreateUpdateRoleDto, CreateUpdateRole>().ReverseMap();
    }
}