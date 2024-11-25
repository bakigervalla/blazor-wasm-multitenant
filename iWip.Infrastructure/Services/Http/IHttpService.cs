/*****************************************************************************

* Copyright (c) 2024 iRobot Corporation. All Rights Reserved.
*****************************************************************************/

namespace iWip.Infrastructure.Services.Http;

public interface IHttpService
{
    Task<HttpResponseMessage> GetAsync(string uri);
    Task<HttpResponseMessage> PostAsync(string uri, object value);
    Task<HttpResponseMessage> PutAsync(string uri, object value);
    Task<HttpResponseMessage> DeleteAsync(string uri);
}
