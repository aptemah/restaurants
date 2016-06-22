using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Intouch.Core
{
    public enum TypePoint
    {
        Fitness = 0,
        Restaurant = 1
    }

    public class Conversation
    {
        public int Id { get; set; }
        public DateTimeOffset Created { get; set; }
        public string Name { get; set; }
        public string Code { get; set; }
        public DateTimeOffset? Activity { get; set; } // TODO: поле будет обезательным позже будет сохранять дату последнего сообщения!
       // public DateTimeOffset? Tolerance { get; set; } // TODO: Поле разрешен или не разрешен разговор с этим человеком!
       // public int? FromWho { get; set; } //TODO: От кого пришел запрос на общение! в том случае елси Tolerance NULL! 
        //public DateTimeOffset? Ban { get; set; } // TODO: забанена ли конфа!
        //public int? BanUser { get; set; } // TODO: Кто дал бан! Переделать на сущность человека
        public virtual Point Point { get; set; }
        public virtual TypePoint TypePoint { get; set; }
        public virtual ICollection<ChatParticipant> Participants { get; set; }
        public virtual ICollection<ChatMessage> Messages { get; set; }
    }
}