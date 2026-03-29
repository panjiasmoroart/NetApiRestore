using AutoMapper;
using NetApiRestore.DTOs;
using NetApiRestore.Entities;

namespace NetApiRestore.RequestHelpers
{
	public class MappingProfiles: Profile
	{
		public MappingProfiles()
		{
			CreateMap<CreateProductDto, Product>();
		}
	}
}
