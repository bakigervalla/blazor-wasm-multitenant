/*****************************************************************************

* Copyright (c) 2024 iRobot Corporation. All Rights Reserved.
*****************************************************************************/

using iWip.Infrastructure.Common;
using iWip.Infrastructure.Common.Pagination;
using iWip.Infrastructure.Models.Containers;
using iWip.Infrastructure.Models.Orders;
using iWip.Infrastructure.Models.PurchaseOrders.Requests;
using iWip.Infrastructure.Services.Http;
using System.Text.Json;

namespace iWip.Infrastructure.Services.PurchaseOrders
{
    public interface IPurchaseOrderService
    {
        // PO
        Task<PagingResponse<POHeader>> GetOrdersAsync(int pageSize, int page = 1);
        Task<PagingResponse<POHeader>> SearchOrdersAsync(string filter, int pageSize, int page);
        Task<POHeader> GetOrderByIdAsync(int id);
        Task<POHeaderRequest> GetOrderByLineIdAsync(int lineId);

        // POLines
        Task<PagingResponse<POLine>> GetPOLinesAsync(int pageSize, int page = 1);
        Task<PagingResponse<POLine>> SearchPOLinesAsync(string filter, int containerId, int pageSize, int page);
        Task<PagingResponse<POLine>> GetPOLinesByHeaderIdAsync(int poHeaderId, int page = 1);

        // containers
        Task<PagingResponse<POContainer>> GetAllContainersAsync(string poHeaderId, int pageSize, int page = 1);
        Task<PagingResponse<POContainer>> SearchContainersAsync(string filter, string poHeaderId, int pageSize, int page = 1);
        Task<HttpResponse<ContainerContent>> AddPOToContainer(POContainerContentRequest poLine);
        Task<HttpResponse<ContainerContent>> CreateNewContainerWithPOAsync(CreateNewContainerWithPORequest container);
    }

    public class PurchaseOrderService : IPurchaseOrderService
    {
        private readonly IHttpService _httpService;
        private readonly JsonSerializerOptions _options;

        public PurchaseOrderService(IHttpService httpService)
        {
            _httpService = httpService;
            _options = new JsonSerializerOptions { PropertyNameCaseInsensitive = true };
        }


        #region Purchase Orders

        public async Task<PagingResponse<POHeader>> GetOrdersAsync(int pageSize, int page = 1)
        {
            var response = await _httpService.GetAsync(Routes.Endpoints.GetOrders(pageSize, page));

            response.EnsureSuccessStatusCode();

            var content = await response.Content.ReadAsStringAsync();
            if (!response.IsSuccessStatusCode)
            {
                throw new ApplicationException(content);
            }
            var pagingResponse = new PagingResponse<POHeader>
            {
                Data = JsonSerializer.Deserialize<List<POHeader>>(content, _options),
                MetaData = JsonSerializer.Deserialize<MetaData>(response.Headers.GetValues("X-Pagination").First(), _options)
            };

            return pagingResponse;
        }

        public async Task<PagingResponse<POHeader>> SearchOrdersAsync(string filter, int pageSize, int page)
        {
            var response = await _httpService.GetAsync(Routes.Endpoints.SearchOrders(filter, pageSize, page));

            response.EnsureSuccessStatusCode();

            var content = await response.Content.ReadAsStringAsync();
            if (!response.IsSuccessStatusCode)
            {
                throw new ApplicationException(content);
            }
            var pagingResponse = new PagingResponse<POHeader>
            {
                Data = JsonSerializer.Deserialize<List<POHeader>>(content, _options),
                MetaData = JsonSerializer.Deserialize<MetaData>(response.Headers.GetValues("X-Pagination").First(), _options)
            };

            return pagingResponse;
        }

        public async Task<POHeader?> GetOrderByIdAsync(int id)
        {
            var response = await _httpService.GetAsync(Routes.Endpoints.GetOrderById(id));

            response.EnsureSuccessStatusCode();

            var result = await response.DeserializeContentAsync<POHeader>();

            return result;
        }

        public async Task<POHeaderRequest> GetOrderByLineIdAsync(int lineId)
        {
            var response = await _httpService.GetAsync(Routes.Endpoints.GethOrderByLineId(lineId));

            response.EnsureSuccessStatusCode();

            var result = await response.DeserializeContentAsync<POHeaderRequest>();

            return result;
        }

        #endregion


        #region PO Lines

        public async Task<PagingResponse<POLine>> GetPOLinesAsync(int pageSize, int page = 1)
        {
            var response = await _httpService.GetAsync(Routes.Endpoints.GetPOLines(pageSize, page));

            response.EnsureSuccessStatusCode();

            var content = await response.Content.ReadAsStringAsync();
            if (!response.IsSuccessStatusCode)
            {
                throw new ApplicationException(content);
            }
            var pagingResponse = new PagingResponse<POLine>
            {
                Data = JsonSerializer.Deserialize<List<POLine>>(content, _options),
                MetaData = JsonSerializer.Deserialize<MetaData>(response.Headers.GetValues("X-Pagination").First(), _options)
            };

            return pagingResponse;
        }

