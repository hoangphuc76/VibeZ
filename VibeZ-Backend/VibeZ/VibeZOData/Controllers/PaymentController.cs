using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging; // Thêm using cho ILogger
using Repositories.IRepository;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using BusinessObjects;
using Repositories.UnitOfWork;

namespace VibeZOData.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PaymentController : ControllerBase
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly ILogger<PaymentController> _logger; // Thêm ILogger vào controller

        public PaymentController(IUnitOfWork unitOfWork, ILogger<PaymentController> logger)
        {
            _unitOfWork = unitOfWork;
            _logger = logger; // Khởi tạo logger
        }

        [HttpGet("all")]
        public async Task<ActionResult<IEnumerable<Payment>>> GetAllPayments()
        {
            _logger.LogInformation("Fetching all payments."); // Log thông tin
            var payments = await _unitOfWork.Payments.GetAll();
            return Ok(payments);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<Payment>> GetPaymentById(Guid id)
        {
            _logger.LogInformation($"Fetching payment with ID: {id}"); // Log thông tin
            var payment = await _unitOfWork.Payments.GetById(id);
            if (payment == null)
            {
                _logger.LogWarning($"No payment found with ID: {id}"); // Log cảnh báo
                return NotFound($"No payment found with ID {id}");
            }
            return Ok(payment);
        }

        [HttpPost("create")]
        public async Task<ActionResult> CreatePayment([FromBody] Payment payment)
        {
            if (!ModelState.IsValid)
            {
                _logger.LogWarning("Model state is invalid."); // Log cảnh báo
                return BadRequest(ModelState);
            }

            payment.Id = Guid.NewGuid(); // Tạo ID cho thanh toán mới
            await _unitOfWork.Payments.Add(payment);
            await _unitOfWork.Complete(); // Lưu thay đổi vào database
            _logger.LogInformation($"Payment created successfully with ID: {payment.Id}"); // Log thông tin
            return Ok(new { message = "Payment created successfully" });
        }

        [HttpPut("update/{id}")]
        public async Task<ActionResult> UpdatePayment(Guid id, [FromBody] Payment updatedPayment)
        {
            if (!ModelState.IsValid)
            {
                _logger.LogWarning("Model state is invalid."); // Log cảnh báo
                return BadRequest(ModelState);
            }

            var payment = await _unitOfWork.Payments.GetById(id);
            if (payment == null)
            {
                _logger.LogWarning($"No payment found with ID: {id}"); // Log cảnh báo
                return NotFound($"No payment found with ID {id}");
            }

            updatedPayment.Id = id; // Đảm bảo ID không thay đổi
            await _unitOfWork.Payments.Update(payment);
            await _unitOfWork.Complete(); // Lưu thay đổi vào database
            _logger.LogInformation($"Payment with ID: {id} updated successfully."); // Log thông tin
            return Ok(new { message = "Payment updated successfully" });
        }

        [HttpDelete("delete/{id}")]
        public async Task<ActionResult> DeletePayment(Guid id)
        {
            var payment = await _unitOfWork.Payments.GetById(id);
            if (payment == null)
            {
                _logger.LogWarning($"No payment found with ID: {id}"); // Log cảnh báo
                return NotFound($"No payment found with ID {id}");
            }

            await _unitOfWork.Payments.Delete(payment);
            await _unitOfWork.Complete(); // Lưu thay đổi vào database
            _logger.LogInformation($"Payment with ID: {id} deleted successfully."); // Log thông tin
            return Ok(new { message = "Payment deleted successfully" });
        }
    }
}
