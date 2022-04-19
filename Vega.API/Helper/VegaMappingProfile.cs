using AutoMapper;
using Vega.API.Core.Models;
using Vega.API.Resources;

namespace Vega.API.Helper
{
    public class VegaMappingProfile : Profile
    {
        public VegaMappingProfile()
        {
            //from domain to resourse
            CreateMap<Make, MakeResource>();
            CreateMap<Make, KeyValuePairResource>();

            CreateMap<Model, ModelResource>();
            CreateMap<Model, KeyValuePairResource>();

            CreateMap<Feature, KeyValuePairResource>();
            CreateMap<Vehicle, VehicleResource>()
            .ForMember(vr => vr.Contact, opt => opt.MapFrom(v => new ContactResource() { Name = v.ContactName, Phone = v.ContactPhone, Email = v.ContactEmail }))
            .ForMember(vr => vr.Make, opt => opt.MapFrom(v => v.Model.Make));

            CreateMap<Vehicle, SaveVehicleResource>()
            .ForMember(vr => vr.Contact, opt => opt.MapFrom(v => new ContactResource() { Name = v.ContactName, Phone = v.ContactPhone, Email = v.ContactEmail }))
            .ForMember(vr => vr.Features, opt => opt.MapFrom(v => v.Features.Select(f => f.Id)));


            // from resource to domain

            CreateMap<SaveVehicleResource, Vehicle>()
            .ForMember(v => v.Id, opt => opt.Ignore())
            .ForMember(v => v.ContactName, opt => opt.MapFrom(vr => vr.Contact.Name))
            .ForMember(v => v.ContactEmail, opt => opt.MapFrom(vr => vr.Contact.Email))
            .ForMember(v => v.ContactPhone, opt => opt.MapFrom(vr => vr.Contact.Phone))
            .ForMember(v => v.Features, opt => opt.Ignore())
            .AfterMap((vr, v) =>
            {
                var removedFeatures = v.Features.Where(f => !vr.Features.Any(featureId => f.Id == featureId)).ToList();
                foreach (var item in removedFeatures)
                    v.Features.Remove(item);

                var addedFeatures = vr.Features.Where(featureId => !v.Features.Any(f => f.Id == featureId))
                .Select(featureId => new Feature() { Id = featureId });
                foreach (var item in addedFeatures)
                    v.Features.Add(item);

            });
        }
    }
}