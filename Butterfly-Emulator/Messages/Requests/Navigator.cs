using Butterfly.HabboHotel.Navigators;
using Butterfly.HabboHotel.Rooms;
using Butterfly.Core;
using Butterfly.HabboHotel.Pathfinding;
using System;
using System.Threading;
using Database_Manager.Database.Session_Details.Interfaces;

namespace Butterfly.Messages
{
    partial class GameClientMessageHandler
    {
        internal void AddFavorite()
        {
            uint Id = Request.PopWiredUInt();
            RoomData Data = ButterflyEnvironment.GetGame().GetRoomManager().GenerateRoomData(Id);

            if (Data == null || Session.GetHabbo().FavoriteRooms.Count >= 30 || Session.GetHabbo().FavoriteRooms.Contains(Id) || Data.Type == "public")
            {
                GetResponse().Init(33);
                GetResponse().AppendInt32(-9001);
                SendResponse();
                return;
            }

            GetResponse().Init(459);
            GetResponse().AppendUInt(Id);
            GetResponse().AppendBoolean(true);
            SendResponse();

            Session.GetHabbo().FavoriteRooms.Add(Id);

            using (IQueryAdapter dbClient = ButterflyEnvironment.GetDatabaseManager().getQueryreactor())
            {
                dbClient.runFastQuery("INSERT INTO user_favorites (user_id,room_id) VALUES (" + Session.GetHabbo().Id + "," + Id + ")");
            }
        }

        internal void RemoveFavorite()
        {
            uint Id = Request.PopWiredUInt();
            Session.GetHabbo().FavoriteRooms.Remove(Id);

            GetResponse().Init(459);
            GetResponse().AppendUInt(Id);
            GetResponse().AppendBoolean(false);
            SendResponse();

            using (IQueryAdapter dbClient = ButterflyEnvironment.GetDatabaseManager().getQueryreactor())
            {
                dbClient.runFastQuery("DELETE FROM user_favorites WHERE user_id = " + Session.GetHabbo().Id + " AND room_id = " + Id);
            }
        }

        internal void GoToHotelView()
        {
            if (Session.GetHabbo().InRoom)
            {
                Room currentRoom = ButterflyEnvironment.GetGame().GetRoomManager().GetRoom(Session.GetHabbo().CurrentRoomId);
                if (currentRoom != null)
                    currentRoom.GetRoomUserManager().RemoveUserFromRoom(Session, true, false);
                Session.CurrentRoomUserID = -1;
            }
        }

        internal void GetFlatCats()
        {
            Session.SendMessage(ButterflyEnvironment.GetGame().GetNavigator().SerializeFlatCategories());
        }

        internal void EnterInquiredRoom()
        {
            uint Id = Request.PopWiredUInt();
            string Password = "";

            Console.WriteLine("[Navigator-EnterInquiredRoom] Loading room [" + Id + "]");

            RoomData Data = ButterflyEnvironment.GetGame().GetRoomManager().GenerateRoomData(Id);

            if ((Data != null) && (Data.Type == "public"))
            {
                PrepareRoomForUser(Id, Password);
            }
        }
        internal void GetPubs()
        {
            Session.SendMessage(ButterflyEnvironment.GetGame().GetNavigator().SerializePublicRooms());
        }

        internal void GetRoomInfo()
        {
            uint RoomId = Request.PopWiredUInt();
            bool unk = Request.PopWiredBoolean();
            bool unk2 = Request.PopWiredBoolean();

            RoomData Data = ButterflyEnvironment.GetGame().GetRoomManager().GenerateRoomData(RoomId);

            if (Data == null) return;

            GetResponse().Init(454);
            GetResponse().AppendBoolean(unk);
            Data.Serialize(GetResponse(), false);
            GetResponse().AppendBoolean(true);
            GetResponse().AppendBoolean(unk2);
            GetResponse().AppendBoolean(false);
            Session.SendMessage(GetResponse());
        }

        internal void GetPopularRooms()
        {
            Session.SendMessage(ButterflyEnvironment.GetGame().GetNavigator().SerializeNavigator(Session, Request.PopFixedInt32()));
        }

        internal void GetHighRatedRooms()
        {
            Session.SendMessage(ButterflyEnvironment.GetGame().GetNavigator().SerializeNavigator(Session, -2));
        }

        internal void GetFriendsRooms()
        {
            Session.SendMessage(ButterflyEnvironment.GetGame().GetNavigator().SerializeNavigator(Session, -4));
        }

        internal void GetRoomsWithFriends()
        {
            Session.SendMessage(ButterflyEnvironment.GetGame().GetNavigator().SerializeNavigator(Session, -5));
        }

        internal void GetOwnRooms()
        {
            Session.SendMessage(ButterflyEnvironment.GetGame().GetNavigator().SerializeNavigator(Session, -3));
        }

        internal void GetFavoriteRooms()
        {
            Session.SendMessage(ButterflyEnvironment.GetGame().GetNavigator().SerializeFavoriteRooms(Session));
        }

        internal void GetRecentRooms()
        {
            Session.SendMessage(ButterflyEnvironment.GetGame().GetNavigator().SerializeRecentRooms(Session));
        }

        internal void GetEvents()
        {
            int Category = int.Parse(Request.PopFixedString());

            // ⚡ Chiamata corretta perché SerializeEventListing è static
            ServerMessage Message = Navigator.SerializeEventListing(Category);
            Session.SendMessage(Message);
        }
        internal void GetPopularTags()
        {
            Session.SendMessage(ButterflyEnvironment.GetGame().GetNavigator().SerializePopularRoomTags());
        }

        internal void PerformSearch()
        {
            Session.SendMessage(ButterflyEnvironment.GetGame().GetNavigator().SerializeSearchResults(Request.PopFixedString()));
        }

        internal void PerformSearch2()
        {
            int junk = Request.PopWiredInt32();
            Session.SendMessage(ButterflyEnvironment.GetGame().GetNavigator().SerializeSearchResults(Request.PopFixedString()));
        }

        internal void OpenFlat()
        {
            uint Id = Request.PopWiredUInt();
            string Password = Request.PopFixedString();
            int Junk = Request.PopWiredInt32();

            Console.WriteLine("[Navigator-OpenFlat] Loading room [" + Id + "]");

            RoomData Data = ButterflyEnvironment.GetGame().GetRoomManager().GenerateRoomData(Id);

            if (Data != null)
            {
                if (Data.Type == "private")
                {
                    Console.WriteLine("[Navigator-OpenFlat] Stanza privata, caricamento...");
                    PrepareRoomForUser(Id, Password);
                }
                else if (Data.Type == "public")
                {
                    Console.WriteLine("[Navigator-OpenFlat] Stanza pubblica, caricamento...");
                    PrepareRoomForUser(Id, string.Empty);
                }
                else
                {
                    Console.WriteLine("[Navigator-OpenFlat] Tipo stanza sconosciuto: " + Data.Type);
                }
            }
            else
            {
                Console.WriteLine("[Navigator-OpenFlat] RoomData è null per stanza: " + Id);
            }
        }
    }
        }
    
    
