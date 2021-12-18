using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace OnlineStore.WebUI.Models
{
    public class User
    {
        // Уникальный идентификатор пользователя (Primary Key)
        public int Id { get; set; }
        // E-mail пользователя, нужен для авторизации
        public string Email { get; set; }
        // Пароль пользователя, так же нужен для авторизации
        public string Password { get; set; }
        // Логин в доте. Ещё в разработке, будет использоваться для идентификации пользователя как студента для получения скидок и т.п.
        public string DotUsername { get; set; }
        // Уникальный идентификатор корзины пользователя
        public int CartId { get  ; set; }

        //Конструктор, в котором ID корзины соотносится с Id пользователя
        public User()
        {
            CartId = Id;
        }

    }
}
