using AutoMapper;
using Shopi.Application.ViewModels;
using Shopi.DomainModel.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.ConstrainedExecution;
using System.Text;
using System.Threading.Tasks;

namespace Shopi.Application.Helpers;

public static class MapperHelper
{
    private static readonly IMapper _mapper;

    static MapperHelper()
    {
        var config = new MapperConfiguration(cfg =>
        {
            cfg.CreateMap<UserEntity, UserViewModel>();
        });
        _mapper = config.CreateMapper();
    }

    public static UserViewModel ToViewModel(this UserEntity entity)
    {
        return _mapper.Map<UserViewModel>(entity);
    }
    public static List<UserViewModel> ToViewModel(this List<UserEntity> entities)
    {
        return entities.Select(x => new UserViewModel
        {
            Id = x.Id,
            Name = x.Name,
            LastName = x.LastName,
            Phone = x.Phone
        }).ToList();
    }
}
