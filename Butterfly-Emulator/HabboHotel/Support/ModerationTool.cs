using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using Butterfly.Collections;
using Butterfly.Core;
using Butterfly.HabboHotel.ChatMessageStorage;
using Butterfly.HabboHotel.GameClients;
using Butterfly.HabboHotel.Rooms;
using Butterfly.HabboHotel.Rooms.RoomIvokedItems;
using Butterfly.Messages;
using Database_Manager.Database.Session_Details.Interfaces;

namespace Butterfly.HabboHotel.Support
{
    public class ModerationTool
    {
        #region General

        internal List<SupportTicket> Tickets;

        internal List<string> UserMessagePresets;
        internal List<string> RoomMessagePresets;

        internal ModerationTool()
        {
            Tickets = new List<SupportTicket>();
            UserMessagePresets = new List<string>();
            RoomMessagePresets = new List<string>();
        }

        internal ServerMessage SerializeTool()
        {
            ServerMessage Response = new ServerMessage(531);
            Response.AppendInt32(-1);

            Response.AppendInt32(UserMessagePresets.Count);
            foreach (String Preset in UserMessagePresets)
            {
                Response.AppendStringWithBreak(Preset);
            }
            Response.AppendUInt(6); //Mod Actions Count

            Response.AppendStringWithBreak("Room Problems"); // modaction Cata
            Response.AppendUInt(8); // ModAction Count
            Response.AppendStringWithBreak("Door Problem"); // mod action Cata
            Response.AppendStringWithBreak("Stop blocking the doors Please"); // Msg
            Response.AppendStringWithBreak("Ban-Problem"); // Mod Action Cata
            Response.AppendStringWithBreak("This your last warning or you received a ban"); // Msg
            Response.AppendStringWithBreak("Help Support");// Mod Action Cata
            Response.AppendStringWithBreak("Please Contact The Player Support For this problem"); // Msg
            Response.AppendStringWithBreak("Bobba Filter"); // Mod Cata
            Response.AppendStringWithBreak("Please stop Playing with the Bobba Filter"); // Msg
            Response.AppendStringWithBreak("Kick user"); // Mod Cata
            Response.AppendStringWithBreak("Please stop Kicking people without a Reason"); // Msg
            Response.AppendStringWithBreak("Ban Room"); // Mod Cata
            Response.AppendStringWithBreak("Please stop banning people without a good reason"); // Msg
            Response.AppendStringWithBreak("RoomName"); // Mod Cata
            Response.AppendStringWithBreak("Please Change your RoomName otherwish we will do it."); // Msg
            Response.AppendStringWithBreak("PH box"); // Mod Cata
            Response.AppendStringWithBreak("Please Contact A administrator about your problem"); // Msg

            Response.AppendStringWithBreak("Player Support");// modaction Cata
            Response.AppendUInt(8); // ModAction Count
            Response.AppendStringWithBreak("Player Bug"); // mod action Cata
            Response.AppendStringWithBreak("Thank you for supporting us and reporting this bug"); // Msg
            Response.AppendStringWithBreak("Login Problem"); // Mod Action Cata
            Response.AppendStringWithBreak("We contact to the player support and will work on your problem"); // Msg
            Response.AppendStringWithBreak("Help Support");// Mod Action Cata
            Response.AppendStringWithBreak("Please Contact The Player Support For this problem"); // Msg
            Response.AppendStringWithBreak("Call for help Filter"); // Mod Cata
            Response.AppendStringWithBreak("Please stop Playing with the CFH Filter"); // Msg
            Response.AppendStringWithBreak("Privacy"); // Mod Cata
            Response.AppendStringWithBreak("You should not give your privacy stuff away."); // Msg
            Response.AppendStringWithBreak("Warning Swf."); // Mod Cata
            Response.AppendStringWithBreak("Please Contact a administrator/moderator."); // Msg
            Response.AppendStringWithBreak("Cache"); // Mod Cata
            Response.AppendStringWithBreak("if you got problems with memmory Please report it"); // Msg
            Response.AppendStringWithBreak("Temp Problem"); // Mod Cata
            Response.AppendStringWithBreak("Delete your temp!"); // Msg

            Response.AppendStringWithBreak("Users Problems");// modaction Cata
            Response.AppendUInt(8); // ModAction Count
            Response.AppendStringWithBreak("Scamming"); // mod action Cata
            Response.AppendStringWithBreak("We will Check the problem for you now can you give us more infomation about what's happens"); // Msg
            Response.AppendStringWithBreak("Trading Scam"); // Mod Action Cata
            Response.AppendStringWithBreak("What happens and how happens please explain us"); // Msg
            Response.AppendStringWithBreak("Disconnection");// Mod Action Cata
            Response.AppendStringWithBreak("Please Contact The Player Support For this problem"); // Msg
            Response.AppendStringWithBreak("Room blocking"); // Mod Cata
            Response.AppendStringWithBreak("Can you say The usersname and explain us what happens"); // Msg
            Response.AppendStringWithBreak("Freezing"); // Mod Cata
            Response.AppendStringWithBreak("Can explain us what happens?"); // Msg
            Response.AppendStringWithBreak("Error page"); // Mod Cata
            Response.AppendStringWithBreak("What was the code from your error Example 404"); // Msg
            Response.AppendStringWithBreak("Can't login"); // Mod Cata
            Response.AppendStringWithBreak("Can you say The usersname and explain us what happens"); // Msg
            Response.AppendStringWithBreak("Updates"); // Mod Cata
            Response.AppendStringWithBreak("We always do our best to update everything"); // Msg

            Response.AppendStringWithBreak("Debug Problems");// modaction Cata
            Response.AppendUInt(8); // ModAction Count
            Response.AppendStringWithBreak("Lag"); // mod action Cata
            Response.AppendStringWithBreak("We will Check the problem for you now can you give us more infomation about what's happens"); // Msg
            Response.AppendStringWithBreak("Disconnection"); // Mod Action Cata
            Response.AppendStringWithBreak("What happens and how happens please explain us"); // Msg
            Response.AppendStringWithBreak("SSO problem");// Mod Action Cata
            Response.AppendStringWithBreak("Please Contact The Player Support For this problem"); // Msg
            Response.AppendStringWithBreak("Char Filter"); // Mod Cata
            Response.AppendStringWithBreak("Can you say The usersname and explain us what happens"); // Msg
            Response.AppendStringWithBreak("System check"); // Mod Cata
            Response.AppendStringWithBreak("We already checking every debug stuff"); // Msg
            Response.AppendStringWithBreak("Error from WireEncoding"); // Mod Cata
            Response.AppendStringWithBreak("Can you say explain us what happens"); // Msg
            Response.AppendStringWithBreak("Error from BASE64"); // Mod Cata
            Response.AppendStringWithBreak("Can you explain us what happens"); // Msg
            Response.AppendStringWithBreak("Error from Flash player"); // Mod Cata
            Response.AppendStringWithBreak("Can you explain us what happens"); // Msg

            Response.AppendStringWithBreak("System Problems");// modaction Cata
            Response.AppendUInt(8); // ModAction Count
            Response.AppendStringWithBreak("Version"); // mod action Cata
            Response.AppendStringWithBreak("Ask a Administrator For more Information"); // Msg
            Response.AppendStringWithBreak("Swf Version?"); // Mod Action Cata
            Response.AppendStringWithBreak("Currenly We use the same version like Habbo.com"); // Msg
            Response.AppendStringWithBreak("Freeze");// Mod Action Cata
            Response.AppendStringWithBreak("Please Contact The Player Support For this problem"); // Msg
            Response.AppendStringWithBreak("Name Filter"); // Mod Cata
            Response.AppendStringWithBreak("Can you say The usersname and explain us what happens"); // Msg
            Response.AppendStringWithBreak("Nickname Filter"); // Mod Cata
            Response.AppendStringWithBreak("Can you say The usersname and explain us what happens"); // Msg
            Response.AppendStringWithBreak("System Filter"); // Mod Cata
            Response.AppendStringWithBreak("Can you say The usersname and explain us what happens"); // Msg
            Response.AppendStringWithBreak("Cursing Filter"); // Mod Cata
            Response.AppendStringWithBreak("Can you say The usersname and explain us what happens"); // Msg
            Response.AppendStringWithBreak("Update Filter"); // Mod Cata
            Response.AppendStringWithBreak("We will update your words in the filter Thanks for report."); // Msg

            Response.AppendStringWithBreak("Swf Problems");// modaction Cata
            Response.AppendUInt(8); // ModAction Count
            Response.AppendStringWithBreak("Version"); // mod action Cata
            Response.AppendStringWithBreak("Ask a Administrator For more Information"); // Msg
            Response.AppendStringWithBreak("Swf Version?"); // Mod Action Cata
            Response.AppendStringWithBreak("Currenly We use the same version like Habbo.com"); // Msg
            Response.AppendStringWithBreak("Freeze");// Mod Action Cata
            Response.AppendStringWithBreak("Please Contact The Player Support For this problem"); // Msg
            Response.AppendStringWithBreak("Name Filter"); // Mod Cata
            Response.AppendStringWithBreak("Can you say The usersname and explain us what happens"); // Msg
            Response.AppendStringWithBreak("Crash on loading"); // Mod Cata
            Response.AppendStringWithBreak("Can you explain us what happens"); // Msg
            Response.AppendStringWithBreak("Crash on login"); // Mod Cata
            Response.AppendStringWithBreak("Can you say the usersname and explain us what happens"); // Msg
            Response.AppendStringWithBreak("Crash in room"); // Mod Cata
            Response.AppendStringWithBreak("Can you say the usersname and explain us what happens"); // Msg
            Response.AppendStringWithBreak("Website error"); // Mod Cata
            Response.AppendStringWithBreak("Can you say the usersname and explain us what happens"); // Msg

            Response.AppendStringWithBreak("");
            Response.AppendStringWithBreak("");
            Response.AppendStringWithBreak("");
            Response.AppendStringWithBreak("");
            Response.AppendStringWithBreak("");
            Response.AppendStringWithBreak("");
            Response.AppendStringWithBreak("");

            Response.AppendInt32(RoomMessagePresets.Count);
            foreach (String Preset in RoomMessagePresets)
            {
                Response.AppendStringWithBreak(Preset);
            }
            Response.AppendStringWithBreak("");
            return Response;
        }

