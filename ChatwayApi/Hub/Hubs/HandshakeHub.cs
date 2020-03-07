using Services.Services;
using System;
using System.Collections.Generic;
using System.Text;

namespace Hub.Hubs {
    class HandshakeHub : Microsoft.AspNetCore.SignalR.Hub {

        public readonly DispositivoService _dispositivoService;
    }
}
