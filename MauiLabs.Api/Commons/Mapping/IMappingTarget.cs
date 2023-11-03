using AutoMapper;

namespace MauiLabs.Api.Commons.Mapping
{
    public interface IMappingTarget<TModel> where TModel : class
    {
        public void ConfigureMapping(Profile profile) => profile.CreateMap(this.GetType(), typeof(TModel)).ReverseMap();
    }
}
