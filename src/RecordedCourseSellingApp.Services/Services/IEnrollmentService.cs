﻿using RecordedCourseSellingApp.Services.BusinessObjects;

namespace RecordedCourseSellingApp.Services.Services;

public interface IEnrollmentService
{
    Task AddCartItemAsync(CartItem item);

    Task RemoveCartItemAsync(Guid id);

    Task<IEnumerable<CartItem>> GetCartItemsAsync(string username);

    Task CreateCheckoutAsync(string username);

    Task<(int CartItems, int TotalAmount)> GetCheckoutDataAsync(string username);
}
