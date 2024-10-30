using H2020.IPMDecisions.EML.Core.Dtos;
using System;
using System.Threading.Tasks;
using H2020.IPMDecisions.EML.Core.Models;
using System.Collections.Concurrent;
using System.Threading;

namespace H2020.IPMDecisions.EML.BLL.Providers;
public sealed class EmailQueue : IEmailQueue
{
    public BlockingCollection<InactiveUserDto> QueueDataStructure { get; set; } = new BlockingCollection<InactiveUserDto>();

    public EmailQueue()
    {
    }

    public void BeginProcessing(Func<InactiveUserDto, Task<GenericResponse>> processor)
    {
        Task.Factory.StartNew(() =>
        {
            while (!QueueDataStructure.IsCompleted)
            {
                InactiveUserDto inactiveUserDto = null;
                try
                {
                    inactiveUserDto = QueueDataStructure.Take();
                }
                catch (Exception ex)
                {
                }

                if (inactiveUserDto != null)
                {
                    processor(inactiveUserDto);
                }
            }
        }, TaskCreationOptions.LongRunning);
    }
    public void Add(InactiveUserDto inactiveUserDto)
    {
        QueueDataStructure.Add(inactiveUserDto);
    }
}
