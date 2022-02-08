using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CarRentalManagement.Client.Static
{
    public static class Endpoints
    {
        private static readonly string Prefix = "api";

        public static readonly string AchievementsEndpoint = $"{Prefix}/achievements";
        public static readonly string CopiesEndpoint = $"{Prefix}/copies";
        public static readonly string GamesEndpoint = $"{Prefix}/games";
        public static readonly string NewsEndpoint = $"{Prefix}/news";
        public static readonly string OrdersEndpoint = $"{Prefix}/orders";
        public static readonly string PaymentsEndpoint = $"{Prefix}/payments";
        public static readonly string StatsEndpoint = $"{Prefix}/stats";
        public static readonly string UsersEndpoint = $"{Prefix}/users";
    }
}
