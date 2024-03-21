using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using ProductApp.ViewModel;
using Repository.Context;
using Repository.Repository;
using Shared.Models;

namespace ProductApp.Controllers;
//[Route("Product")]
public class ProductController : Controller
{
    private readonly IRepository<Product, int> _productRepository;
    private readonly IMapper _mapper;
    private readonly IContext _context;

    public ProductController(IRepository<Product, int> productRepository, IMapper mapper, IContext context)
    {
        _productRepository = productRepository;
        _mapper = mapper;
        _context = context;
    }

    [HttpGet]
    public IActionResult Index()
    {
        return View(_productRepository.GetAll());
    }
    [HttpGet("Details/{id}")]
    public IActionResult Details(int id) 
    {
        var product = _mapper.Map<ListProduct> (_productRepository.Get(id));
        return View(product);
    }

    public IActionResult Add()
    {
        ViewBag.Categories = new SelectList(_context.Categories, "Id", "Name");
        return View(new CreateProduct());
    }

    [HttpPost("Add")]
    public IActionResult Add(CreateProduct product)
    {
        
        if (ModelState.IsValid)
        {
            //Should use Mapping to Convert CreateProduct Object to Product Object
            var productMapped = _mapper.Map<Product>(product);
            productMapped.Category = _context.Categories.FirstOrDefault(c => c.Id == product.ProductCategoryID);
            var createdProduct = _productRepository.Create(productMapped);
            return RedirectToAction("Details", new { id = createdProduct.Id });
        }
        return View(product);
        
    }
    public IActionResult Delete(int id)
    {
        _productRepository.Delete(id);
        return RedirectToAction("List");   
    }
    [HttpPut]
    public IActionResult Update(CreateProduct product)
    {
        //Should use Mapping to Convert CreateProduct Object to Product Object
        var productMapped = _mapper.Map<Product>(product);
        Console.WriteLine(productMapped);
        var createdProduct = _productRepository.Update(new Product());
        return RedirectToAction("Details", new { id = createdProduct.Id });
    }

    public IActionResult List()
    {
        var products = _productRepository.GetAll().Select(_mapper.Map<ListProduct>);
        return View(products);
    }
}
