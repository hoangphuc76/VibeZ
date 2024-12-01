using Microsoft.AspNetCore.Mvc;
using VibeZOData.Services.ElasticSearch;
using VibeZDTO;
using VibeZOData.Models;
using System.Threading.Tasks;
using System.Threading;
using BusinessObjects;

namespace VibeZOData.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class ElasticSearchController : ControllerBase
    {
        private readonly DataSyncService _dataSyncService;
        private readonly IElasticSearchService<SearchModel> _elasticSearchService;

        public ElasticSearchController(DataSyncService dataSyncService, IElasticSearchService<SearchModel> elasticSearchService)
        {
            _dataSyncService = dataSyncService;
            _elasticSearchService = elasticSearchService;
        }

        // Endpoint để đồng bộ dữ liệu từ SQL Server vào Elasticsearch
        [HttpPost("sync-sql-to-es")]
        public async Task<IActionResult> SyncDataToElasticsearch()
        {
            try
            {
                await _dataSyncService.SyncDataToElasticsearch();
                return Ok(new { Message = "Data synced from SQL Server to Elasticsearch successfully." });
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Error syncing data: {ex.Message}");
            }
        }

        // Endpoint để tìm kiếm tài liệu chung cho tất cả các loại thực thể trong Elasticsearch
        [HttpGet("search")]
        public async Task<IActionResult> SearchDocuments([FromQuery] string query, [FromQuery] string? fieldName = null, CancellationToken cancellationToken = default)
        {
            try
            {
                var result = await _elasticSearchService.SearchDocumentAsync(query, fieldName, cancellationToken);
                return Ok(result);
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Error searching documents: {ex.Message}");
            }
        }
    }
}
