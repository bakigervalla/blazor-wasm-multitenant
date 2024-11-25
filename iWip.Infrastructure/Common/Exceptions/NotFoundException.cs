/*****************************************************************************

* Copyright (c) 2024 iRobot Corporation. All Rights Reserved.
*****************************************************************************/

namespace iWip.Infrastructure.Common.Exceptions;

public class NotFoundException : ServerException
{


    public NotFoundException(string message)
        : base(message, System.Net.HttpStatusCode.NotFound)
    {
    }
    public NotFoundException(string name, object key)
         : base($"Entity \"{name}\" ({key}) was not found.", System.Net.HttpStatusCode.NotFound)
    {
    }
}
