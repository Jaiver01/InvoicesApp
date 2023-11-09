using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using InvoicesApp.Models;
using InvoicesApp.Repositories.Interfaces;

namespace InvoicesApp.Controllers;

public class InvoicesController : Controller
{
    private readonly ILogger<InvoicesController> _logger;
    private readonly IInvoiceRepository _invoiceRepository;
    private readonly IProductRepository _productRepository;
    private readonly ICustomerRepository _customerRepository;

    public InvoicesController(
        ILogger<InvoicesController> logger,
        IInvoiceRepository invoiceRepository,
        IProductRepository productRepository,
        ICustomerRepository customerRepository
    )
    {
        _logger = logger;
        _invoiceRepository = invoiceRepository;
        _productRepository = productRepository;
        _customerRepository = customerRepository;
    }

    public async Task<IActionResult> Index()
    {
        List<InvoiceViewModel> invoices = await _invoiceRepository.GetAllAsync();
        List<CustomerModel> customers = await _customerRepository.GetAllAsync();

        ViewBag.Customers = customers;

        return View(invoices);
    }

    public async Task<IActionResult> Create()
    {
        List<ProductModel> products = await _productRepository.GetAllAsync();
        List<CustomerModel> customers = await _customerRepository.GetAllAsync();

        ViewBag.Products = products;
        ViewBag.Customers = customers;

        return View();
    }

    [HttpPost]
    public async Task<IActionResult> Create([FromBody] InvoiceModel invoice)
    {
        InvoiceModel invoiceCreated = await _invoiceRepository.CreateAsync(invoice);

        return StatusCode(StatusCodes.Status201Created, invoiceCreated);
    }

    [HttpGet("Invoices/GetInvoicesByCustomer/{customerId}")]
    public async Task<IActionResult> GetInvoicesByCustomer(int customerId)
    {
        List<InvoiceViewModel> result = await _invoiceRepository.GetAllByCustomerAsync(customerId);
        return StatusCode(StatusCodes.Status200OK, result);
    }

    [HttpGet("Invoices/GetInvoicesByNumber/{invoiceNumber}")]
    public async Task<IActionResult> GetInvoicesByNumber(int invoiceNumber)
    {
        List<InvoiceViewModel> result = await _invoiceRepository.GetAllByNumberAsync(invoiceNumber);
        return StatusCode(StatusCodes.Status200OK, result);
    }

    [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
    public IActionResult Error()
    {
        return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
    }
}
