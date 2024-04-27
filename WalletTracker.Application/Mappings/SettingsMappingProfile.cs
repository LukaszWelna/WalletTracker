using AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WalletTracker.Application.Settings;
using WalletTracker.Application.Settings.Commands.CreateIncomeCategory;
using WalletTracker.Domain.Entities;

namespace WalletTracker.Application.Mappings
{
    public class SettingsMappingProfile : Profile
    {
        public SettingsMappingProfile()
        {
            CreateMap<CreateIncomeCategoryCommand, IncomeCategoryAssignedToUser>();
        }
    }
}
