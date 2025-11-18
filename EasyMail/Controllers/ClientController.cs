using Application.DTOs.Requests;
using Application.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace EasyMail.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class ClientController(ILogger<ClientController> logger, IClientService clientService) : ControllerBase
    {
        private readonly ILogger<ClientController> _logger = logger;
        private readonly IClientService _clientService = clientService;

        [HttpPost]
        public async Task<IActionResult> Create([FromBody] CreateClientRequest request)
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    return BadRequest(ModelState);
                }

                var createdClient = await _clientService.CreateClient(request);
                _logger.LogInformation("Client created successfully: {@CreatedClient}", createdClient);
                
                return Ok(createdClient);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Failed to insert new client");
                return BadRequest("Failed to insert new client");
            }
        }

        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            _logger.LogInformation("Received get all clients request.");
            var clients = await _clientService.GetAllClients();
            return Ok(clients);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateClientAsync(string id, [FromBody] UpdateClientRequest request)
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    return BadRequest(ModelState);
                }

                var updatedClient = await _clientService.UpdateClientAsync(id, request);
                _logger.LogInformation("Client updated successfully: {@UpdatedClient}", updatedClient);

                return Ok(updatedClient);
            }
            catch (ArgumentException ex)
            {
                return NotFound(ex.Message);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Failed to update client");
                return BadRequest("Failed to update client");
            }
        }
    }
}