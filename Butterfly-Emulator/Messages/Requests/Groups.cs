using System.Data;
using Database_Manager.Database.Session_Details.Interfaces;

namespace Butterfly.Messages
{
    internal partial class GameClientMessageHandler
    {
        internal void GetGroupdetails()
        {
            if (!ButterflyEnvironment.groupsEnabled)
                return;

            // Legge l'ID del gruppo dal client
            int groupID = Request.PopWiredInt32();

            DataRow dRow;
            using (IQueryAdapter dbClient = ButterflyEnvironment.GetDatabaseManager().getQueryreactor())
            {
                // Seleziona le colonne corrette e rinomina 'desc' per evitare conflitti
                dbClient.setQuery("SELECT id, name, `desc` AS description, roomid, ownerid FROM groups WHERE id = @groupid");
                dbClient.addParameter("groupid", groupID);
                dRow = dbClient.getRow();
            }

            if (dRow != null)
            {
                Response.Init(311); // Dw packet

                // ID gruppo
                Response.AppendInt32((int)dRow["id"]);

                // Nome gruppo
                Response.AppendStringWithBreak((string)dRow["name"]);

                // Descrizione gruppo
                Response.AppendStringWithBreak((string)dRow["description"]);

                // RoomID (controlla se valido)
                int roomID = 0;
                if (int.TryParse(dRow["roomid"].ToString(), out roomID) && roomID > 0)
                    Response.AppendInt32(roomID);
                else
                    Response.AppendInt32(-1);

                // Badge placeholder, puoi cambiare con badge reale se vuoi
                Response.AppendStringWithBreak("Test");

                // Invia risposta al client
                SendResponse();
            }
            else
            {
                // Se non trova il gruppo, invia ID -1
                Response.Init(311);
                Response.AppendInt32(-1);
                Response.AppendStringWithBreak("Unknown Group");
                Response.AppendStringWithBreak("");
                Response.AppendInt32(-1);
                Response.AppendStringWithBreak("");
                SendResponse();
            }
        }
    }
}