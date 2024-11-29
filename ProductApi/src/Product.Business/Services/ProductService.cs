﻿using AutoMapper;
using Product.Business.Models;
using Product.Business.Services.Interfaces;
using Product.DataAccess.Repositories.Interfaces;

namespace Product.Business.Services;

public class ProductService : IProductService
{
    private readonly IProductRepository _productRepository;
    private readonly IMapper _mapper;

    public ProductService(IProductRepository productRepository, IMapper mapper)
    {
        _productRepository = productRepository;
        _mapper = mapper;
    }

    public async Task<IEnumerable<ProductPayload>> GetProducts()
    {
        var result = await _productRepository.GetAllProductsAsync();
        var response = _mapper.Map<IEnumerable<ProductPayload>>(result);
        return response;
    }

    public async Task<ProductPayload> GetProduct(int id)
    {
        var product = await _productRepository.GetProductByIdAsync(id);
        if (product == null) return null;

        return _mapper.Map<ProductPayload>(product);
    }

    public async Task<ProductPayload> PostProduct(ProductPayload product)
    {
        var productDetails = _mapper.Map<DataAccess.Entities.Product>(product);
        var response = await _productRepository.AddProductAsync(productDetails);

        return _mapper.Map<ProductPayload>(response);
    }

    public async Task<ProductPayload> PutProduct(int id, ProductPayload product)
    {
        var productDetails = _mapper.Map<DataAccess.Entities.Product>(product);
        await _productRepository.UpdateProductAsync(id, productDetails);
        return product;
    }

    public async Task DeleteProduct(int id)
    {
        await _productRepository.DeleteProductAsync(id);
    }
}