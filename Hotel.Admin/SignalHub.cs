using System;
using System.Web;
using Microsoft.AspNet.SignalR;

namespace Hotel.Admin
{
    
    public class SignalHub : Hub
    {
        public void Hello()
        {
            Clients.All.hello();
        }
        public void Send(String name, String message)
        {
            Clients.All.addNewMessageToPage(name, message);
        }
    }
}