        #endregion

        #region Message Presets

        internal void LoadMessagePresets(IQueryAdapter dbClient)
        {
            UserMessagePresets.Clear();
            RoomMessagePresets.Clear();

            dbClient.setQuery("SELECT type,message FROM moderation_presets WHERE enabled = 2");
            DataTable Data = dbClient.getTable();

            if (Data == null)
                return;

            foreach (DataRow Row in Data.Rows)
            {
                String Message = (String)Row["message"];

                switch (Row["type"].ToString().ToLower())
                {
                    case "message":

                        UserMessagePresets.Add(Message);
                        break;

                    case "roommessage":

                        RoomMessagePresets.Add(Message);
                        break;
                }
            }
        }

        #endregion

        #region Support Tickets

        internal void LoadPendingTickets(IQueryAdapter dbClient)
        {
            //dbClient.runFastQuery("TRUNCATE TABLE moderation_tickets");

            dbClient.setQuery("SELECT moderation_tickets.*, p1.username AS sender_username, p2.username AS reported_username, p3.username AS moderator_username FROM moderation_tickets LEFT OUTER JOIN users AS p1 ON moderation_tickets.sender_id = p1.id LEFT OUTER JOIN users AS p2 ON moderation_tickets.reported_id = p2.id LEFT OUTER JOIN users AS p3 ON moderation_tickets.moderator_id = p3.id WHERE moderation_tickets.status != 'resolved'");
            DataTable Data = dbClient.getTable();

            if (Data == null)
            {
                return;
            }

            foreach (DataRow Row in Data.Rows)
            {
                SupportTicket Ticket = new SupportTicket(Convert.ToUInt32(Row["id"]), (int)Row["score"], (int)Row["type"], Convert.ToUInt32(Row["sender_id"]), Convert.ToUInt32(Row["reported_id"]), (String)Row["message"], Convert.ToUInt32(Row["room_id"]), (String)Row["room_name"], (Double)Row["timestamp"], Row["sender_username"], Row["reported_username"], Row["moderator_username"]);

                if (Row["status"].ToString().ToLower() == "picked")
                {
                    Ticket.Pick(Convert.ToUInt32(Row["moderator_id"]), false);
                }

                Tickets.Add(Ticket);
            }
        }

