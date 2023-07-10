using Infrastructure.Data;
using Core.Entities;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Core.Interfaces;
using Core.Specifications;
using API.DTO;
using AutoMapper;
using API.Errors;
using Microsoft.AspNetCore.Http;
using API.Helpers;

namespace API.Controllers {
[ApiController]
[Route("api/[controller]")]
public class ProductsController : ControllerBase
{

  private readonly IGenericRepository<Product> _productsRepo;
  private readonly IGenericRepository<ProductBrand> _productBrandRepo;

  private readonly IGenericRepository<ProductType> _productTypeRepo;

  private readonly IMapper _mapper;

  public ProductsController(IGenericRepository<Product> productsRepo,
                            IGenericRepository<ProductBrand> productBrandRepo,
                            IGenericRepository<ProductType> productTypeRepo,
                            IMapper mapper)
  {
    _mapper = mapper;
    _productsRepo = productsRepo;
    _productBrandRepo = productBrandRepo;
    _productTypeRepo = productTypeRepo;
  }
  [HttpGet]
  public async Task<ActionResult<Pagination<ProductToReturnDto>>> Getproducts(
    [FromQuery] ProductParams productParams)
  {
    var spec = new ProductsWithBrandsAndTypesSpecifications(productParams);

    var countSpec = new ProductWithFiltersForCountSpecification(productParams);
    var totalItems = await _productsRepo.CountAsync(countSpec);
    var products = await _productsRepo.ListAsync(spec);

    var data =
      _mapper.Map<IReadOnlyList<Product>, IReadOnlyList<ProductToReturnDto>>(
        products);

    return Ok(new Pagination<ProductToReturnDto>(
      productParams.PageIndex, productParams.PageSize, totalItems, data));
  }

  [HttpGet("{id}")]
  [ProducesResponseType(StatusCodes.Status200OK)]
  [ProducesResponseType(typeof(APIResponse), StatusCodes.Status404NotFound)]
  public async Task<ActionResult<ProductToReturnDto>> Getproducts(int id)
  {
    var spec = new ProductsWithBrandsAndTypesSpecifications(id);

    var product = await _productsRepo.GetEntityWithSpec(spec);

    if (product == null)
      return NotFound(new APIResponse(404));

    return _mapper.Map<Product, ProductToReturnDto>(product);
  }
  [HttpGet("brands")]
  public async Task<ActionResult<IReadOnlyList<ProductBrand>>>
  GetProductBrands()
  {

    return Ok(await _productBrandRepo.ListAllAsync());
  }
  [HttpGet("producttypes")]
  public async Task<ActionResult<IReadOnlyList<ProductType>>> GetProductTypes()
  {
    var productstype = await _productTypeRepo.ListAllAsync();
    return Ok(productstype);
  }
}

}
