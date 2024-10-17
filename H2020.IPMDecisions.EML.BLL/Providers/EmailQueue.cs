using H2020.IPMDecisions.EML.Core.Dtos;
using Org.BouncyCastle.Bcpg;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Collections;
using Org.BouncyCastle.Asn1.BC;

using H2020.IPMDecisions.EML.BLL;
using H2020.IPMDecisions.EML.BLL.Helpers;
using H2020.IPMDecisions.EML.Core.EmailTemplates;
using H2020.IPMDecisions.EML.Core.Models;

using System.Threading;
using System.Collections.Concurrent;
using Microsoft.CodeAnalysis.CSharp.Syntax;
using NLog;
using Microsoft.Extensions.Logging;
using MimeKit;
using SendGrid.Helpers.Mail;

using MailKit.Net.Smtp;

namespace H2020.IPMDecisions.EML.BLL.Providers;
public sealed class EmailQueue : IEmailQueue
{

    public BlockingCollection<InactiveUserDto> QueueDataStructure { get; set; } = new BlockingCollection<InactiveUserDto>();


    public EmailQueue()
    {
    }

    public void BeginProcessing(Func<InactiveUserDto, Task<GenericResponse>> processor)
    {
        Task.Run(async () =>
        {
            while(!QueueDataStructure.IsCompleted)
            {
                InactiveUserDto inactiveUserDto = null;

                try
                {
                    inactiveUserDto = QueueDataStructure.Take();
                }
                catch(Exception ex)
                {
                }

                if(inactiveUserDto != null)
                {
                    var emailSendResponse = await processor(inactiveUserDto);
                    if(!emailSendResponse.IsSuccessful)
                    {
                        QueueDataStructure.Add(inactiveUserDto);
                    }
                }
            }
            Console.WriteLine("Queue has been processed");
        });
    }
    public void Add(InactiveUserDto inactiveUserDto)
    {
        QueueDataStructure.Add(inactiveUserDto);
    }
}