        internal void SendNewTicket(GameClient Session, int Category, uint ReportedUser, String Message)
        {
            if (Session.GetHabbo().CurrentRoomId <= 0)
            {
                return;
            }

            RoomData Data = ButterflyEnvironment.GetGame().GetRoomManager().GenerateNullableRoomData(Session.GetHabbo().CurrentRoomId);

            uint TicketId = 0;

            using (IQueryAdapter dbClient = ButterflyEnvironment.GetDatabaseManager().getQueryreactor())
            {
                if (dbClient.dbType == Database_Manager.Database.DatabaseType.MSSQL)
                    dbClient.setQuery("INSERT INTO moderation_tickets (score,type,status,sender_id,reported_id,moderator_id,message,room_id,room_name,timestamp) OUTPUT INSERTED.* VALUES (1,'" + Category + "','open','" + Session.GetHabbo().Id + "','" + ReportedUser + "','0',@message,'" + Data.Id + "',@name,'" + ButterflyEnvironment.GetUnixTimestamp() + "')");
                else
                    dbClient.setQuery("INSERT INTO moderation_tickets (score,type,status,sender_id,reported_id,moderator_id,message,room_id,room_name,timestamp) VALUES (1,'" + Category + "','open','" + Session.GetHabbo().Id + "','" + ReportedUser + "','0',@message,'" + Data.Id + "',@name,'" + ButterflyEnvironment.GetUnixTimestamp() + "')");
                dbClient.addParameter("message", Message);
                dbClient.addParameter("name", Data.Name);
                TicketId = (uint)dbClient.insertQuery();

                dbClient.runFastQuery("UPDATE user_info SET cfhs = cfhs + 1 WHERE user_id = " + Session.GetHabbo().Id + "");

                //dbClient.setQuery("SELECT id FROM moderation_tickets WHERE sender_id = " + Session.GetHabbo().Id + " ORDER BY id DESC LIMIT 1");
                //TicketId = (uint)dbClient.getRow()[0];
            }

            SupportTicket Ticket = new SupportTicket(TicketId, 1, Category, Session.GetHabbo().Id, ReportedUser, Message, Data.Id, Data.Name, ButterflyEnvironment.GetUnixTimestamp());

            Tickets.Add(Ticket);

            SendTicketToModerators(Ticket);
        }

