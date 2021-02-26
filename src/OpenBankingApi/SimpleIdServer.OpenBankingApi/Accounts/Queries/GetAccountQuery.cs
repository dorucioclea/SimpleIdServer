﻿using MediatR;
using SimpleIdServer.OpenBankingApi.Accounts.Results;

namespace SimpleIdServer.OpenBankingApi.Accounts.Queries
{
    public class GetAccountQuery : IRequest<GetAccountsResult>
    {
        public GetAccountQuery(string accountId, string issuer)
        {
            AccountId = accountId;
            Issuer = issuer;
        }

        public string AccountId { get; set; }
        public string Issuer { get; set; }
    }
}