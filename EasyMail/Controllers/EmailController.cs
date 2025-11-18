using Application.DTOs.Requests;
using Application.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace EasyMail.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class EmailController : ControllerBase
    {
        private readonly ILogger<EmailController> _logger;
        private readonly IEmailService _emailService;

        public EmailController(ILogger<EmailController> logger, IEmailService emailService)
        {
            _emailService = emailService;
            _logger = logger;
        }

        [HttpPost]
        public async Task<IActionResult> SendEmailAsync([FromBody] SendEmailRequest request)
        {
            try
            {
                _logger.LogInformation("Received email send request to {To} with subject {Subject}", request.To, request.Subject);

                if (!ModelState.IsValid)
                {
                    return BadRequest(ModelState);
                }

                var response = await _emailService.SendEmailAsync(request);
                _logger.LogInformation("Email sent successfully to {To}", request.To);

                return Ok(response);
            }
            catch (Exception ex)
            {
                return BadRequest(ex);
            }

        }

        [HttpPost]
        public async Task<IActionResult> EmailAllClientsAsync([FromBody] EmailAllClientsRequest request)
        {

            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            try
            {
                _logger.LogInformation("Received email to all clients from database request with subject: {Subject}", request.Subject);

                return Ok(await _emailService.EmailAllClientsAsync(request));
            }
            catch (Exception ex)
            {
                return BadRequest(ex);
            }
        }
    }
}