        internal void SerializeOpenTickets(ref QueuedServerMessage serverMessages, uint userID)
        {
            foreach (SupportTicket ticket in Tickets)
            {
                if (ticket.Status == TicketStatus.OPEN || (ticket.Status == TicketStatus.PICKED && ticket.ModeratorId == userID))
                    serverMessages.appendResponse(ticket.Serialize());
            }
        }

        internal SupportTicket GetTicket(uint TicketId)
        {
            foreach (SupportTicket Ticket in Tickets)
            {
                if (Ticket.TicketId == TicketId)
                {
                    return Ticket;
                }
            }
            return null;
        }

        internal void PickTicket(GameClient Session, uint TicketId)
        {
            SupportTicket Ticket = GetTicket(TicketId);

            if (Ticket == null || Ticket.Status != TicketStatus.OPEN)
            {
                return;
            }

            Ticket.Pick(Session.GetHabbo().Id, true);
            SendTicketToModerators(Ticket);
        }

        internal void ReleaseTicket(GameClient Session, uint TicketId)
        {
            SupportTicket Ticket = GetTicket(TicketId);

            if (Ticket == null || Ticket.Status != TicketStatus.PICKED || Ticket.ModeratorId != Session.GetHabbo().Id)
            {
                return;
            }

            Ticket.Release(true);
            SendTicketToModerators(Ticket);
        }

        internal void CloseTicket(GameClient Session, uint TicketId, int Result)
        {
            SupportTicket Ticket = GetTicket(TicketId);

            if (Ticket == null || Ticket.Status != TicketStatus.PICKED || Ticket.ModeratorId != Session.GetHabbo().Id)
            {
                return;
            }

            GameClient Client = ButterflyEnvironment.GetGame().GetClientManager().GetClientByUserID(Ticket.SenderId);

            TicketStatus NewStatus;
            int ResultCode;

            switch (Result)
            {
                case 1:

                    ResultCode = 1;
                    NewStatus = TicketStatus.INVALID;
                    break;

                case 2:

                    ResultCode = 2;
                    NewStatus = TicketStatus.ABUSIVE;

                    using (IQueryAdapter dbClient = ButterflyEnvironment.GetDatabaseManager().getQueryreactor())
                    {
                        dbClient.runFastQuery("UPDATE user_info SET cfhs_abusive = cfhs_abusive + 1 WHERE user_id = " + Ticket.SenderId + "");
                    }

                    break;

                case 3:
                default:

                    ResultCode = 0;
                    NewStatus = TicketStatus.RESOLVED;
                    break;
            }

            if (Client != null)
            {
                Client.GetMessageHandler().GetResponse().Init(540);
                Client.GetMessageHandler().GetResponse().AppendInt32(ResultCode);
                Client.GetMessageHandler().SendResponse();
            }

            Ticket.Close(NewStatus, true);
            SendTicketToModerators(Ticket);
        }

        internal Boolean UsersHasPendingTicket(UInt32 Id)
        {
            foreach (SupportTicket Ticket in Tickets)
            {
                if (Ticket.SenderId == Id && Ticket.Status == TicketStatus.OPEN)
                {
                    return true;
                }
            }
            return false;
        }

        internal void DeletePendingTicketForUser(UInt32 Id)
        {
            foreach (SupportTicket Ticket in Tickets)
            {
                if (Ticket.SenderId == Id)
                {
                    Ticket.Delete(true);
                    SendTicketToModerators(Ticket);
                    return;
                }
            }
        }

        internal static void SendTicketToModerators(SupportTicket Ticket)
        {
            ButterflyEnvironment.GetGame().GetClientManager().QueueBroadcaseMessage(Ticket.Serialize(), "fuse_mod");
        }


        internal void LogStaffEntry(string modName, string target, string type, string description)
        {
            using (IQueryAdapter dbClient = ButterflyEnvironment.GetDatabaseManager().getQueryreactor())
            {
                dbClient.setQuery("INSERT INTO staff_logs (staffuser,target,action_type,description) VALUES (@username,@target,@type,@desc)");
                dbClient.addParameter("username", modName);
                dbClient.addParameter("target", target);
                dbClient.addParameter("type", type);
                dbClient.addParameter("desc", description);
                dbClient.runQuery();
            }
        }

        #endregion

        #region Room Moderation


