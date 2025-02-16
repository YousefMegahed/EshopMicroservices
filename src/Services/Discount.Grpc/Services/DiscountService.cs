using Discount.Grpc.Data;
using Discount.Grpc.Models;
using Grpc.Core;
using Mapster;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

namespace Discount.Grpc.Services
{
    public class DiscountService(DiscountContext _dbContext, ILogger<DiscountService> _logger) : DiscountProtoService.DiscountProtoServiceBase
    {
        public override async Task<CouponModel> CreateDiscount(CreateDiscountRequest request, ServerCallContext context)
        {

            var coupon = request.Coupon.Adapt<Coupon>() ?? throw new RpcException(new Status(StatusCode.InvalidArgument, "Invalid request object."));
            await _dbContext.Coupons.AddAsync(coupon);

            await _dbContext.SaveChangesAsync();

            _logger.LogInformation("Discount is successfully created. ProductName : {ProductName}", coupon.ProductName);

            var couponModel = coupon.Adapt<CouponModel>();
            return couponModel;
        }

        public override async Task<CouponModel> GetDiscount(GetDiscountRequest request, ServerCallContext context)
        {
            var coupon = await _dbContext
           .Coupons
           .FirstOrDefaultAsync(x => x.ProductName == request.ProductName);

            if (coupon is null)
                coupon = new Coupon { ProductName = "No Discount", Amount = 0, Description = "No Discount Desc" };

            _logger.LogInformation("Discount is retrieved for ProductName : {productName}, Amount : {amount}", coupon.ProductName, coupon.Amount);

            var couponModel = coupon.Adapt<CouponModel>();
            return couponModel;
        }

        public override async Task<DeleteDiscountResponse> DeleteDiscount(DeleteDiscountRequest request, ServerCallContext context)
        {
            var coupon = await _dbContext
            .Coupons
            .FirstOrDefaultAsync(x => x.ProductName == request.ProductName);

            if (coupon is null)
                throw new RpcException(new Status(StatusCode.NotFound, $"Discount with ProductName={request.ProductName} is not found."));

            _dbContext.Coupons.Remove(coupon);
            await _dbContext.SaveChangesAsync();

            _logger.LogInformation("Discount is successfully deleted. ProductName : {ProductName}", request.ProductName);

            return new DeleteDiscountResponse { Success = true };
        }


        public override async Task<CouponModel> UpdateDiscount(UpdateDiscountRequest request, ServerCallContext context)
        {

            var coupon = await _dbContext
            .Coupons
            .FirstOrDefaultAsync(x => x.Id == request.Coupon.Id);

            

            if (coupon is null)
                throw new RpcException(new Status(StatusCode.InvalidArgument, "Invalid request object."));

            coupon.Amount = request.Coupon.Amount;
            coupon.Description = request.Coupon.Description;
            coupon.ProductName = request.Coupon.ProductName;

            _dbContext.Coupons.Update(coupon);
            await _dbContext.SaveChangesAsync();

            _logger.LogInformation("Discount is successfully updated. ProductName : {ProductName}", coupon.ProductName);

            var couponModel = coupon.Adapt<CouponModel>();
            return couponModel;
        }

    }
}
