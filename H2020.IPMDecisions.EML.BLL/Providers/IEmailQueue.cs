using H2020.IPMDecisions.EML.Core.Dtos;
using H2020.IPMDecisions.EML.Core.Models;
using System;
using System.Threading.Tasks;

namespace H2020.IPMDecisions.EML.BLL.Providers;
public interface IEmailQueue
{
    void Add(InactiveUserDto inactiveUserDto);
    void BeginProcessing(Func<InactiveUserDto, Task<GenericResponse>> processor);
}
