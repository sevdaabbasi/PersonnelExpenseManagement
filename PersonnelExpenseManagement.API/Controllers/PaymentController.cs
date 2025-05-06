using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using PersonnelExpenseManagement.Application.DTOs.Payment;
using PersonnelExpenseManagement.Application.Interfaces;

namespace PersonnelExpenseManagement.API.Controllers;

[ApiController]
[Route("api/[controller]")]
[Authorize]
public class PaymentController : ControllerBase
{
    private readonly IPaymentService _paymentService;

    public PaymentController(IPaymentService paymentService)
    {
        _paymentService = paymentService;
    }

    [HttpPost("process/{expenseId}")]
    [Authorize(Roles = "Admin")]
    public async Task<ActionResult<PaymentResultDto>> ProcessPayment(int expenseId)
    {
        try
        {
            var result = await _paymentService.ProcessPaymentAsync(expenseId);
            return Ok(result);
        }
        catch (ArgumentException ex)
        {
            return NotFound(ex.Message);
        }
        catch (InvalidOperationException ex)
        {
            return BadRequest(ex.Message);
        }
    }

    [HttpGet("status/{expenseId}")]
    public async Task<ActionResult<PaymentStatusDto>> GetPaymentStatus(int expenseId)
    {
        try
        {
            var status = await _paymentService.GetPaymentStatusAsync(expenseId);
            return Ok(status);
        }
        catch (ArgumentException ex)
        {
            return NotFound(ex.Message);
        }
    }

    [HttpGet("validate/{expenseId}")]
    [Authorize(Roles = "Admin")]
    public async Task<ActionResult<PaymentValidationDto>> ValidatePayment(int expenseId)
    {
        try
        {
            var validation = await _paymentService.ValidatePaymentAsync(expenseId);
            return Ok(validation);
        }
        catch (ArgumentException ex)
        {
            return NotFound(ex.Message);
        }
    }

    [HttpPost("simulate/{expenseId}")]
    [Authorize(Roles = "Admin")]
    public async Task<ActionResult<PaymentSimulationDto>> SimulatePayment(int expenseId)
    {
        try
        {
            var simulation = await _paymentService.SimulatePaymentAsync(expenseId);
            return Ok(simulation);
        }
        catch (ArgumentException ex)
        {
            return NotFound(ex.Message);
        }
    }
} 