        internal static void PerformRoomAction(GameClient ModSession, uint RoomId, bool KickUsers, bool LockRoom, bool InappropriateRoom, string Message)
        {
            Room Room = ButterflyEnvironment.GetGame().GetRoomManager().GetRoom(RoomId);
            if (Room == null)
                return;

           
            if (LockRoom)
            {
                Room.State = 1;
                Room.RoomData.State = 1;

                using (IQueryAdapter dbClient = ButterflyEnvironment.GetDatabaseManager().getQueryreactor())
                {
                    dbClient.setQuery("UPDATE rooms SET state = @state WHERE id = @id");
                    dbClient.addParameter("state", 1);
                    dbClient.addParameter("id", Room.RoomId);
                    dbClient.runQuery();
                }

                Room.Name = "Inappropriate to Hotel Management";
                Room.RoomData.Name = Room.Name;

                // Mostra messaggio nella stanza se fornito
                if (!string.IsNullOrEmpty(Message))
                {
                    var usersList = Room.GetRoomUserManager().UserList.Values.ToList();
                    foreach (RoomUser User in usersList)
                    {
                        if (User != null && !User.IsBot && User.GetClient() != null)
                            User.GetClient().SendNotif(Message);
                    }
                }
            }

            // =====================
            // INAPPROPRIATE ROOM
            // =====================
            if (InappropriateRoom)
            {
                Room.Name = "Inappropriate to Hotel Management";
                Room.Description = "Inappropriate to Hotel Management";
                Room.Tags.Clear();

                Room.RoomData.Name = Room.Name;
                Room.RoomData.Description = Room.Description;
                Room.RoomData.Tags.Clear();

                using (IQueryAdapter dbClient = ButterflyEnvironment.GetDatabaseManager().getQueryreactor())
                {
                    dbClient.setQuery("UPDATE rooms SET caption = @caption, description = @desc, tags = '' WHERE id = @id");
                    dbClient.addParameter("caption", Room.Name);
                    dbClient.addParameter("desc", Room.Description);
                    dbClient.addParameter("id", Room.RoomId);
                    dbClient.runQuery();
                }

                // Mostra messaggio nella stanza se fornito
                if (!string.IsNullOrEmpty(Message))
                {
                    var usersList = Room.GetRoomUserManager().UserList.Values.ToList();
                    foreach (RoomUser User in usersList)
                    {
                        if (User != null && !User.IsBot && User.GetClient() != null)
                            User.GetClient().SendNotif(Message);
                    }
                }
            }

            // =====================
            // KICK USERS
            // =====================
            if (KickUsers)
            {
                var usersList = Room.GetRoomUserManager().UserList.Values.ToList();
                foreach (RoomUser User in usersList)
                {
                    if (User == null || User.IsBot || User.GetClient() == null)
                        continue;

                    if (User.GetClient().GetHabbo().Rank >= ModSession.GetHabbo().Rank)
                        continue;

                    Room.GetRoomUserManager().RemoveUserFromRoom(User.GetClient(), true, false);
                }
            }

            ButterflyEnvironment.GetGame().GetRoomManager().QueueActiveRoomUpdate(Room.RoomData);
        }

        // =====================
        // ROOM ALERT
        // =====================
        internal static void RoomAlert(uint RoomId, bool Caution, string Message)
        {
            Room Room = ButterflyEnvironment.GetGame().GetRoomManager().GetRoom(RoomId);
            if (Room == null || string.IsNullOrEmpty(Message))
                return;

            StringBuilder QueryBuilder = new StringBuilder();
            int j = 0;

            var usersList = Room.GetRoomUserManager().UserList.Values.ToList();
            foreach (RoomUser User in usersList)
            {
                if (User == null || User.IsBot || User.GetClient() == null)
                    continue;

                User.GetClient().SendNotif(Message, Caution);

                if (j > 0)
                    QueryBuilder.Append(" OR ");

                QueryBuilder.Append("user_id = '" + User.GetClient().GetHabbo().Id + "'");
                j++;
            }

            if (Caution && j > 0)
            {
                using (IQueryAdapter dbClient = ButterflyEnvironment.GetDatabaseManager().getQueryreactor())
                {
                    dbClient.setQuery("UPDATE user_info SET cautions = cautions + 1 WHERE " + QueryBuilder.ToString() + " LIMIT " + j);
                    dbClient.runQuery();
                }
            }
        }

        // =====================
        // SERIALIZE ROOM TOOL
        // =====================
        internal static ServerMessage SerializeRoomTool(RoomData Data)
        {
            Room Room = ButterflyEnvironment.GetGame().GetRoomManager().GetRoom(Data.Id);
            UInt32 OwnerId = 0;

            using (IQueryAdapter dbClient = ButterflyEnvironment.GetDatabaseManager().getQueryreactor())
            {
                try
                {
                    dbClient.setQuery("SELECT id FROM users WHERE username = @owner LIMIT 1");
                    dbClient.addParameter("owner", Data.Owner);
                    OwnerId = Convert.ToUInt32(dbClient.getRow()[0]);
                }
                catch (Exception) { }
            }

            ServerMessage Message = new ServerMessage(538);
            Message.AppendUInt(Data.Id);
            Message.AppendInt32(Data.UsersNow);

            // Controlla se il proprietario è nella stanza
            bool ownerInRoom = false;
            if (Room != null)
            {
                var usersList = Room.GetRoomUserManager().UserList.Values.ToList();
                foreach (RoomUser User in usersList)
                {
                    if (User != null && User.GetClient() != null && User.GetClient().GetHabbo().Username == Data.Owner)
                    {
                        ownerInRoom = true;
                        break;
                    }
                }
            }
            Message.AppendBoolean(ownerInRoom);

            Message.AppendUInt(OwnerId);
            Message.AppendStringWithBreak(Data.Owner);
            Message.AppendUInt(Data.Id);
            Message.AppendStringWithBreak(Data.Name);
            Message.AppendStringWithBreak(Data.Description);
            Message.AppendInt32(Data.TagCount);

            foreach (string Tag in Data.Tags)
                Message.AppendStringWithBreak(Tag);

            if (Room != null)
            {
                Message.AppendBoolean(Room.HasOngoingEvent);

                if (Room.Event != null)
                {
                    Message.AppendStringWithBreak(Room.Event.Name);
                    Message.AppendStringWithBreak(Room.Event.Description);
                    Message.AppendInt32(Room.Event.Tags.Count);

                    foreach (string Tag in Room.Event.Tags)
                        Message.AppendStringWithBreak(Tag);
                }
            }
            else
            {
                Message.AppendBoolean(false);
            }

            return Message;
        }

        

