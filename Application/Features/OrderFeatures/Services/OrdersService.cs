using Application.Interfaces;
using Domain.Dtos;
using Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Application.Features.OrderFeatures.Services
{
    public class OrdersService : IOrdersService
    {
        public string ConvertToCsv(IEnumerable<Order> orders)
        {
            StringBuilder stringBuilder = new();
            stringBuilder.AppendLine("Id,OperatorId,CustomerId,Date,Total");
            foreach (Order order in orders)
            {
                stringBuilder.AppendLine($"{order.Id},{order.OperatorId},{order.UserId},{order.Timestamp},{order.Total}");
            }
            return stringBuilder.ToString();
        }

        public IQueryable<Order> Filter(IQueryable<Order> orders, OrderFilterDto filter)
        {
            if (IsIdValid(filter.OperatorId))
            {
                orders = orders.Where(order => order.OperatorId == filter.OperatorId);
            }

            if (IsIdValid(filter.UserId))
            {
                orders = orders.Where(order => order.UserId == filter.UserId);
            }

            orders = FilterByDate(orders, filter.StartDate, filter.EndDate);
            orders = FilterByPrice(orders, filter.MinPrice, filter.MaxPrice);
            orders = ApplySorting(orders, filter.FieldToSortBy, filter.SortDirection);
            return orders;
        }

        private static bool IsIdValid(Guid? id)
        {
            return id != null && id != Guid.Empty;
        }

        private static IQueryable<Order> FilterByDate(IQueryable<Order> orders, DateTime? startDate, DateTime? endDate)
        {
            if (startDate != null)
            {
                orders = orders.Where(order => order.Timestamp >= startDate);
            }

            if (endDate != null)
            {
                orders = orders.Where(order => order.Timestamp <= endDate);
            }

            return orders;
        }

        private static IQueryable<Order> FilterByPrice(IQueryable<Order> orders, double minPrice, double maxPrice)
        {
            if (minPrice > 0)
            {
                orders = orders.Where(order => order.Total >= minPrice);
            }

            if (maxPrice > 0)
            {
                orders = orders.Where(order => order.Total <= maxPrice);
            }

            return orders;
        }

        private static IQueryable<Order> ApplySorting(IQueryable<Order> orders, string field, string direction)
        {
            if (!string.IsNullOrEmpty(field))
            {
                if (field == "Timestamp")
                {
                    orders = direction == "asc" ? orders.OrderBy(order => order.Timestamp) : orders.OrderByDescending(order => order.Timestamp);
                }
                else if (field == "Total")
                {
                    orders = direction == "asc" ? orders.OrderBy(order => order.Total) : orders.OrderByDescending(order => order.Total);
                }
            }
            return orders;
        }
    }
}
