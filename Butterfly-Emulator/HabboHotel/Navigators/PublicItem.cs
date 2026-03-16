using System;
using Butterfly.Messages;
using Butterfly.HabboHotel.Rooms;
using Butterfly;

namespace Butterfly.HabboHotel.Navigators
{
    internal enum PublicImageType
    {
        INTERNAL = 0,
        EXTERNAL = 1
    }

    internal class PublicItem
    {
        private readonly Int32 BannerId;

        internal int Type;
        internal string Caption;
        internal string Image;
        internal PublicImageType ImageType;
        internal UInt32 RoomId;
        internal Int32 ParentId;
        internal Boolean Category;
        internal Boolean Recommended;

        internal Int32 Id
        {
            get { return BannerId; }
        }

        internal RoomData RoomData
        {
            get
            {
                if (RoomId == 0 || ButterflyEnvironment.GetGame() == null || ButterflyEnvironment.GetGame().GetRoomManager() == null)
                {
                    // Se non c'è un RoomId valido o RoomManager, restituisci null
                    return null;
                }

                var roomData = ButterflyEnvironment.GetGame().GetRoomManager().GenerateRoomData(RoomId);

                // Se non esiste la stanza, restituisci null
                return roomData;
            }
        }

        internal PublicItem(int mId, int mType, string mCaption, string mImage, PublicImageType mImageType, uint mRoomId, int mParentId, Boolean mCategory, Boolean mRecommand)
        {
            BannerId = mId;
            Type = mType;
            Caption = mCaption;
            Image = mImage;
            ImageType = mImageType;
            RoomId = mRoomId;
            ParentId = mParentId;
            Category = mCategory;
            Recommended = mRecommand;
        }

        internal void Serialize(ServerMessage Message)
        {
            if (!Category)
            {
                Message.AppendInt32(Id);

                // Se RoomData è null, usa valori di fallback
                string roomName = RoomData != null ? RoomData.Name : "Stanza non disponibile";
                string roomDescription = RoomData != null ? RoomData.Description : "";
                int usersNow = RoomData != null ? RoomData.UsersNow : 0;
                int usersMax = RoomData != null ? RoomData.UsersMax : 0;
                string ccts = RoomData != null ? RoomData.CCTs : "";

                Message.AppendStringWithBreak((Type == 1) ? Caption : roomName);
                Message.AppendStringWithBreak(roomDescription);
                Message.AppendInt32(Type);
                Message.AppendStringWithBreak(Caption);
                Message.AppendStringWithBreak((ImageType == PublicImageType.EXTERNAL) ? Image : string.Empty);
                Message.AppendInt32(ParentId);
                Message.AppendInt32(usersNow);
                Message.AppendInt32(3);
                Message.AppendStringWithBreak((ImageType == PublicImageType.INTERNAL) ? Image : string.Empty);
                Message.AppendUInt(1337);
                Message.AppendBoolean(true);
                Message.AppendStringWithBreak(ccts);
                Message.AppendInt32(usersMax);
                Message.AppendUInt(RoomId);
            }
            else
            {
                Message.AppendInt32(Id);
                Message.AppendStringWithBreak(Caption);
                Message.AppendStringWithBreak(string.Empty);
                Message.AppendBoolean(true);
                Message.AppendStringWithBreak(string.Empty);
                Message.AppendStringWithBreak(Image);
                Message.AppendBoolean(false);
                Message.AppendBoolean(false);
                Message.AppendInt32(4);
                Message.AppendBoolean(false);
            }
        }
    }
}