        #endregion

        #region User Moderation

        internal static void KickUser(GameClient ModSession, uint UserId, String Message, Boolean Soft)
        {
            GameClient Client = ButterflyEnvironment.GetGame().GetClientManager().GetClientByUserID(UserId);

            if (Client == null || Client.GetHabbo().CurrentRoomId < 1 || Client.GetHabbo().Id == ModSession.GetHabbo().Id)
            {
                return;
            }

            if (Client.GetHabbo().Rank >= ModSession.GetHabbo().Rank)
            {
                ModSession.SendNotif(LanguageLocale.GetValue("moderation.kick.missingrank"));
                return;
            }

            Room Room = ButterflyEnvironment.GetGame().GetRoomManager().GetRoom(Client.GetHabbo().CurrentRoomId);

            if (Room == null)
            {
                return;
            }

            Room.GetRoomUserManager().RemoveUserFromRoom(Client, true, false);
            Client.CurrentRoomUserID = -1;

            if (!Soft)
            {
                Client.SendNotif(Message);

                using (IQueryAdapter dbClient = ButterflyEnvironment.GetDatabaseManager().getQueryreactor())
                {
                    dbClient.runFastQuery("UPDATE user_info SET cautions = cautions + 1 WHERE user_id = " + UserId + "");
                }
            }
        }

        internal static void AlertUser(GameClient ModSession, uint UserId, String Message, Boolean Caution)
        {
            GameClient Client = ButterflyEnvironment.GetGame().GetClientManager().GetClientByUserID(UserId);

            if (Client == null)
            {
                ModSession.SendNotif("Errore: utente non trovato o non online.");
                return;
            }

            if (Client.GetHabbo().Id == ModSession.GetHabbo().Id)
            {
                ModSession.SendNotif("Non puoi inviarti messaggi a te stesso.");
                return;
            }

            if (Caution && Client.GetHabbo().Rank >= ModSession.GetHabbo().Rank)
            {
                ModSession.SendNotif(LanguageLocale.GetValue("moderation.caution.missingrank"));
                Caution = false; // mantiene la funzione ma evita blocco totale
            }

            // Invia messaggio
            Client.SendNotif(Message, Caution);

            // Log al mod
            ModSession.SendNotif($"Messaggio inviato a {Client.GetHabbo().Username}.");

            // Se è un avvertimento, aggiorna DB
            if (Caution)
            {
                using (IQueryAdapter dbClient = ButterflyEnvironment.GetDatabaseManager().getQueryreactor())
                {
                    dbClient.runFastQuery("UPDATE user_info SET cautions = cautions + 1 WHERE user_id = " + UserId + "");
                }
            }
        }

        internal static void BanUser(GameClient ModSession, uint UserId, int Length, String Message)
        {
            GameClient Client = ButterflyEnvironment.GetGame().GetClientManager().GetClientByUserID(UserId);

            if (Client == null || Client.GetHabbo().Id == ModSession.GetHabbo().Id)
            {
                return;
            }

            if (Client.GetHabbo().Rank >= ModSession.GetHabbo().Rank)
            {
                ModSession.SendNotif(LanguageLocale.GetValue("moderation.ban.missingrank"));
                return;
            }

            Double dLength = Length;

            ButterflyEnvironment.GetGame().GetBanManager().BanUser(Client, ModSession.GetHabbo().Username, dLength, Message, false);
        }

        #endregion

        #region User Info

