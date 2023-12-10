using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SchoolMusic.Common
{
    public class Messages
    {
        //Player - SetSchedule

        public const string WrongHourError = "Имате грешка във формата на въведения час! Моля опитайте отново.";
        public const string WrongNumberOrLength = "Имате грешка във формата, опитайте отново";
        public const string StartingHourMsg = "Моля въведете часа, в който искате да се стартира песента във формат час:минути:секунди. Пример: 10:21:00";
        public const string SongNumberAndLengthMsg = "Моля въведете номера на песента и продължителността ѝ в секунди. Пример 1,120";

        public const string NextAvaliableTiming = "Следващия свободен час е в {0}";
        public const string TimingAlreadyExist = "Въведения час вече е зает. Опитайте отново.";

        //Player - CreatePlayList

        public const string WrongInput = "Въведения вход е с грешен формат! Опитайте отново.";
        public const string ExistingPlaylistName = "Въведеното име за плейлист вече съществува. Изберете друго.";
        public const string SongsCountMsg = "Въведе бройката на песните, които ще се стартират.";
        public const string SavePlaylistMsg = "Ако желаете този плейлист да бъде запазен натиснете 1, в противен случай натиснете 2";
        public const string NamingPlaylistMsg = "Въведете име на плейлиста.";
        public const string NextSongStartingTime = "Следващата песен ще започне в {0}.";
        public const string SuccessfullyCreatedPlaylist = "Плейлист с име {0} беше успешно създаден!";

        //Program - Main

        public const string StartingMenu = "Ако искате да използвате готов плейлист натиснете 1.\r\nАко искате да създадете нов плейлист натиснете 2.";
        public const string ChooseList = "Изберете от списъка с плейлисти и въведете името на плейлиста, който желаете да се стартира.";

        public const string EndMessage = "Всички песни бяха изпълнени за деня!";

        public const string InvalidNumber = "Въвели сте невалиден номер";

    }
}
