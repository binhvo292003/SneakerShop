using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Options;
using CloudinaryDotNet.Actions;
using CloudinaryDotNet;
using SneakerShop.Application.Settings;
using SneakerShop.Domain.Entities;
using SneakerShop.Domain.Repositories;


namespace SneakerShop.Application.Common.Services
{
    public class ProductImageService
    {
        private readonly Cloudinary _cloudinary;
        private readonly IProductImageRepository _productImageRepository;

        public ProductImageService(IOptions<CloudinarySettings> config, IProductImageRepository productImageRepository)
        {
            _productImageRepository = productImageRepository;

            var acc = new Account
            (
                config.Value.CloudName,
                config.Value.ApiKey,
                config.Value.ApiSecret
            );

            _cloudinary = new Cloudinary(acc);
        }

        public async Task<ProductImage> CreateProductImage(IFormFile file, long? productId)
        {
            if (!(file.Length > 0))
            {
                return null;
            }

            using var stream = file.OpenReadStream();

            var uploadParams = new ImageUploadParams
            {
                File = new FileDescription(file.FileName, stream),
                Transformation = new Transformation().Height(500).Width(500).Crop("fill").Gravity("face")
            };

            ImageUploadResult uploadResult = await _cloudinary.UploadAsync(uploadParams);
            if (uploadResult.Error != null)
            {
                throw new Exception($"Cloudinary upload failed: {uploadResult.Error.Message}");
            }

            var productImage = new ProductImage
            {
                PublicId = uploadResult.PublicId,
                ImageUrl = uploadResult.SecureUrl.ToString(),
                ProductId = productId,
            };

            return await _productImageRepository.CreateProductImage(productImage);
        }

        public async Task<DeletionResult> DeleteImage(long Id)
        {
            ProductImage productImage = await _productImageRepository.GetProductImageById(Id);
            if (productImage == null)
            {
                return null;
            }

            var deleteParams = new DeletionParams(productImage.PublicId);
            var result = await _cloudinary.DestroyAsync(deleteParams);
            await _productImageRepository.DeleteProductImage(Id);

            return result;
        }

        public async Task<ProductImage> GetProductImageByPublicId(string id)
        {
            if (string.IsNullOrEmpty(id))
            {
                return null;
            }

            var productImage = await _productImageRepository.FindProductImageByUrl(id);
            return productImage;
        }
    }
}