        internal static ServerMessage SerializeUserInfo(uint UserId)
        {
            using (IQueryAdapter dbClient = ButterflyEnvironment.GetDatabaseManager().getQueryreactor())
            {
                dbClient.setQuery("SELECT id, username, online FROM users WHERE id = " + UserId + "");
                DataRow User = dbClient.getRow();

                dbClient.setQuery("SELECT reg_timestamp, login_timestamp, cfhs, cfhs_abusive, cautions, bans FROM user_info WHERE user_id = " + UserId + "");
                DataRow Info = dbClient.getRow();

                if (User == null)
                {
                    throw new NullReferenceException("No user found in database");
                }

                ServerMessage Message = new ServerMessage(533);

                Message.AppendUInt(Convert.ToUInt32(User["id"]));
                Message.AppendStringWithBreak((string)User["username"]);

                if (Info != null)
                {
                    Message.AppendInt32((int)Math.Ceiling((ButterflyEnvironment.GetUnixTimestamp() - (Double)Info["reg_timestamp"]) / 60));
                    Message.AppendInt32((int)Math.Ceiling((ButterflyEnvironment.GetUnixTimestamp() - (Double)Info["login_timestamp"]) / 60));
                }
                else
                {
                    Message.AppendInt32(0);
                    Message.AppendInt32(0);
                }

                if (User["online"].ToString() == "1")
                {
                    Message.AppendBoolean(true);
                }
                else
                {
                    Message.AppendBoolean(false);
                }

                if (Info != null)
                {
                    Message.AppendInt32((int)Info["cfhs"]);
                    Message.AppendInt32((int)Info["cfhs_abusive"]);
                    Message.AppendInt32((int)Info["cautions"]);
                    Message.AppendInt32((int)Info["bans"]);
                }
                else
                {
                    Message.AppendInt32(0); // cfhs
                    Message.AppendInt32(0); // abusive cfhs
                    Message.AppendInt32(0); // cautions
                    Message.AppendInt32(0); // bans
                }

                return Message;
            }
        }

        internal static ServerMessage SerializeRoomVisits(UInt32 UserId)
        {
            using (IQueryAdapter dbClient = ButterflyEnvironment.GetDatabaseManager().getQueryreactor())
            {
                dbClient.setQuery("SELECT room_id,hour,minute FROM user_roomvisits WHERE user_id = " + UserId + " ORDER BY entry_timestamp DESC LIMIT 50");
                DataTable Data = dbClient.getTable();

                ServerMessage Message = new ServerMessage(537);
                Message.AppendUInt(UserId);
                Message.AppendStringWithBreak(ButterflyEnvironment.GetGame().GetClientManager().GetNameById(UserId));

                if (Data != null)
                {
                    Message.AppendInt32(Data.Rows.Count);

                    foreach (DataRow Row in Data.Rows)
                    {
                        RoomData RoomData = ButterflyEnvironment.GetGame().GetRoomManager().GenerateNullableRoomData(Convert.ToUInt32(Row["room_id"]));

                        if (RoomData != null)
                        {
                            Message.AppendBoolean(RoomData.IsPublicRoom);
                            Message.AppendUInt(RoomData.Id);
                            Message.AppendStringWithBreak(RoomData.Name);
                        }
                        else
                        {
                            Message.AppendBoolean(false);
                            Message.AppendUInt(Convert.ToUInt32(Row["room_id"]));
                            Message.AppendStringWithBreak("Unknown Room");
                        }

                        Message.AppendInt32((int)Row["hour"]);
                        Message.AppendInt32((int)Row["minute"]);
                    }
                }
                else
                {
                    Message.AppendInt32(0);
                }

                return Message;
            }
        
        }

        #endregion

        #region Chatlogs

