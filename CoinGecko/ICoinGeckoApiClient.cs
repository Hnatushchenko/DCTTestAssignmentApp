﻿using CoinGecko.Responces;

namespace CoinGecko
{
    public interface ICoinGeckoApiClient
    {
        Task<IReadOnlyCollection<CoinsListItem>?> GetCoinsList();
    }
}