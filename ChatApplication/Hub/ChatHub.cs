using Microsoft.AspNetCore.SignalR;
using System.Collections.Generic;
using System;

namespace ChatApplication.Hub
{
    public class ChatHub : Microsoft.AspNetCore.SignalR.Hub
    {
        // Dictionary to store user connections and their associated room information
        private readonly IDictionary<string, UserRoomConnection> _connection;

        //Dictionary to manage user-room connections
        public ChatHub(IDictionary<string, UserRoomConnection> connection)
        {
            _connection = connection;
        }

        //join room
        public async Task JoinRoom(UserRoomConnection userConnection)
        {
            // Add the user to the specified chat room group
            await Groups.AddToGroupAsync(Context.ConnectionId, groupName: userConnection.Room!);

            //save user connection to dictionary
            //Context.ConnectionId used as a key, ensuring each connection is unique
            _connection[Context.ConnectionId] = userConnection;

            // Notify all users in the room about the new user joining
            await Clients.Group(userConnection.Room!)
                .SendAsync("ReceiveMessage", "Lets Progream Bot", $"{userConnection.User} has Joined the Group");
        }
    }
}