        public async Task<PagingResponse<POLine>> SearchPOLinesAsync(string filter, int containerId, int pageSize, int page)
        {
            var response = await _httpService.GetAsync(Routes.Endpoints.SearchPOLines(filter, containerId, pageSize, page));

            response.EnsureSuccessStatusCode();

            var content = await response.Content.ReadAsStringAsync();
            if (!response.IsSuccessStatusCode)
            {
                throw new ApplicationException(content);
            }
            var pagingResponse = new PagingResponse<POLine>
            {
                Data = JsonSerializer.Deserialize<List<POLine>>(content, _options),
                MetaData = JsonSerializer.Deserialize<MetaData>(response.Headers.GetValues("X-Pagination").First(), _options)
            };

            return pagingResponse;
        }

        public async Task<PagingResponse<POLine>> GetPOLinesByHeaderIdAsync(int poHeaderId, int page = 1)
        {
            var response = await _httpService.GetAsync(Routes.Endpoints.GetPOLinesByHeaderId(poHeaderId, page));

            response.EnsureSuccessStatusCode();

            var content = await response.Content.ReadAsStringAsync();
            if (!response.IsSuccessStatusCode)
            {
                throw new ApplicationException(content);
            }
            var pagingResponse = new PagingResponse<POLine>
            {
                Data = JsonSerializer.Deserialize<List<POLine>>(content, _options),
                MetaData = JsonSerializer.Deserialize<MetaData>(response.Headers.GetValues("X-Pagination").First(), _options)
            };

            return pagingResponse;
        }

        public async Task<PagingResponse<POContainer>> GetAllContainersAsync(string poHeaderId, int pageSize, int page = 1)
        {
            var response = await _httpService.GetAsync(Routes.Endpoints.GetPOContainers(poHeaderId, pageSize, page));

            response.EnsureSuccessStatusCode();

            var content = await response.Content.ReadAsStringAsync();
            if (!response.IsSuccessStatusCode)
            {
                throw new ApplicationException(content);
            }
            var pagingResponse = new PagingResponse<POContainer>
            {
                Data = JsonSerializer.Deserialize<List<POContainer>>(content, _options),
                MetaData = JsonSerializer.Deserialize<MetaData>(response.Headers.GetValues("X-Pagination").First(), _options)
            };

            return pagingResponse;
        }

        public async Task<PagingResponse<POContainer>> SearchContainersAsync(string filter, string poHeaderId, int pageSize, int page = 1)
        {
            var response = await _httpService.GetAsync(Routes.Endpoints.SearchPOContainers(filter, poHeaderId, pageSize, page));

            response.EnsureSuccessStatusCode();

            var content = await response.Content.ReadAsStringAsync();
            if (!response.IsSuccessStatusCode)
            {
                throw new ApplicationException(content);
            }
            var pagingResponse = new PagingResponse<POContainer>
            {
                Data = JsonSerializer.Deserialize<List<POContainer>>(content, _options),
                MetaData = JsonSerializer.Deserialize<MetaData>(response.Headers.GetValues("X-Pagination").First(), _options)
            };

            return pagingResponse;
        }

        public async Task<HttpResponse<ContainerContent>> CreateNewContainerWithPOAsync(CreateNewContainerWithPORequest request)
        {
            var response = await _httpService.PostAsync(Routes.Endpoints.NewContainerAndAddPO, request);

            if (!response.IsSuccessStatusCode)
                return new HttpResponse<ContainerContent> { HttpStatusCode = response.StatusCode };

            var content = await response.Content.ReadAsStringAsync();

            var responseContent = new HttpResponse<ContainerContent>
            {
                Response = JsonSerializer.Deserialize<ContainerContent>(content, _options),
                HttpStatusCode = response.StatusCode
            };

            return responseContent;
        }

        public async Task<HttpResponse<ContainerContent>> AddPOToContainer(POContainerContentRequest request)
        {
            var response = await _httpService.PostAsync(Routes.Endpoints.AddPOToContainer, request);

            if (!response.IsSuccessStatusCode)
                return new HttpResponse<ContainerContent> { HttpStatusCode = response.StatusCode };

            var content = await response.Content.ReadAsStringAsync();

            var responseContent = new HttpResponse<ContainerContent>
            {
                Response = JsonSerializer.Deserialize<ContainerContent>(content, _options),
                HttpStatusCode = response.StatusCode
            };

            return responseContent;
        }
        #endregion

    }
}
