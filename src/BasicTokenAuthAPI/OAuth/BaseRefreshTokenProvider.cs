using Microsoft.Owin.Security.Infrastructure;
using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;

namespace BasicTokenAuthAPI.OAuth
{
    public class BaseRefreshTokenProvider : IAuthenticationTokenProvider
    {
        private readonly ConcurrentDictionary<string, string> _refreshToken =
            new ConcurrentDictionary<string, string>(StringComparer.Ordinal);

        public void Create(AuthenticationTokenCreateContext context)
        {
            context.SetToken(Guid.NewGuid().ToString("n") + Guid.NewGuid().ToString("n"));
            _refreshToken[context.Token] = context.SerializeTicket();
        }

        public async Task CreateAsync(AuthenticationTokenCreateContext context)
        {
            await Task.Run(() =>
            {
                Create(context);
            });
        }

        public void Receive(AuthenticationTokenReceiveContext context)
        {
            string value;
            if (_refreshToken.TryRemove(context.Token, out value))
            {
                context.DeserializeTicket(value);
            }
        }

        public async Task ReceiveAsync(AuthenticationTokenReceiveContext context)
        {
            await Task.Run(() =>
            {
                Receive(context);
            });
        }
    }
}