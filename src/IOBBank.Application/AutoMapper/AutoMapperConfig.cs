using AutoMapper;
using IOBBank.Application.Commands.BankCommands.CreateBankAccountCommand;
using IOBBank.Domain.Entidades;

namespace IOBBank.Application.AutoMapper;

public class AutoMapperConfig : Profile
{
    public AutoMapperConfig()
    {

        CreateMap<BankAccount, CreateBankAccountInput>();
      
    }
}