        internal static ServerMessage SerializeTicketChatlog(SupportTicket Ticket, RoomData RoomData, double Timestamp)
        {
            ServerMessage Message = new ServerMessage(534);
            Message.AppendUInt(Ticket.TicketId);
            Message.AppendUInt(Ticket.SenderId);
            Message.AppendUInt(Ticket.ReportedId);
            Message.AppendBoolean(RoomData.IsPublicRoom);
            Message.AppendUInt(RoomData.Id);
            Message.AppendStringWithBreak(RoomData.Name);

            try
            {
                // Recupera la stanza reale
                Room currentRoom = ButterflyEnvironment.GetGame().GetRoomManager().GetRoom(RoomData.Id);
                if (currentRoom == null)
                {
                    Message.AppendInt32(0); // stanza offline o inesistente
                    return Message;
                }

                using (IQueryAdapter dbClient = ButterflyEnvironment.GetDatabaseManager().getQueryreactor())
                {
                    // Legge tutti i messaggi della stanza dalla tabella chatlogs
                    dbClient.setQuery("SELECT user_id, user_name, hour, minute, message FROM chatlogs " +
                                      "WHERE room_id = " + currentRoom.RoomId + " ORDER BY timestamp ASC");

                    DataTable Data = dbClient.getTable();
                    if (Data != null && Data.Rows.Count > 0)
                    {
                        Message.AppendInt32(Data.Rows.Count);

                        foreach (DataRow Row in Data.Rows)
                        {
                            int hour = Row["hour"] != DBNull.Value ? Convert.ToInt32(Row["hour"]) : 0;
                            int minute = Row["minute"] != DBNull.Value ? Convert.ToInt32(Row["minute"]) : 0;
                            uint userId = Row["user_id"] != DBNull.Value ? Convert.ToUInt32(Row["user_id"]) : 0;
                            string userName = Row["user_name"] != DBNull.Value ? (string)Row["user_name"] : "Unknown";
                            string messageText = Row["message"] != DBNull.Value ? (string)Row["message"] : "";

                            Message.AppendInt32(hour);
                            Message.AppendInt32(minute);
                            Message.AppendUInt(userId);
                            Message.AppendStringWithBreak(userName);
                            Message.AppendStringWithBreak(messageText);
                        }
                    }
                    else
                    {
                        Message.AppendInt32(0);
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine("Errore SerializeTicketChatlog: " + ex.Message);
                Message.AppendInt32(0);
            }

            return Message;
        }

        internal static ServerMessage SerializeRoomChatlog(UInt32 roomID)
        {
            Room currentRoom = ButterflyEnvironment.GetGame().GetRoomManager().GetRoom(roomID);
            ServerMessage Message = new ServerMessage(535);

            if (currentRoom == null)
            {
                Message.AppendBoolean(false);
                Message.AppendUInt(roomID);
                Message.AppendStringWithBreak("Unknown Room");
                Message.AppendInt32(0);
                return Message;
            }

            Message.AppendBoolean(currentRoom.IsPublic);
            Message.AppendUInt(currentRoom.RoomId);
            Message.AppendStringWithBreak(currentRoom.Name);

            try
            {
                using (IQueryAdapter dbClient = ButterflyEnvironment.GetDatabaseManager().getQueryreactor())
                {
                    dbClient.setQuery("SELECT user_id, user_name, hour, minute, message FROM chatlogs " +
                                      "WHERE room_id = " + currentRoom.RoomId + " ORDER BY timestamp DESC LIMIT 150");

                    DataTable Data = dbClient.getTable();
                    if (Data != null && Data.Rows.Count > 0)
                    {
                        Message.AppendInt32(Data.Rows.Count);

                        foreach (DataRow Row in Data.Rows)
                        {
                            int hour = Row["hour"] != DBNull.Value ? Convert.ToInt32(Row["hour"]) : 0;
                            int minute = Row["minute"] != DBNull.Value ? Convert.ToInt32(Row["minute"]) : 0;
                            uint userId = Row["user_id"] != DBNull.Value ? Convert.ToUInt32(Row["user_id"]) : 0;
                            string userName = Row["user_name"] != DBNull.Value ? (string)Row["user_name"] : "Unknown";
                            string messageText = Row["message"] != DBNull.Value ? (string)Row["message"] : "";

                            Message.AppendInt32(hour);
                            Message.AppendInt32(minute);
                            Message.AppendUInt(userId);
                            Message.AppendStringWithBreak(userName);
                            Message.AppendStringWithBreak(messageText);
                        }
                    }
                    else
                    {
                        Message.AppendInt32(0);
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine("Errore SerializeRoomChatlog: " + ex.Message);
                Message.AppendInt32(0);
            }

            return Message;
        }

        internal static ServerMessage SerializeUserChatlog(UInt32 UserId)
        {
            ServerMessage Message = new ServerMessage(536);
            Message.AppendUInt(UserId);
            Message.AppendStringWithBreak(ButterflyEnvironment.GetGame().GetClientManager().GetNameById(UserId));

            try
            {
                using (IQueryAdapter dbClient = ButterflyEnvironment.GetDatabaseManager().getQueryreactor())
                {
                    dbClient.setQuery("SELECT room_id, user_id, user_name, hour, minute, message FROM chatlogs " +
                                      "WHERE user_id = " + UserId + " ORDER BY timestamp DESC LIMIT 200");

                    DataTable Data = dbClient.getTable();
                    if (Data != null && Data.Rows.Count > 0)
                    {
                        Message.AppendInt32(Data.Rows.Count);

                        foreach (DataRow Row in Data.Rows)
                        {
                            uint roomId = Row["room_id"] != DBNull.Value ? Convert.ToUInt32(Row["room_id"]) : 0;
                            int hour = Row["hour"] != DBNull.Value ? Convert.ToInt32(Row["hour"]) : 0;
                            int minute = Row["minute"] != DBNull.Value ? Convert.ToInt32(Row["minute"]) : 0;
                            uint userId = Row["user_id"] != DBNull.Value ? Convert.ToUInt32(Row["user_id"]) : 0;
                            string userName = Row["user_name"] != DBNull.Value ? (string)Row["user_name"] : "Unknown";
                            string messageText = Row["message"] != DBNull.Value ? (string)Row["message"] : "";

                            RoomData RoomData = ButterflyEnvironment.GetGame().GetRoomManager().GenerateNullableRoomData(roomId);

                            Message.AppendBoolean(RoomData != null && RoomData.IsPublicRoom);
                            Message.AppendUInt(roomId);
                            Message.AppendStringWithBreak(RoomData != null ? RoomData.Name : "Unknown");

                            Message.AppendInt32(1); // un messaggio per questa iterazione
                            Message.AppendInt32(hour);
                            Message.AppendInt32(minute);
                            Message.AppendUInt(userId);
                            Message.AppendStringWithBreak(userName);
                            Message.AppendStringWithBreak(messageText);
                        }
                    }
                    else
                    {
                        Message.AppendInt32(0);
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine("Errore SerializeUserChatlog: " + ex.Message);
                Message.AppendInt32(0);
            }

            return Message;
        }

    }
    #endregion
}



