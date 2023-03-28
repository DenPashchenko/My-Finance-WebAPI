﻿using AutoMapper;
using MyFinance.Application.Common.Mappings;
using MyFinance.Application.Transactions.Commands.CreateTransaction;
using MyFinance.Domain.Enums;
using System.ComponentModel.DataAnnotations;

namespace MyFinanceWebApi.ModelDtos.FinancialTransactionDto
{
    public class CreateTransactionDto : IMapWith<CreateTransactionCommand>
    {
        public TransactionType TransactionType { get; set; }

        public int CategoryId { get; set; }

        [StringLength(50, MinimumLength = 2, ErrorMessage = "Name length can't be more then 50 and less then 2.")]
        public string Name { get; set; } = null!;

        [StringLength(250, MinimumLength = 2, ErrorMessage = "Description length can't be more then 250 and less then 2.")]
        public string? Description { get; set; }

        [Range(0.01, double.MaxValue, ErrorMessage = "The value must be equal or greater than 0.01")]
        public decimal Sum { get; set; }

        public void Mapping(Profile profile)
        {
            profile.CreateMap<CreateTransactionDto, CreateTransactionCommand>();
        }
    }
}
