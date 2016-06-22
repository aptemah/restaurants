using Intouch.Core;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web.Mvc;

namespace Intouch.Restaurant.Controllers
{
    public class MenuController : BaseController
    {
        public void AddMenu()
        {
            var restaurant = db.RestPoints.Find(1);
            //кухня
            var Menu1 = new RestCategory { RestPoint = restaurant, Name = "Кухня", Image = "kitchen.png", Activity = Activity.Active };
            db.RestCategorys.Add(Menu1);
            //японская кухня
            var Menu1Category1 = new RestCategory { RestPoint = restaurant, Name = "Японская кухня", ParentCategory = Menu1, Image = "asia_food.png", Activity = Activity.Active };
            db.RestCategorys.Add(Menu1Category1);
            //суши
            var Menu1Category1SubCategory1 = new RestCategory { RestPoint = restaurant, Name = "Суши", ParentCategory = Menu1Category1, Image = "sushi.png", Activity = Activity.Active };
            db.RestCategorys.Add(Menu1Category1SubCategory1);
            var Menu1Category1SubCategory1SubSubCategory1SubProduct1 = new RestProduct { Price = 200, Category = Menu1Category1SubCategory1, Name = "Лосось", Weight = "63гр.", Activity = Activity.Active };
            db.RestProducts.Add(Menu1Category1SubCategory1SubSubCategory1SubProduct1);
            var Menu1Category1SubCategory1SubSubCategory2SubProduct2 = new RestProduct { Price = 200, Category = Menu1Category1SubCategory1, Name = "Тунец", Weight = "63гр.", Activity = Activity.Active };
            db.RestProducts.Add(Menu1Category1SubCategory1SubSubCategory2SubProduct2);
            var Menu1Category1SubCategory1SubSubCategory3SubProduct3 = new RestProduct { Price = 200, Category = Menu1Category1SubCategory1, Name = "Копченый угорь", Weight = "63гр.", Activity = Activity.Active };
            db.RestProducts.Add(Menu1Category1SubCategory1SubSubCategory3SubProduct3);
            //острые суши
            var Menu1Category1SubCategory2 = new RestCategory { RestPoint = restaurant, Name = "Острые суши", ParentCategory = Menu1Category1, Image = "sushi.png", Activity = Activity.Active };
            db.RestCategorys.Add(Menu1Category1SubCategory2);
            var Menu1Category1SubCategory2SubSubCategory1SubProduct1 = new RestProduct { Price = 200, Category = Menu1Category1SubCategory2, Name = "Лосось", Weight = "65гр.", Activity = Activity.Active };
            db.RestProducts.Add(Menu1Category1SubCategory2SubSubCategory1SubProduct1);
            var Menu1Category1SubCategory2SubSubCategory2SubProduct2 = new RestProduct { Price = 200, Category = Menu1Category1SubCategory2, Name = "Тунец", Weight = "65гр.", Activity = Activity.Active };
            db.RestProducts.Add(Menu1Category1SubCategory2SubSubCategory2SubProduct2);
            //сашими
            var Menu1Category1SubCategory3 = new RestCategory { RestPoint = restaurant, Name = "Сашими", ParentCategory = Menu1Category1, Image = "sushi.png", Activity = Activity.Active };
            db.RestCategorys.Add(Menu1Category1SubCategory3);
            var Menu1Category1SubCategory3SubSubCategory1SubProduct1 = new RestProduct { Price = 440, Category = Menu1Category1SubCategory3, Name = "Лосось", Weight = "74гр.", Description = "Подается с дайконом,листом салата и лимоном", Activity = Activity.Active };
            db.RestProducts.Add(Menu1Category1SubCategory3SubSubCategory1SubProduct1);
            var Menu1Category1SubCategory3SubSubCategory2SubProduct2 = new RestProduct { Price = 540, Category = Menu1Category1SubCategory3, Name = "Копченый угорь", Weight = "74гр.", Description = "Подается с дайконом,листом салата и лимоном", Activity = Activity.Active };
            db.RestProducts.Add(Menu1Category1SubCategory3SubSubCategory2SubProduct2);
            //роллы
            var Menu1Category1SubCategory4 = new RestCategory { RestPoint = restaurant, Name = "Роллы", ParentCategory = Menu1Category1, Image = "sushi.png", Activity = Activity.Active };
            db.RestCategorys.Add(Menu1Category1SubCategory4);
            var Menu1Category1SubCategory4SubSubCategory1SubProduct1 = new RestProduct { Price = 750, Category = Menu1Category1SubCategory4, Name = "«Калифорния»", Weight = "210гр.", Description = "Мясо краба,авокадо,огурец,икра «тобико»", Activity = Activity.Active };
            db.RestProducts.Add(Menu1Category1SubCategory4SubSubCategory1SubProduct1);
            var Menu1Category1SubCategory4SubSubCategory2SubProduct2 = new RestProduct { Price = 660, Category = Menu1Category1SubCategory4, Name = "«Филадельфия»", Weight = "260гр.", Description = "Лосось,копченый угорь,сыр «филадельфия»,огурец", Activity = Activity.Active };
            db.RestProducts.Add(Menu1Category1SubCategory4SubSubCategory2SubProduct2);
            var Menu1Category1SubCategory4SubSubCategory3SubProduct3 = new RestProduct { Price = 620, Category = Menu1Category1SubCategory4, Name = "«Канада»", Weight = "250гр.", Description = "Лосось,копченый угорь,сыр «филадельфия»,авокадо", Activity = Activity.Active };
            db.RestProducts.Add(Menu1Category1SubCategory4SubSubCategory3SubProduct3);
            //теплые роллы
            var Menu1Category1SubCategory5 = new RestCategory { RestPoint = restaurant, Name = "Теплые роллы", ParentCategory = Menu1Category1, Image = "sushi.png", Activity = Activity.Active };
            db.RestCategorys.Add(Menu1Category1SubCategory5);
            var Menu1Category1SubCategory5SubSubCategory1SubProduct1 = new RestProduct { Price = 750, Category = Menu1Category1SubCategory5, Name = "Ролл «Who is Who»", Weight = "250гр.", Description = "Лосось,копченый угорь,мясо краба,сыр «филадельфия»", Activity = Activity.Active };
            db.RestProducts.Add(Menu1Category1SubCategory5SubSubCategory1SubProduct1);
            var Menu1Category1SubCategory5SubSubCategory2SubProduct2 = new RestProduct { Price = 700, Category = Menu1Category1SubCategory5, Name = "Ролл «Норвегия»", Weight = "240гр.", Description = "Тигровые креветки,лист салата,«соус «спайс»", Activity = Activity.Active };
            db.RestProducts.Add(Menu1Category1SubCategory5SubSubCategory2SubProduct2);
            //европейская кухня
            var Menu1Category2 = new RestCategory { RestPoint = restaurant, Name = "Европейская кухня", ParentCategory = Menu1, Image = "euro_food.png", Activity = Activity.Active };
            db.RestCategorys.Add(Menu1Category2);
            //Салаты
            var Menu1Category2SubCategory1 = new RestCategory { RestPoint = restaurant, Name = "Салаты", ParentCategory = Menu1Category2, Image = "salad.png", Activity = Activity.Active };
            db.RestCategorys.Add(Menu1Category2SubCategory1);
            var Menu1Category2SubCategory1SubSubCategory1SubProduct1 = new RestProduct { Price = 1500, Category = Menu1Category2SubCategory1, Name = "Фирменный салат «Who is Who»", Weight = "250гр.", Description = "(Ананас,морепродукты,салатный микс,секрет шефа)", Activity = Activity.Active };
            db.RestProducts.Add(Menu1Category2SubCategory1SubSubCategory1SubProduct1);
            var Menu1Category2SubCategory1SubSubCategory2SubProduct2 = new RestProduct { Price = 800, Category = Menu1Category2SubCategory1, Name = "Цезарь с цыпленком", Weight = "200гр.", Description = "(Микс салата,соус цезарь,пармезан,филе цыпленка)", Activity = Activity.Active };
            db.RestProducts.Add(Menu1Category2SubCategory1SubSubCategory2SubProduct2);
            var Menu1Category2SubCategory1SubSubCategory3SubProduct3 = new RestProduct { Price = 950, Category = Menu1Category2SubCategory1, Name = "Цезарь с тигровыми креветками", Weight = "260гр.", Description = "(Микс салата,соус цезарь,пармезан,тигровые креветки)", Activity = Activity.Active };
            db.RestProducts.Add(Menu1Category2SubCategory1SubSubCategory3SubProduct3);
            var Menu1Category2SubCategory1SubSubCategory4SubProduct4 = new RestProduct { Price = 850, Category = Menu1Category2SubCategory1, Name = "Овощной салат с креветками и сербским сыром", Weight = "300гр.", Description = "(Помидор,болгарский перец,огурец,красный лук,сербская брынза,креветки)", Activity = Activity.Active };
            db.RestProducts.Add(Menu1Category2SubCategory1SubSubCategory4SubProduct4);
            var Menu1Category2SubCategory1SubSubCategory5SubProduct5 = new RestProduct { Price = 700, Category = Menu1Category2SubCategory1, Name = "Салат с куриной печенью и грушей", Weight = "250гр.", Description = "(Микс салата,груша,медовый соус Тонкацу,куриная печень,грецкий орех)", Activity = Activity.Active };
            db.RestProducts.Add(Menu1Category2SubCategory1SubSubCategory5SubProduct5);
            //Холодные закуски
            var Menu1Category2SubCategory2 = new RestCategory { RestPoint = restaurant, Name = "Холодные закуски", ParentCategory = Menu1Category2, Image = "cold_snacks.png", Activity = Activity.Active };
            db.RestCategorys.Add(Menu1Category2SubCategory2);
            var Menu1Category2SubCategory2SubSubCategory1SubProduct1 = new RestProduct { Price = 1800, Category = Menu1Category2SubCategory2, Name = "Мясное ассорти", Weight = "200гр.", Description = "(Ростбиф,бастурма,говяжий язык,пармская ветчина)", Activity = Activity.Active };
            db.RestProducts.Add(Menu1Category2SubCategory2SubSubCategory1SubProduct1);
            var Menu1Category2SubCategory2SubSubCategory2SubProduct2 = new RestProduct { Price = 1600, Category = Menu1Category2SubCategory2, Name = "Ассорти сыров", Weight = "200гр.", Description = "(Дор блю,пармезан,бри,копченый сыр скаморца,проволоне)", Activity = Activity.Active };
            db.RestProducts.Add(Menu1Category2SubCategory2SubSubCategory2SubProduct2);
            var Menu1Category2SubCategory2SubSubCategory3SubProduct3 = new RestProduct { Price = 2000, Category = Menu1Category2SubCategory2, Name = "Рыбное ассорти", Weight = "200гр.", Description = "(Лосось с/с,угорь,масляная рыба,тигровые креветки,красная икра)", Activity = Activity.Active };
            db.RestProducts.Add(Menu1Category2SubCategory2SubSubCategory3SubProduct3);
            var Menu1Category2SubCategory2SubSubCategory4SubProduct4 = new RestProduct { Price = 700, Category = Menu1Category2SubCategory2, Name = "Домашние соленья", Weight = "200гр.", Description = "(Соленые огурцы,соленый зеленый помидор,маринованный чеснок,черемша,капуста)", Activity = Activity.Active };
            db.RestProducts.Add(Menu1Category2SubCategory2SubSubCategory4SubProduct4);
            //горячие закуски
            var Menu1Category2SubCategory3 = new RestCategory { RestPoint = restaurant, Name = "Горячие закуски", ParentCategory = Menu1Category2, Image = "hot_snacks.png", Activity = Activity.Active };
            db.RestCategorys.Add(Menu1Category2SubCategory3);
            var Menu1Category2SubCategory3SubSubCategory1SubProduct1 = new RestProduct { Price = 680, Category = Menu1Category2SubCategory3, Name = "Жульен из цыпленка с грибами", Weight = "240гр.", Description = "(Филе цыпленка,грибы,помидоры черри,трюфельное масло,сыр пармезан)", Activity = Activity.Active };
            db.RestProducts.Add(Menu1Category2SubCategory3SubSubCategory1SubProduct1);
            var Menu1Category2SubCategory3SubSubCategory2SubProduct2 = new RestProduct { Price = 2100, Category = Menu1Category2SubCategory3, Name = "Жульен из камчатского краба", Weight = "230гр.", Description = "(Мясо краба,сливочный соус,помидоры черри,тимьян)", Activity = Activity.Active };
            db.RestProducts.Add(Menu1Category2SubCategory3SubSubCategory2SubProduct2);
            var Menu1Category2SubCategory3SubSubCategory3SubProduct3 = new RestProduct { Price = 950, Category = Menu1Category2SubCategory3, Name = "Мидии жареные с огурцом в соусе терияки", Weight = "250гр.", Description = "(Мидии «Киви»,огурцы,болгарский перец,лук порей,соус Терияки,помидоры черри,зелень)", Activity = Activity.Active };
            db.RestProducts.Add(Menu1Category2SubCategory3SubSubCategory3SubProduct3);
            //мясные горячие блюда
            var Menu1Category2SubCategory4 = new RestCategory { RestPoint = restaurant, Name = "Мясные горячие блюда", ParentCategory = Menu1Category2, Image = "meat.png", Activity = Activity.Active };
            db.RestCategorys.Add(Menu1Category2SubCategory4);
            var Menu1Category2SubCategory4SubSubCategory1SubProduct1 = new RestProduct { Price = 2000, Category = Menu1Category2SubCategory4, Name = "«Шато Бриан»", Description = "(Говяжья вырезка,помидоры черри,зелень,спаржа зеленая,оливковое масло)", Weight = "200гр", Activity = Activity.Active };
            db.RestProducts.Add(Menu1Category2SubCategory4SubSubCategory1SubProduct1);
            var Menu1Category2SubCategory4SubSubCategory2SubProduct2 = new RestProduct { Price = 2500, Category = Menu1Category2SubCategory4, Name = "Ягненок «Тандури»", Description = "(Каре ягненка,зелень,мини картофель,размарин,чеснок,гранатовый соус)", Weight = "220/80гр", Activity = Activity.Active };
            db.RestProducts.Add(Menu1Category2SubCategory4SubSubCategory2SubProduct2);
            var Menu1Category2SubCategory4SubSubCategory3SubProduct3 = new RestProduct { Price = 600, Category = Menu1Category2SubCategory4, Name = "Куриные котлеты с картофельным пюре", Description = "(Подаются со сливочным маслом,тимьяном,и картофельным пюре)", Weight = "180/140гр", Activity = Activity.Active };
            db.RestProducts.Add(Menu1Category2SubCategory4SubSubCategory3SubProduct3);
            var Menu1Category2SubCategory4SubSubCategory4SubProduct4 = new RestProduct { Price = 800, Category = Menu1Category2SubCategory4, Name = "«Бифштекс» из мраморной говядины", Description = "(рубленная говядина,яйцо пашот,зелень,черри)", Weight = "260гр", Activity = Activity.Active };
            db.RestProducts.Add(Menu1Category2SubCategory4SubSubCategory4SubProduct4);
            var Menu1Category2SubCategory4SubSubCategory5SubProduct5 = new RestProduct { Price = 600, Category = Menu1Category2SubCategory4, Name = "Жаркое из говядины с овощами", Description = "(Сочная говядина тушеная с картофелем,луком,морковью,помидорами,зелень)", Weight = "300", Activity = Activity.Active };
            db.RestProducts.Add(Menu1Category2SubCategory4SubSubCategory5SubProduct5);
            //супы
            var Menu1Category2SubCategory7 = new RestCategory { RestPoint = restaurant, Name = "Супы", ParentCategory = Menu1Category2, Image = "soup.png", Activity = Activity.Active };
            db.RestCategorys.Add(Menu1Category2SubCategory7);
            var Menu1Category2SubCategory7SubSubCategory1SubProduct1 = new RestProduct { Price = 700, Category = Menu1Category2SubCategory7, Name = "Крем суп с белыми грибами", Description = "(Белые грибы,сливки,трюфельное масло)", Weight = "300гр", Activity = Activity.Active };
            db.RestProducts.Add(Menu1Category2SubCategory7SubSubCategory1SubProduct1);
            var Menu1Category2SubCategory7SubSubCategory2SubProduct2 = new RestProduct { Price = 300, Category = Menu1Category2SubCategory7, Name = "Крем суп из брокколи", Weight = "280гр", Activity = Activity.Active };
            db.RestProducts.Add(Menu1Category2SubCategory7SubSubCategory2SubProduct2);
            //Рыбные горячие блюда
            var Menu1Category2SubCategory5 = new RestCategory { RestPoint = restaurant, Name = "Рыбные горячие блюда", ParentCategory = Menu1Category2, Image = "fish.png", Activity = Activity.Active };
            db.RestCategorys.Add(Menu1Category2SubCategory5);
            var Menu1Category2SubCategory5SubSubCategory1SubProduct1 = new RestProduct { Price = 1400, Category = Menu1Category2SubCategory5, Name = "Дорадо с овощами", Description = "(Дорадо ,цукини.болгарский перец,спаржа зелень)", Weight = "250гр", Activity = Activity.Active };
            db.RestProducts.Add(Menu1Category2SubCategory5SubSubCategory1SubProduct1);
            var Menu1Category2SubCategory5SubSubCategory2SubProduct2 = new RestProduct { Price = 1400, Category = Menu1Category2SubCategory5, Name = "Сибас на гриле", Description = "(жареный сибас,лимон,тимья,чеснок,оливковое масло,зелень)", Weight = "250гр", Activity = Activity.Active };
            db.RestProducts.Add(Menu1Category2SubCategory5SubSubCategory2SubProduct2);
            var Menu1Category2SubCategory5SubSubCategory3SubProduct3 = new RestProduct { Price = 900, Category = Menu1Category2SubCategory5, Name = "Стейк лосося на гриле", Description = "(лосось,тимьян,чеснок,масло оливковое,бальзамический соус)", Weight = "220гр", Activity = Activity.Active };
            db.RestProducts.Add(Menu1Category2SubCategory5SubSubCategory3SubProduct3);
            var Menu1Category2SubCategory5SubSubCategory4SubProduct4 = new RestProduct { Price = 1200, Category = Menu1Category2SubCategory5, Name = "«Сальмон Роз»", Description = "(лосось,шпинат,помидоры черри,соус сливочный)", Weight = "220/100гр", Activity = Activity.Active };
            db.RestProducts.Add(Menu1Category2SubCategory5SubSubCategory4SubProduct4);
            //ризотто
            var Menu1Category2SubCategory9 = new RestCategory { RestPoint = restaurant, Name = "Ризотто", ParentCategory = Menu1Category2, Image = "risotto.png", Activity = Activity.Active };
            db.RestCategorys.Add(Menu1Category2SubCategory9);
            var Menu1Category2SubCategory9SubSubCategory1SubProduct1 = new RestProduct { Price = 800, Category = Menu1Category2SubCategory9, Name = "Ризотто с грибами", Description = "(Грибы вешенки,лук ,белые грибы,трюфельное масло)", Weight = "270гр", Activity = Activity.Active };
            db.RestProducts.Add(Menu1Category2SubCategory9SubSubCategory1SubProduct1);
            var Menu1Category2SubCategory9SubSubCategory2SubProduct2 = new RestProduct { Price = 1300, Category = Menu1Category2SubCategory9, Name = "Ризотто с морепродуктами и чернилами каракатицы", Description = "(Тигровые креветки,мини кальмары,морской гребешок,зелень,рыбный бульон,чернила каракатицы)", Weight = "300гр", Activity = Activity.Active };
            db.RestProducts.Add(Menu1Category2SubCategory9SubSubCategory2SubProduct2);
            //Гарниры
            var Menu1Category2SubCategory6 = new RestCategory { RestPoint = restaurant, Name = "Гарниры", ParentCategory = Menu1Category2, Image = "garnish.png", Activity = Activity.Active };
            db.RestCategorys.Add(Menu1Category2SubCategory6);
            var Menu1Category2SubCategory6SubSubCategory1SubProduct1 = new RestProduct { Price = 400, Category = Menu1Category2SubCategory6, Name = "Рис дикий с травами ", Description = "(Рис дикий,лепестки тимьяна,сливки,масло сливочное)", Weight = "150гр", Activity = Activity.Active };
            db.RestProducts.Add(Menu1Category2SubCategory6SubSubCategory1SubProduct1);
            var Menu1Category2SubCategory6SubSubCategory2SubProduct2 = new RestProduct { Price = 400, Category = Menu1Category2SubCategory6, Name = "Пюре картофельное", Description = "(Картофель,молоко,масло сливочное)", Weight = "150гр", Activity = Activity.Active };
            db.RestProducts.Add(Menu1Category2SubCategory6SubSubCategory2SubProduct2);
            var Menu1Category2SubCategory6SubSubCategory3SubProduct3 = new RestProduct { Price = 400, Category = Menu1Category2SubCategory6, Name = "Картофель фри", Description = "(Жареный картофель,трюфельное масло)", Weight = "150гр", Activity = Activity.Active };
            db.RestProducts.Add(Menu1Category2SubCategory6SubSubCategory3SubProduct3);
            db.SaveChanges();

        }
        public void AddBar()
        {
            var restaurant = db.RestPoints.Find(1);
            //бар
            var Menu2 = new RestCategory { RestPoint = restaurant, Name = "Бар", Image = "bar.png", Activity = Activity.Active };
            db.RestCategorys.Add(Menu2);
            //шампанское
            var Menu2Category1 = new RestCategory { RestPoint = restaurant, Name = "Шампанское/Champagne", ParentCategory = Menu2, Image = "bar_mini.png", Activity = Activity.Active };
            db.RestCategorys.Add(Menu2Category1);
            var Menu2Category1Product1 = new RestProduct { Price = 15000, Category = Menu2Category1, Name = "Pol Roger Brut Reserve", Description = "Поль Роже Брют Резерв", Weight = "750мл", Activity = Activity.Active };
            db.RestProducts.Add(Menu2Category1Product1);
            var Menu2Category1Product2 = new RestProduct { Price = 17000, Category = Menu2Category1, Name = "Moet&Chandon Brut Imperial Rose", Description = "Моёт и Шандон Брют Империал Розе", Weight = "750мл", Activity = Activity.Active };
            db.RestProducts.Add(Menu2Category1Product2);
            var Menu2Category1Product3 = new RestProduct { Price = 14000, Category = Menu2Category1, Name = "Moet&Chandon Nectar Imperial", Description = "Моёт и Шандон Нектар Империал (полусухое)", Weight = "750мл", Activity = Activity.Active };
            db.RestProducts.Add(Menu2Category1Product3);
            var Menu2Category1Product4 = new RestProduct { Price = 12500, Category = Menu2Category1, Name = "Moet&Chandon Brut Imperial", Description = "Моёт и Шандон Брют Империал", Weight = "750мл", Activity = Activity.Active };
            db.RestProducts.Add(Menu2Category1Product4);
            //игристые вина
            var Menu2Category2 = new RestCategory { RestPoint = restaurant, Name = "Игристые вина /Sparkling wines", ParentCategory = Menu2, Image = "bar_mini.png", Activity = Activity.Active };
            db.RestCategorys.Add(Menu2Category2);
            var Menu2Category2Product1 = new RestProduct { Price = 800, Category = Menu2Category2, Name = "Victor Pinot Rose Brut Contarini . Italy", Description = "Виктор Пино Розе Брют Контарини . Италия", Weight = "150мл/750мл", Activity = Activity.Active };
            db.RestProducts.Add(Menu2Category2Product1);
            var Menu2Category2Product2 = new RestProduct { Price = 800, Category = Menu2Category2, Name = "Prosecco Spumante Botter . Italy", Description = "Просекко Спуманте Боттер . Италия", Weight = "150мл/750мл", Activity = Activity.Active };
            db.RestProducts.Add(Menu2Category2Product2);
            var Menu2Category2Product3 = new RestProduct { Price = 700, Category = Menu2Category2, Name = "Il Mossiere Prosecco Extra Dray . Italy", Description = "Иль Моссьере Просекко Экстра Драй . Италия", Weight = "150мл/750мл", Activity = Activity.Active };
            db.RestProducts.Add(Menu2Category2Product3);
            var Menu2Category2Product4 = new RestProduct { Price = 700, Category = Menu2Category2, Name = "Il Mossiere Asti . Italy", Description = "Иль Моссьере Асти", Weight = "150мл/750мл", Activity = Activity.Active };
            db.RestProducts.Add(Menu2Category2Product4);
            //белые вина
            var Menu2Category3 = new RestCategory { RestPoint = restaurant, Name = "Белые вина/White Wine", ParentCategory = Menu2, Image = "bar_mini.png", Activity = Activity.Active };
            db.RestCategorys.Add(Menu2Category3);
            //вина по бокалам
            var Menu2Category3SubCategory1 = new RestCategory { RestPoint = restaurant, Name = "Вина по бакалам/Wine by Glass", ParentCategory = Menu2Category3, Image = "bar_mini.png", Activity = Activity.Active };
            db.RestCategorys.Add(Menu2Category3SubCategory1);
            var Menu2Category3SubCategory1SubSubCategory1SubProduct1 = new RestProduct { Price = 1200, Category = Menu2Category3SubCategory1, Name = "Petit Chablis Domaine Laroche . France", Description = "Пти Шабли Домен Ларош . Франция", Weight = "150мл/750мл", Activity = Activity.Active };
            db.RestProducts.Add(Menu2Category3SubCategory1SubSubCategory1SubProduct1);
            var Menu2Category3SubCategory1SubSubCategory2SubProduct2 = new RestProduct { Price = 800, Category = Menu2Category3SubCategory1, Name = "Fioris Pinot Grigio . Italy", Description = "Фьорис Пино Гриджио . Италия", Weight = "150мл/750мл", Activity = Activity.Active };
            db.RestProducts.Add(Menu2Category3SubCategory1SubSubCategory2SubProduct2);
            //франция
            var Menu2Category3SubCategory2 = new RestCategory { RestPoint = restaurant, Name = "Франция/France", ParentCategory = Menu2Category3, Image = "bar_mini.png", Activity = Activity.Active };
            db.RestCategorys.Add(Menu2Category3SubCategory2);
            //эльзас
            var Menu2Category3SubCategory2SubSubCategory1SubSubProduct1 = new RestProduct { Price = 7500, Category = Menu2Category3SubCategory2, Name = "Gewurztraminer Signature 2012", Description = "Гевюрцтраминер Синьятюр (полусухое) 2012", Weight = "750мл", Activity = Activity.Active };
            db.RestProducts.Add(Menu2Category3SubCategory2SubSubCategory1SubSubProduct1);
            var Menu2Category3SubCategory2SubSubCategory1SubSubProduct2 = new RestProduct { Price = 7500, Category = Menu2Category3SubCategory2, Name = "Riesling Signature 2011", Description = "Рислинг Синьятюр (полусухое) 2011", Weight = "750мл", Activity = Activity.Active };
            db.RestProducts.Add(Menu2Category3SubCategory2SubSubCategory1SubSubProduct2);
            //долина лауры
            var Menu2Category3SubCategory2SubSubCategory2SubSubProduct1 = new RestProduct { Price = 7500, Category = Menu2Category3SubCategory2, Name = "Sanсerre Blanc Les Caillottes Jean-Max Roger", Description = "Сансер Блан Ле Кайотт Жан-Макс Роже 2009", Weight = "750мл", Activity = Activity.Active };
            db.RestProducts.Add(Menu2Category3SubCategory2SubSubCategory2SubSubProduct1);
            //бургундия и шабли
            var Menu2Category3SubCategory2SubSubCategory3SubSubProduct1 = new RestProduct { Price = 10000, Category = Menu2Category3SubCategory2, Name = "Chablis 1-er Cru La Chantrerie", Description = "Шабли Премье Крю Ля Шантрери 2012", Weight = "750мл", Activity = Activity.Active };
            db.RestProducts.Add(Menu2Category3SubCategory2SubSubCategory3SubSubProduct1);
            var Menu2Category3SubCategory2SubSubCategory3SubSubProduct2 = new RestProduct { Price = 13000, Category = Menu2Category3SubCategory2, Name = "Meursault Domaine Bitouzet - Prieur", Description = "Мерсо Домэн Битузе-Прийо 2011", Weight = "750мл", Activity = Activity.Active };
            db.RestProducts.Add(Menu2Category3SubCategory2SubSubCategory3SubSubProduct2);
            //долина роны
            var Menu2Category3SubCategory2SubSubCategory4SubSubProduct1 = new RestProduct { Price = 5500, Category = Menu2Category3SubCategory2, Name = "Cotes-du-Rhone Blanc. E.Guigal", Description = "Кот-дю-Рон Блан. Е.Гигаль 2012", Weight = "750мл", Activity = Activity.Active };
            db.RestProducts.Add(Menu2Category3SubCategory2SubSubCategory4SubSubProduct1);
            //гасконь
            var Menu2Category3SubCategory2SubSubCategory5SubSubProduct1 = new RestProduct { Price = 4000, Category = Menu2Category3SubCategory2, Name = "Sauvignon Tariquet", Description = "Совиньон Тарике 2013", Weight = "750мл", Activity = Activity.Active };
            db.RestProducts.Add(Menu2Category3SubCategory2SubSubCategory5SubSubProduct1);
            //италия
            var Menu2Category3SubCategory3 = new RestCategory { RestPoint = restaurant, Name = "Италия/Italy", ParentCategory = Menu2Category3, Image = "bar_mini.png", Activity = Activity.Active };
            db.RestCategorys.Add(Menu2Category3SubCategory3);
            //пьемонт
            var Menu2Category3SubCategory3SubSubCategory1SubSubProduct1 = new RestProduct { Price = 4000, Category = Menu2Category3SubCategory3, Name = "Di Luccio Gavi di Gavi Di Vi Vine", Description = "Ди Люччио Гави ди Гави Ди Ви Вайн 2012", Weight = "750мл", Activity = Activity.Active };
            db.RestProducts.Add(Menu2Category3SubCategory3SubSubCategory1SubSubProduct1);
            var Menu2Category3SubCategory3SubSubCategory1SubSubProduct2 = new RestProduct { Price = 8500, Category = Menu2Category3SubCategory3, Name = "Albino Rocca Chardonnay", Description = "Альбино Рокко Шардоне 2012", Weight = "750мл", Activity = Activity.Active };
            db.RestProducts.Add(Menu2Category3SubCategory3SubSubCategory1SubSubProduct2);
            //альто-адидже
            var Menu2Category3SubCategory3SubSubCategory2SubSubProduct1 = new RestProduct { Price = 5500, Category = Menu2Category3SubCategory3, Name = "Pinot Grigio Alto-Adige Tramin", Description = "Пино Гриджио Альто-Адидже Трамин 2012", Weight = "750мл", Activity = Activity.Active };
            db.RestProducts.Add(Menu2Category3SubCategory3SubSubCategory2SubSubProduct1);
            //венето
            var Menu2Category3SubCategory3SubSubCategory3SubSubProduct1 = new RestProduct { Price = 4500, Category = Menu2Category3SubCategory3, Name = "Soave Carnevale di Venezia DOC", Description = "Соаве Карневале ди Венеция 2011", Weight = "750мл", Activity = Activity.Active };
            db.RestProducts.Add(Menu2Category3SubCategory3SubSubCategory3SubSubProduct1);
            //тоскана
            var Menu2Category3SubCategory3SubSubCategory4SubSubProduct1 = new RestProduct { Price = 7000, Category = Menu2Category3SubCategory3, Name = "Vermentino Bolgheri Ceralti", Description = "Верментино Болгери Чералти 2011", Weight = "750мл", Activity = Activity.Active };
            db.RestProducts.Add(Menu2Category3SubCategory3SubSubCategory4SubSubProduct1);
            //сицилия
            var Menu2Category3SubCategory3SubSubCategory5SubSubProduct1 = new RestProduct { Price = 4500, Category = Menu2Category3SubCategory3, Name = "Alambra Bianco Azienda Agricola Spadafora", Description = "Аламбра Бьянко Альзиенда Агрикола Спадофора 2011", Weight = "750мл", Activity = Activity.Active };
            db.RestProducts.Add(Menu2Category3SubCategory3SubSubCategory5SubSubProduct1);
            //южная африка
            //розовое вино
            var Menu2Category4 = new RestCategory { RestPoint = restaurant, Name = "Розовое Вино/Rose Wine", ParentCategory = Menu2, Image = "bar_mini.png", Activity = Activity.Active };
            db.RestCategorys.Add(Menu2Category4);
            var Menu2Category4Product1 = new RestProduct { Price = 600, Category = Menu2Category4, Name = "Syrah Rose Georges Duboeuf . France", Description = "Сира Розе Жорж Дюбеф. Франция", Weight = "150мл/750мл", Activity = Activity.Active };
            db.RestProducts.Add(Menu2Category4Product1);
            var Menu2Category4Product2 = new RestProduct { Price = 7000, Category = Menu2Category4, Name = "Tavel E.Guigal. France", Description = "Тавель Гигаль. Франция 2012", Weight = "750мл", Activity = Activity.Active };
            db.RestProducts.Add(Menu2Category4Product2);
            //красные вина
            var Menu2Category5 = new RestCategory { RestPoint = restaurant, Name = "Красные вина/Red Wine", ParentCategory = Menu2, Image = "bar_mini.png", Activity = Activity.Active };
            db.RestCategorys.Add(Menu2Category5);
            //вина по бокалам
            var Menu2Category5SubCategory1 = new RestCategory { RestPoint = restaurant, Name = "Вина по бакалам/Wine by Glass", ParentCategory = Menu2Category5, Image = "bar_mini.png", Activity = Activity.Active };
            db.RestCategorys.Add(Menu2Category5SubCategory1);
            var Menu2Category5SubCategory1SubSubCategory1SubProduct1 = new RestProduct { Price = 1100, Category = Menu2Category5SubCategory1, Name = "Cote du Rhone Rouge E. Guigal. France", Description = "Кот дю Рон Руж Е. Гигаль. Франция", Weight = "150мл/750мл", Activity = Activity.Active };
            db.RestProducts.Add(Menu2Category5SubCategory1SubSubCategory1SubProduct1);
            var Menu2Category5SubCategory1SubSubCategory2SubProduct2 = new RestProduct { Price = 800, Category = Menu2Category5SubCategory1, Name = "Chianti Vecchia Cantina di Montepulciano. Italy", Description = "Кьянти Веккия Кантина ди Монтепульчано. Италия ", Weight = "150мл/750мл", Activity = Activity.Active };
            db.RestProducts.Add(Menu2Category5SubCategory1SubSubCategory2SubProduct2);
            var Menu2Category5SubCategory1SubSubCategory3SubProduct3 = new RestProduct { Price = 1000, Category = Menu2Category5SubCategory1, Name = "Marques de Caceres Crianza. Spain", Description = "Маркес де Касерес Крианса . Испания ", Weight = "150мл/750мл", Activity = Activity.Active };
            db.RestProducts.Add(Menu2Category5SubCategory1SubSubCategory3SubProduct3);
            var Menu2Category5SubCategory1SubSubCategory4SubProduct4 = new RestProduct { Price = 900, Category = Menu2Category5SubCategory1, Name = "Escudo Rojo Baron Philippe de Rothschild. Chile", Description = "Эскудо Рохо Барон Филип де Ротшильд. Чили", Weight = "150мл/750мл", Activity = Activity.Active };
            db.RestProducts.Add(Menu2Category5SubCategory1SubSubCategory4SubProduct4);
            //франция
            var Menu2Category5SubCategory2 = new RestCategory { RestPoint = restaurant, Name = "Франция/France", ParentCategory = Menu2Category5, Image = "bar_mini.png", Activity = Activity.Active };
            db.RestCategorys.Add(Menu2Category5SubCategory2);
            //бургундия
            var Menu2Category5SubCategory2SubSubCategory1SubSubProduct1 = new RestProduct { Price = 11000, Category = Menu2Category5SubCategory2, Name = "Volnay Domaine Bitouzet-Prieur", Description = "Вольне Домен Битузэ-Прийо 2010", Weight = "750мл", Activity = Activity.Active };
            db.RestProducts.Add(Menu2Category5SubCategory2SubSubCategory1SubSubProduct1);
            var Menu2Category5SubCategory2SubSubCategory1SubSubProduct2 = new RestProduct { Price = 6500, Category = Menu2Category5SubCategory2, Name = "Bourgogne Rouge Domaine des Vercheres", Description = "Бургонь Руж Домен де Вершер 2009", Weight = "750мл", Activity = Activity.Active };
            db.RestProducts.Add(Menu2Category5SubCategory2SubSubCategory1SubSubProduct2);
            //долина роны
            var Menu2Category5SubCategory2SubSubCategory2SubSubProduct1 = new RestProduct { Price = 9000, Category = Menu2Category5SubCategory2, Name = "Crozes-Hermitage Rouge E.Guigal", Description = "Кроз-Эрмитаж Руж Е.Гигаль 2008", Weight = "750мл", Activity = Activity.Active };
            db.RestProducts.Add(Menu2Category5SubCategory2SubSubCategory2SubSubProduct1);
            var Menu2Category5SubCategory2SubSubCategory2SubSubProduct2 = new RestProduct { Price = 18000, Category = Menu2Category5SubCategory2, Name = "Chateauneuf-du-Pape Roug E.Guigal", Description = "Шатонёф-дю-Пап Руж Е.Гигаль 2004", Weight = "750мл", Activity = Activity.Active };
            db.RestProducts.Add(Menu2Category5SubCategory2SubSubCategory2SubSubProduct2);
            //бордо
            var Menu2Category5SubCategory2SubSubCategory3SubSubProduct1 = new RestProduct { Price = 4000, Category = Menu2Category5SubCategory2, Name = "Chateau Martignan Medoc", Description = "Шато Мартиньян Медок 2012", Weight = "750мл", Activity = Activity.Active };
            db.RestProducts.Add(Menu2Category5SubCategory2SubSubCategory3SubSubProduct1);
            var Menu2Category5SubCategory2SubSubCategory3SubSubProduct2 = new RestProduct { Price = 12000, Category = Menu2Category5SubCategory2, Name = "Chateau Lalande Cru Bourgeois", Description = "Шато Лаланд Крю Буржуа 2007", Weight = "750мл", Activity = Activity.Active };
            db.RestProducts.Add(Menu2Category5SubCategory2SubSubCategory3SubSubProduct2);
            var Menu2Category5SubCategory2SubSubCategory3SubSubProduct3 = new RestProduct { Price = 9500, Category = Menu2Category5SubCategory2, Name = "Chateau Citran Cru Bourgeois Haut-Medoc", Description = "Шато Ситран Крю Буржуа О-Медок 2008", Weight = "750мл", Activity = Activity.Active };
            db.RestProducts.Add(Menu2Category5SubCategory2SubSubCategory3SubSubProduct3);
            var Menu2Category5SubCategory2SubSubCategory3SubSubProduct4 = new RestProduct { Price = 10000, Category = Menu2Category5SubCategory2, Name = "Chateau Gaillard Grand Cru Saint Emilion", Description = "Шато Гайар Гран Крю Сент Эмильон 2007", Weight = "750мл", Activity = Activity.Active };
            db.RestProducts.Add(Menu2Category5SubCategory2SubSubCategory3SubSubProduct4);
            //италия
            var Menu2Category5SubCategory3 = new RestCategory { RestPoint = restaurant, Name = "Италия/Italy", ParentCategory = Menu2Category5, Image = "bar_mini.png", Activity = Activity.Active };
            db.RestCategorys.Add(Menu2Category5SubCategory3);
            //пьемонт
            var Menu2Category5SubCategory3SubSubCategory1SubSubProduct1 = new RestProduct { Price = 6000, Category = Menu2Category5SubCategory3, Name = "Barbera d’Alba Gianfranco Alessandria", Description = "Барбера д’Альба Джанфранко Алессадриа 2012", Weight = "750мл", Activity = Activity.Active };
            db.RestProducts.Add(Menu2Category5SubCategory3SubSubCategory1SubSubProduct1);
            var Menu2Category5SubCategory3SubSubCategory1SubSubProduct2 = new RestProduct { Price = 14000, Category = Menu2Category5SubCategory3, Name = "Barolo Gianfranco Alessandria", Description = "Бароло Джанфранко Алессадриа 2009", Weight = "750мл", Activity = Activity.Active };
            db.RestProducts.Add(Menu2Category5SubCategory3SubSubCategory1SubSubProduct2);
            //венето
            var Menu2Category5SubCategory3SubSubCategory2SubSubProduct1 = new RestProduct { Price = 5500, Category = Menu2Category5SubCategory3, Name = "Valpolicella Classico Stefano Accordini", Description = "Вальполичелла Классико Стефано Аккордини 2013", Weight = "750мл", Activity = Activity.Active };
            db.RestProducts.Add(Menu2Category5SubCategory3SubSubCategory2SubSubProduct1);
            //тоскана
            var Menu2Category5SubCategory3SubSubCategory3SubSubProduct1 = new RestProduct { Price = 5000, Category = Menu2Category5SubCategory3, Name = "Volano Il Molino di Grace", Description = "Волано Иль Молино ди Грейс 2008", Weight = "750мл", Activity = Activity.Active };
            db.RestProducts.Add(Menu2Category5SubCategory3SubSubCategory3SubSubProduct1);
            var Menu2Category5SubCategory3SubSubCategory3SubSubProduct2 = new RestProduct { Price = 10500, Category = Menu2Category5SubCategory3, Name = "Chianti Classico Riserva Il Molino di Grace", Description = "Кьянти Классико Резерва Иль Молино ди Грейс 2010", Weight = "750мл", Activity = Activity.Active };
            db.RestProducts.Add(Menu2Category5SubCategory3SubSubCategory3SubSubProduct2);
            //пьемонт
            var Menu2Category5SubCategory3SubSubCategory4SubSubProduct1 = new RestProduct { Price = 15000, Category = Menu2Category5SubCategory3, Name = "Barbaresco Albino Rocca DOCG", Description = "Барбареско Альбино Рокка ДОКГ 2010", Weight = "750мл", Activity = Activity.Active };
            db.RestProducts.Add(Menu2Category5SubCategory3SubSubCategory4SubSubProduct1);
            //испания
            var Menu2Category5SubCategory4 = new RestCategory { RestPoint = restaurant, Name = "Испания/Spain", ParentCategory = Menu2Category5, Image = "bar_mini.png", Activity = Activity.Active };
            db.RestCategorys.Add(Menu2Category5SubCategory4);
            //риоха
            var Menu2Category5SubCategory4SubSubCategory1SubSubProduct1 = new RestProduct { Price = 10000, Category = Menu2Category5SubCategory4, Name = "Protos Crianza Bodegas", Description = "Протос Крианса Бодегас 2010", Weight = "750мл", Activity = Activity.Active };
            db.RestProducts.Add(Menu2Category5SubCategory4SubSubCategory1SubSubProduct1);
            //австралия
            var Menu2Category5SubCategory5 = new RestCategory { RestPoint = restaurant, Name = "Австралия/Australia", ParentCategory = Menu2Category5, Image = "bar_mini.png", Activity = Activity.Active };
            db.RestCategorys.Add(Menu2Category5SubCategory5);
            var Menu2Category5SubCategory5SubSubCategory1SubProduct1 = new RestProduct { Price = 3500, Category = Menu2Category5SubCategory5, Name = "Oxford Landing Shiraz", Description = "Оксфорд Лэндинг Шираз 2012", Weight = "750мл", Activity = Activity.Active };
            db.RestProducts.Add(Menu2Category5SubCategory5SubSubCategory1SubProduct1);
            var Menu2Category5SubCategory5SubSubCategory2SubProduct2 = new RestProduct { Price = 6500, Category = Menu2Category5SubCategory5, Name = "Bush Vine Grenache Yalumba", Description = "Буш Вайн Гренаш Яламба 2012", Weight = "750мл", Activity = Activity.Active };
            db.RestProducts.Add(Menu2Category5SubCategory5SubSubCategory2SubProduct2);
            //коньяк
            var Menu2Category6 = new RestCategory { RestPoint = restaurant, Name = "Коньяк", ParentCategory = Menu2, Image = "bar_mini.png", Activity = Activity.Active };
            db.RestCategorys.Add(Menu2Category6);
            var Menu2Category6Product1 = new RestProduct { Price = 1150, Category = Menu2Category6, Name = "Martell VSOP", Weight = "50мл", Activity = Activity.Active };
            db.RestProducts.Add(Menu2Category6Product1);
            var Menu2Category6Product2 = new RestProduct { Price = 3400, Category = Menu2Category6, Name = "Martell ХО", Weight = "50мл", Activity = Activity.Active };
            db.RestProducts.Add(Menu2Category6Product2);
            var Menu2Category6Product3 = new RestProduct { Price = 1150, Category = Menu2Category6, Name = "Remy Martin VSOP", Weight = "50мл", Activity = Activity.Active };
            db.RestProducts.Add(Menu2Category6Product3);
            //арманьяк
            var Menu2Category7 = new RestCategory { RestPoint = restaurant, Name = "Арманьяк", ParentCategory = Menu2, Image = "bar_mini.png", Activity = Activity.Active };
            db.RestCategorys.Add(Menu2Category7);
            var Menu2Category7Product2 = new RestProduct { Price = 1250, Category = Menu2Category7, Name = "Janneau ХО", Weight = "50мл", Activity = Activity.Active };
            db.RestProducts.Add(Menu2Category7Product2);
            //кальвадос
            var Menu2Category8 = new RestCategory { RestPoint = restaurant, Name = "Кальвадос", ParentCategory = Menu2, Image = "bar_mini.png", Activity = Activity.Active };
            db.RestCategorys.Add(Menu2Category8);
            var Menu2Category8Product1 = new RestProduct { Price = 1500, Category = Menu2Category8, Name = "Pere Magloire VSOP", Weight = "50мл", Activity = Activity.Active };
            db.RestProducts.Add(Menu2Category8Product1);
            //виски
            var Menu2Category10 = new RestCategory { RestPoint = restaurant, Name = "Виски", ParentCategory = Menu2, Image = "bar_mini.png", Activity = Activity.Active };
            db.RestCategorys.Add(Menu2Category10);
            //купажированный
            var Menu2Category10SubCategory2 = new RestCategory { RestPoint = restaurant, Name = "Шотландский Купажированный Виски", ParentCategory = Menu2Category10, Image = "bar_mini.png", Activity = Activity.Active };
            db.RestCategorys.Add(Menu2Category10SubCategory2);
            var Menu2Category10SubCategory2SubSubCategory1SubProduct1 = new RestProduct { Price = 700, Category = Menu2Category10SubCategory2, Name = "Chivas Regal 12 y.o.", Weight = "50мл", Activity = Activity.Active };
            db.RestProducts.Add(Menu2Category10SubCategory2SubSubCategory1SubProduct1);
            var Menu2Category10SubCategory2SubSubCategory2SubProduct2 = new RestProduct { Price = 1300, Category = Menu2Category10SubCategory2, Name = "Chivas Regal 18 y.o.", Weight = "50мл", Activity = Activity.Active };
            db.RestProducts.Add(Menu2Category10SubCategory2SubSubCategory2SubProduct2);
            var Menu2Category10SubCategory2SubSubCategory3SubProduct3 = new RestProduct { Price = 3500, Category = Menu2Category10SubCategory2, Name = "Chivas Regal Royal Salute 21 y.o.", Weight = "50мл", Activity = Activity.Active };
            db.RestProducts.Add(Menu2Category10SubCategory2SubSubCategory3SubProduct3);
            var Menu2Category10SubCategory2SubSubCategory4SubProduct4 = new RestProduct { Price = 600, Category = Menu2Category10SubCategory2, Name = "William Lawson’s Super Spiced", Weight = "50мл", Activity = Activity.Active };
            db.RestProducts.Add(Menu2Category10SubCategory2SubSubCategory4SubProduct4);
            //джонни
            var Menu2Category10SubCategory3SubSubCategory1SubProduct1 = new RestProduct { Price = 800, Category = Menu2Category10SubCategory2, Name = "Johnnie Walker Black Label", Weight = "50мл", Activity = Activity.Active };
            db.RestProducts.Add(Menu2Category10SubCategory3SubSubCategory1SubProduct1);
            var Menu2Category10SubCategory3SubSubCategory2SubProduct2 = new RestProduct { Price = 1100, Category = Menu2Category10SubCategory2, Name = "Johnnie Walker Gold Label", Weight = "50мл", Activity = Activity.Active };
            db.RestProducts.Add(Menu2Category10SubCategory3SubSubCategory2SubProduct2);
            var Menu2Category10SubCategory3SubSubCategory3SubProduct3 = new RestProduct { Price = 2600, Category = Menu2Category10SubCategory2, Name = "Johnnie Walker Blue Label", Weight = "50мл", Activity = Activity.Active };
            db.RestProducts.Add(Menu2Category10SubCategory3SubSubCategory3SubProduct3);
            //шотландский
            var Menu2Category10SubCategory1 = new RestCategory { RestPoint = restaurant, Name = "Шотландский Солодовый Виски", ParentCategory = Menu2Category10, Image = "bar_mini.png", Activity = Activity.Active };
            db.RestCategorys.Add(Menu2Category10SubCategory1);
            //айла
            var Menu2Category10SubCategory1SubSubCategory1SubSubProduct1 = new RestProduct { Price = 1000, Category = Menu2Category10SubCategory1, Name = "Lagavulin 16 y.o.", Weight = "50мл", Activity = Activity.Active };
            db.RestProducts.Add(Menu2Category10SubCategory1SubSubCategory1SubSubProduct1);
            //лоулендс
            var Menu2Category10SubCategory1SubSubCategory2SubSubProduct1 = new RestProduct { Price = 1100, Category = Menu2Category10SubCategory1, Name = "Glenkinchie 12 y.o.", Weight = "50мл", Activity = Activity.Active };
            db.RestProducts.Add(Menu2Category10SubCategory1SubSubCategory2SubSubProduct1);
            //хайлендс
            var Menu2Category10SubCategory1SubSubCategory3SubSubProduct1 = new RestProduct { Price = 1100, Category = Menu2Category10SubCategory1, Name = "Aberfeldy 12 y.o.", Weight = "50мл", Activity = Activity.Active };
            db.RestProducts.Add(Menu2Category10SubCategory1SubSubCategory3SubSubProduct1);
            //спейсайд
            var Menu2Category10SubCategory1SubSubCategory4SubSubProduct1 = new RestProduct { Price = 1100, Category = Menu2Category10SubCategory1, Name = "Macallan 12 y.o.", Weight = "50мл", Activity = Activity.Active };
            db.RestProducts.Add(Menu2Category10SubCategory1SubSubCategory4SubSubProduct1);
            //ирландия
            var Menu2Category10SubCategory4 = new RestCategory { RestPoint = restaurant, Name = "Ирландия", ParentCategory = Menu2Category10, Image = "bar_mini.png", Activity = Activity.Active };
            db.RestCategorys.Add(Menu2Category10SubCategory4);
            var Menu2Category10SubCategory4SubSubCategory1SubProduct1 = new RestProduct { Price = 750, Category = Menu2Category10SubCategory4, Name = "Jameson 12 years Limited Reserve", Weight = "50мл", Activity = Activity.Active };
            db.RestProducts.Add(Menu2Category10SubCategory4SubSubCategory1SubProduct1);
            //америка
            var Menu2Category10SubCategory6 = new RestCategory { RestPoint = restaurant, Name = "Америка", ParentCategory = Menu2Category10, Image = "bar_mini.png", Activity = Activity.Active };
            db.RestCategorys.Add(Menu2Category10SubCategory6);
            //теннесси
            var Menu2Category10SubCategory6SubSubCategory1SubSubProduct1 = new RestProduct { Price = 600, Category = Menu2Category10SubCategory6, Name = "Jack Daniel’s", Weight = "50мл", Activity = Activity.Active };
            db.RestProducts.Add(Menu2Category10SubCategory6SubSubCategory1SubSubProduct1);
            var Menu2Category10SubCategory6SubSubCategory1SubSubProduct2 = new RestProduct { Price = 1100, Category = Menu2Category10SubCategory6, Name = "Jack Daniel’s Single Barrel", Weight = "50мл", Activity = Activity.Active };
            db.RestProducts.Add(Menu2Category10SubCategory6SubSubCategory1SubSubProduct2);
            //кентукки
            var Menu2Category10SubCategory6SubSubCategory2SubSubProduct1 = new RestProduct { Price = 800, Category = Menu2Category10SubCategory6, Name = "Woodford Reserve", Weight = "50мл", Activity = Activity.Active };
            db.RestProducts.Add(Menu2Category10SubCategory6SubSubCategory2SubSubProduct1);
            var Menu2Category10SubCategory6SubSubCategory2SubSubProduct2 = new RestProduct { Price = 750, Category = Menu2Category10SubCategory6, Name = "Maker’s Mark", Weight = "50мл", Activity = Activity.Active };
            db.RestProducts.Add(Menu2Category10SubCategory6SubSubCategory2SubSubProduct2);
            //ром
            var Menu2Category12 = new RestCategory { RestPoint = restaurant, Name = "Ром", ParentCategory = Menu2, Image = "bar_mini.png", Activity = Activity.Active };
            db.RestCategorys.Add(Menu2Category12);
            //куба
            var Menu2Category12SubCategory1SubSubCategory1SubProduct1 = new RestProduct { Price = 550, Category = Menu2Category12, Name = "BACARDI Carta Blanca", Weight = "50мл", Activity = Activity.Active };
            db.RestProducts.Add(Menu2Category12SubCategory1SubSubCategory1SubProduct1);
            var Menu2Category12SubCategory1SubSubCategory2SubProduct2 = new RestProduct { Price = 650, Category = Menu2Category12, Name = "BACARDI Carta Oro", Weight = "50мл", Activity = Activity.Active };
            db.RestProducts.Add(Menu2Category12SubCategory1SubSubCategory2SubProduct2);
            //чачача
            var Menu2Category12SubCategory5 = new RestCategory { RestPoint = restaurant, Name = "Cachaca", ParentCategory = Menu2, Image = "bar_mini.png", Activity = Activity.Active };
            db.RestCategorys.Add(Menu2Category12SubCategory5);
            var Menu2Category12SubCategory5SubSubCategory1SubProduct1 = new RestProduct { Price = 500, Category = Menu2Category12SubCategory5, Name = "Terra Brazilis Cachaca", Weight = "50мл", Activity = Activity.Active };
            db.RestProducts.Add(Menu2Category12SubCategory5SubSubCategory1SubProduct1);
            //текила
            var Menu2Category13 = new RestCategory { RestPoint = restaurant, Name = "Текила", ParentCategory = Menu2, Image = "bar_mini.png", Activity = Activity.Active };
            db.RestCategorys.Add(Menu2Category13);
            var Menu2Category13Product1 = new RestProduct { Price = 520, Category = Menu2Category13, Name = "Camino Real Blanco", Weight = "50мл", Activity = Activity.Active };
            db.RestProducts.Add(Menu2Category13Product1);
            var Menu2Category13Product2 = new RestProduct { Price = 520, Category = Menu2Category13, Name = "Camino Real Gold", Weight = "50мл", Activity = Activity.Active };
            db.RestProducts.Add(Menu2Category13Product2);
            var Menu2Category13Product3 = new RestProduct { Price = 600, Category = Menu2Category13, Name = "Cazadores Blanco", Weight = "50мл", Activity = Activity.Active };
            db.RestProducts.Add(Menu2Category13Product3);
            //джин
            var Menu2Category14 = new RestCategory { RestPoint = restaurant, Name = "Джин", ParentCategory = Menu2, Image = "bar_mini.png", Activity = Activity.Active };
            db.RestCategorys.Add(Menu2Category14);
            var Menu2Category14Product1 = new RestProduct { Price = 500, Category = Menu2Category14, Name = "Beefeater", Weight = "50мл", Activity = Activity.Active };
            db.RestProducts.Add(Menu2Category14Product1);
            var Menu2Category14Product2 = new RestProduct { Price = 600, Category = Menu2Category14, Name = "Bombay Sapphire", Weight = "50мл", Activity = Activity.Active };
            db.RestProducts.Add(Menu2Category14Product2);
            //граппа
            var Menu2Category9 = new RestCategory { RestPoint = restaurant, Name = "Граппа", ParentCategory = Menu2, Image = "bar_mini.png", Activity = Activity.Active };
            db.RestCategorys.Add(Menu2Category9);
            var Menu2Category9Product1 = new RestProduct { Price = 650, Category = Menu2Category9, Name = "Grappa Euganea Luxardo", Weight = "50мл", Activity = Activity.Active };
            db.RestProducts.Add(Menu2Category9Product1);
            //вермуты
            var Menu2Category15 = new RestCategory { RestPoint = restaurant, Name = "Вермуты", ParentCategory = Menu2, Image = "bar_mini.png", Activity = Activity.Active };
            db.RestCategorys.Add(Menu2Category15);
            var Menu2Category15Product1 = new RestProduct { Price = 450, Category = Menu2Category15, Name = "Martini Extra Dry", Weight = "100мл", Activity = Activity.Active };
            db.RestProducts.Add(Menu2Category15Product1);
            var Menu2Category15Product2 = new RestProduct { Price = 450, Category = Menu2Category15, Name = "Martini Bianco", Weight = "100мл", Activity = Activity.Active };
            db.RestProducts.Add(Menu2Category15Product2);
            var Menu2Category15Product3 = new RestProduct { Price = 450, Category = Menu2Category15, Name = "Martini Rosso", Weight = "100мл", Activity = Activity.Active };
            db.RestProducts.Add(Menu2Category15Product3);
            //аперитивы
            var Menu2Category16 = new RestCategory { RestPoint = restaurant, Name = "Аперитивы и Биттеры", ParentCategory = Menu2, Image = "bar_mini.png", Activity = Activity.Active };
            db.RestCategorys.Add(Menu2Category16);
            var Menu2Category16Product1 = new RestProduct { Price = 400, Category = Menu2Category16, Name = "Aperol", Weight = "50мл", Activity = Activity.Active };
            db.RestProducts.Add(Menu2Category16Product1);
            var Menu2Category16Product2 = new RestProduct { Price = 400, Category = Menu2Category16, Name = "Ricard", Weight = "50мл", Activity = Activity.Active };
            db.RestProducts.Add(Menu2Category16Product2);
            var Menu2Category16Product3 = new RestProduct { Price = 400, Category = Menu2Category16, Name = "Becherovka", Weight = "50мл", Activity = Activity.Active };
            db.RestProducts.Add(Menu2Category16Product3);
            var Menu2Category16Product4 = new RestProduct { Price = 400, Category = Menu2Category16, Name = "Campari", Weight = "50мл", Activity = Activity.Active };
            db.RestProducts.Add(Menu2Category16Product4);
            //марочные ликеры
            var Menu2Category17 = new RestCategory { RestPoint = restaurant, Name = "Ликеры", ParentCategory = Menu2, Image = "bar_mini.png", Activity = Activity.Active };
            db.RestCategorys.Add(Menu2Category17);
            var Menu2Category17Product1 = new RestProduct { Price = 400, Category = Menu2Category17, Name = "Kahlua", Weight = "50мл", Activity = Activity.Active };
            db.RestProducts.Add(Menu2Category17Product1);
            var Menu2Category17Product2 = new RestProduct { Price = 400, Category = Menu2Category17, Name = "Baileys", Weight = "50мл", Activity = Activity.Active };
            db.RestProducts.Add(Menu2Category17Product2);
            var Menu2Category17Product3 = new RestProduct { Price = 400, Category = Menu2Category17, Name = "Cointreau", Weight = "50мл", Activity = Activity.Active };
            db.RestProducts.Add(Menu2Category17Product3);
            var Menu2Category17Product4 = new RestProduct { Price = 400, Category = Menu2Category17, Name = "Malibu", Weight = "50мл", Activity = Activity.Active };
            db.RestProducts.Add(Menu2Category17Product4);
            //ликеры
            var Menu2Category18Product1 = new RestProduct { Price = 400, Category = Menu2Category17, Name = "Maraschino", Weight = "50мл", Activity = Activity.Active };
            db.RestProducts.Add(Menu2Category18Product1);
            var Menu2Category18Product2 = new RestProduct { Price = 400, Category = Menu2Category17, Name = "Blue Curacao", Weight = "50мл", Activity = Activity.Active };
            db.RestProducts.Add(Menu2Category18Product2);
            var Menu2Category18Product3 = new RestProduct { Price = 400, Category = Menu2Category17, Name = "Melon", Weight = "50мл", Activity = Activity.Active };
            db.RestProducts.Add(Menu2Category18Product3);
            //водка
            var Menu2Category11 = new RestCategory { RestPoint = restaurant, Name = "Водка", ParentCategory = Menu2, Image = "bar_mini.png", Activity = Activity.Active };
            db.RestCategorys.Add(Menu2Category11);
            //россия
            var Menu2Category11SubCategory1SubSubCategory2SubProduct2 = new RestProduct { Price = 600, Category = Menu2Category11, Name = "Beluga", Weight = "50мл", Activity = Activity.Active };
            db.RestProducts.Add(Menu2Category11SubCategory1SubSubCategory2SubProduct2);
            //финляндия
            var Menu2Category11SubCategory2SubSubCategory1SubProduct1 = new RestProduct { Price = 500, Category = Menu2Category11, Name = "Finlandia", Weight = "50мл", Activity = Activity.Active };
            db.RestProducts.Add(Menu2Category11SubCategory2SubSubCategory1SubProduct1);
            var Menu2Category11SubCategory2SubSubCategory2SubProduct2 = new RestProduct { Price = 500, Category = Menu2Category11, Name = "Finlandia Cranberry", Weight = "50мл", Activity = Activity.Active };
            db.RestProducts.Add(Menu2Category11SubCategory2SubSubCategory2SubProduct2);
            //франция
            var Menu2Category11SubCategory3SubSubCategory1SubProduct1 = new RestProduct { Price = 550, Category = Menu2Category11, Name = "Eristoff", Weight = "50мл", Activity = Activity.Active };
            db.RestProducts.Add(Menu2Category11SubCategory3SubSubCategory1SubProduct1);
            var Menu2Category11SubCategory3SubSubCategory2SubProduct2 = new RestProduct { Price = 600, Category = Menu2Category11, Name = "Grey Goose", Weight = "50мл", Activity = Activity.Active };
            db.RestProducts.Add(Menu2Category11SubCategory3SubSubCategory2SubProduct2);
            //пиво
            var Menu2Category19 = new RestCategory { RestPoint = restaurant, Name = "Пиво", ParentCategory = Menu2, Image = "bar_mini.png", Activity = Activity.Active };
            db.RestCategorys.Add(Menu2Category19);
            var Menu2Category19Product1 = new RestProduct { Price = 450, Category = Menu2Category19, Name = "Corona Extra", Weight = "330мл", Activity = Activity.Active };
            db.RestProducts.Add(Menu2Category19Product1);
            var Menu2Category19Product2 = new RestProduct { Price = 450, Category = Menu2Category19, Name = "Leffe Blond", Description = "Леффе Светлое", Weight = "330мл", Activity = Activity.Active };
            db.RestProducts.Add(Menu2Category19Product2);
            //коктейли
            var Menu2Category22 = new RestCategory { RestPoint = restaurant, Name = "Cocktails", ParentCategory = Menu2, Image = "bar_mini.png", Activity = Activity.Active };
            db.RestCategorys.Add(Menu2Category22);
            //бакарди
            var Menu2Category22SubCategory5 = new RestCategory { RestPoint = restaurant, Name = "Bacardi cocktails", ParentCategory = Menu2Category22, Image = "bar_mini.png", Activity = Activity.Active };
            db.RestCategorys.Add(Menu2Category22SubCategory5);
            var Menu2Category22SubCategory5SubSubCategory1SubProduct1 = new RestProduct { Price = 750, Category = Menu2Category22SubCategory5, Name = "БАКАРДИ Куба Либре / BACARDI Cuba Libre", Activity = Activity.Active };
            db.RestProducts.Add(Menu2Category22SubCategory5SubSubCategory1SubProduct1);
            var Menu2Category22SubCategory5SubSubCategory2SubProduct2 = new RestProduct { Price = 900, Category = Menu2Category22SubCategory5, Name = "БАКАРДИ Оакхарт-Кола / BACARDI Oakheart-Cola", Activity = Activity.Active };
            db.RestProducts.Add(Menu2Category22SubCategory5SubSubCategory2SubProduct2);
            //мартини
            var Menu2Category22SubCategory6 = new RestCategory { RestPoint = restaurant, Name = "Martini cocktails", ParentCategory = Menu2Category22, Image = "bar_mini.png", Activity = Activity.Active };
            db.RestCategorys.Add(Menu2Category22SubCategory5);
            var Menu2Category22SubCategory6SubSubCategory1SubProduct1 = new RestProduct { Price = 900, Category = Menu2Category22SubCategory6, Name = "МАРТИНИ Рояль / MARTINI Royale", Activity = Activity.Active };
            db.RestProducts.Add(Menu2Category22SubCategory6SubSubCategory1SubProduct1);
            var Menu2Category22SubCategory6SubSubCategory2SubProduct2 = new RestProduct { Price = 800, Category = Menu2Category22SubCategory6, Name = "МАРТИНИ-Тоник / MARTINI-Tonic", Activity = Activity.Active };
            db.RestProducts.Add(Menu2Category22SubCategory6SubSubCategory2SubProduct2);
            //шутеры
            var Menu2Category22SubCategory1 = new RestCategory { RestPoint = restaurant, Name = "Шутеры/shooters", ParentCategory = Menu2Category22, Image = "bar_mini.png", Activity = Activity.Active };
            db.RestCategorys.Add(Menu2Category22SubCategory1);
            var Menu2Category22SubCategory1SubSubCategory1SubProduct1 = new RestProduct { Price = 400, Category = Menu2Category22SubCategory1, Name = "Б-52/B-52", Description = "(калуа,бейлиз,куантро)", Activity = Activity.Active };
            db.RestProducts.Add(Menu2Category22SubCategory1SubSubCategory1SubProduct1);
            var Menu2Category22SubCategory1SubSubCategory2SubProduct2 = new RestProduct { Price = 400, Category = Menu2Category22SubCategory1, Name = "Б-53/B-53", Description = "(калуа,бейлиз,абсент)", Activity = Activity.Active };
            db.RestProducts.Add(Menu2Category22SubCategory1SubSubCategory2SubProduct2);
            var Menu2Category22SubCategory1SubSubCategory3SubProduct3 = new RestProduct { Price = 450, Category = Menu2Category22SubCategory1, Name = "Хиросима/Hirosima", Description = "(самбука,бейлиз,абсент,гренадин)", Activity = Activity.Active };
            db.RestProducts.Add(Menu2Category22SubCategory1SubSubCategory3SubProduct3);
            var Menu2Category22SubCategory1SubSubCategory4SubProduct4 = new RestProduct { Price = 400, Category = Menu2Category22SubCategory1, Name = "Тирамису/Tirаmisu", Description = "(калуа,касис,бейлиз,сливки)", Activity = Activity.Active };
            db.RestProducts.Add(Menu2Category22SubCategory1SubSubCategory4SubProduct4);
            var Menu2Category22SubCategory1SubSubCategory5SubProduct5 = new RestProduct { Price = 450, Category = Menu2Category22SubCategory1, Name = "Опухоль Мозга/Brain tumors", Description = "(самбука,мартини,бейлиз,гренадин)", Activity = Activity.Active };
            db.RestProducts.Add(Menu2Category22SubCategory1SubSubCategory5SubProduct5);
            //шорт
            var Menu2Category22SubCategory2 = new RestCategory { RestPoint = restaurant, Name = "Шорт Дринкс/short Drinks", ParentCategory = Menu2Category22, Image = "bar_mini.png", Activity = Activity.Active };
            db.RestCategorys.Add(Menu2Category22SubCategory2);
            var Menu2Category22SubCategory2SubSubCategory1SubProduct1 = new RestProduct { Price = 600, Category = Menu2Category22SubCategory2, Name = "Яблочный Тини/Apple Tini", Description = "(водка,сироп,кальвадос,яблоко,сок лимона)", Activity = Activity.Active };
            db.RestProducts.Add(Menu2Category22SubCategory2SubSubCategory1SubProduct1);
            var Menu2Category22SubCategory2SubSubCategory2SubProduct2 = new RestProduct { Price = 600, Category = Menu2Category22SubCategory2, Name = "Тихий Тини/Tiny Pacific", Description = "(сок лимона,руккола,сироп,водка)", Activity = Activity.Active };
            db.RestProducts.Add(Menu2Category22SubCategory2SubSubCategory2SubProduct2);
            var Menu2Category22SubCategory2SubSubCategory3SubProduct3 = new RestProduct { Price = 600, Category = Menu2Category22SubCategory2, Name = "Май Тай/Mai Tai", Description = "(сок лимона,три вида рома,амаретто,куантро,сироп кокос)", Activity = Activity.Active };
            db.RestProducts.Add(Menu2Category22SubCategory2SubSubCategory3SubProduct3);
            var Menu2Category22SubCategory2SubSubCategory4SubProduct4 = new RestProduct { Price = 700, Category = Menu2Category22SubCategory2, Name = "Смоки Сауэр/Smokey Sauer", Description = "(сок лимона,белок,пюре маракуя,сироп,виски островной)", Activity = Activity.Active };
            db.RestProducts.Add(Menu2Category22SubCategory2SubSubCategory4SubProduct4);
            var Menu2Category22SubCategory2SubSubCategory5SubProduct5 = new RestProduct { Price = 600, Category = Menu2Category22SubCategory2, Name = "Ванила Айс/Vanilla ice", Description = "(виноград,сок лимона,водка,сироп)", Activity = Activity.Active };
            db.RestProducts.Add(Menu2Category22SubCategory2SubSubCategory5SubProduct5);
            var Menu2Category22SubCategory2SubSubCategory6SubProduct6 = new RestProduct { Price = 600, Category = Menu2Category22SubCategory2, Name = "Дайкири/Daiquiri", Description = "(ром,сироп,сок лайма,мараскино)", Activity = Activity.Active };
            db.RestProducts.Add(Menu2Category22SubCategory2SubSubCategory6SubProduct6);
            var Menu2Category22SubCategory2SubSubCategory7SubProduct7 = new RestProduct { Price = 650, Category = Menu2Category22SubCategory2, Name = "Клубничный Дайкири/Strawberry Daiquiri", Description = "(клубника,ром,сок лайма,сироп)", Activity = Activity.Active };
            db.RestProducts.Add(Menu2Category22SubCategory2SubSubCategory7SubProduct7);
            var Menu2Category22SubCategory2SubSubCategory8SubProduct8 = new RestProduct { Price = 600, Category = Menu2Category22SubCategory2, Name = "Белая Леди/White Lady", Description = "(джин,белок,сок лимона,куантро,сироп)", Activity = Activity.Active };
            db.RestProducts.Add(Menu2Category22SubCategory2SubSubCategory8SubProduct8);
            var Menu2Category22SubCategory2SubSubCategory9SubProduct9 = new RestProduct { Price = 600, Category = Menu2Category22SubCategory2, Name = "Оргазм/Orgasm", Description = "(бейлиз,гранд марньер,мараскино)", Activity = Activity.Active };
            db.RestProducts.Add(Menu2Category22SubCategory2SubSubCategory9SubProduct9);
            var Menu2Category22SubCategory2SubSubCategory10SubProduct10 = new RestProduct { Price = 600, Category = Menu2Category22SubCategory2, Name = "Французский Связной/The French Connection", Description = "(коньяк,амарето,мараскино)", Activity = Activity.Active };
            db.RestProducts.Add(Menu2Category22SubCategory2SubSubCategory10SubProduct10);
            var Menu2Category22SubCategory2SubSubCategory11SubProduct11 = new RestProduct { Price = 600, Category = Menu2Category22SubCategory2, Name = "Александр/Alexander", Description = "(коньяк,сливки,калуа)", Activity = Activity.Active };
            db.RestProducts.Add(Menu2Category22SubCategory2SubSubCategory11SubProduct11);
            var Menu2Category22SubCategory2SubSubCategory12SubProduct12 = new RestProduct { Price = 600, Category = Menu2Category22SubCategory2, Name = "Мартинез/Martinez", Description = "(Джин,мартини россо,мараскино,ангостура)", Activity = Activity.Active };
            db.RestProducts.Add(Menu2Category22SubCategory2SubSubCategory12SubProduct12);
            //лонг дрнкс
            var Menu2Category22SubCategory3 = new RestCategory { RestPoint = restaurant, Name = "Лонг Дринкс/Long Drinks", ParentCategory = Menu2Category22, Image = "bar_mini.png", Activity = Activity.Active };
            db.RestCategorys.Add(Menu2Category22SubCategory3);
            var Menu2Category22SubCategory3SubSubCategory1SubProduct1 = new RestProduct { Price = 550, Category = Menu2Category22SubCategory3, Name = "Ху из Ху/Who is who", Description = "(водка,пюре маракуя,сок лимона,сироп,имбирный эль)", Activity = Activity.Active };
            db.RestProducts.Add(Menu2Category22SubCategory3SubSubCategory1SubProduct1);
            var Menu2Category22SubCategory3SubSubCategory2SubProduct2 = new RestProduct { Price = 600, Category = Menu2Category22SubCategory3, Name = "Секс на Пляже/Sex on the beach", Description = "(водка,персиковый ликер,сироп,морс,ананасовый и апельсиновый соки,сок лимона)", Activity = Activity.Active };
            db.RestProducts.Add(Menu2Category22SubCategory3SubSubCategory2SubProduct2);
            var Menu2Category22SubCategory3SubSubCategory3SubProduct3 = new RestProduct { Price = 900, Category = Menu2Category22SubCategory3, Name = "Мохито/Mojito", Description = "(ром,мята,лайм,сиров,содовая)", Activity = Activity.Active };
            db.RestProducts.Add(Menu2Category22SubCategory3SubSubCategory3SubProduct3);
            var Menu2Category22SubCategory3SubSubCategory4SubProduct4 = new RestProduct { Price = 1000, Category = Menu2Category22SubCategory3, Name = "Клубничный Мохито/Strawberry Mojito", Description = "(ром,мята,клубника,сироп,содовая,лайм)", Activity = Activity.Active };
            db.RestProducts.Add(Menu2Category22SubCategory3SubSubCategory4SubProduct4);
            var Menu2Category22SubCategory3SubSubCategory5SubProduct5 = new RestProduct { Price = 600, Category = Menu2Category22SubCategory3, Name = "Линчбург Лимонад/Lynchburg Lemonade", Description = "(джек дениэлс,сироп,сок лимона,имбирный эль)", Activity = Activity.Active };
            db.RestProducts.Add(Menu2Category22SubCategory3SubSubCategory5SubProduct5);
            var Menu2Category22SubCategory3SubSubCategory6SubProduct6 = new RestProduct { Price = 950, Category = Menu2Category22SubCategory3, Name = "Лонг Айленд Айс Ти/Long Island Ice Tea", Description = "(водка,текила,джин,куантро,ром,сок лимона,кола)", Activity = Activity.Active };
            db.RestProducts.Add(Menu2Category22SubCategory3SubSubCategory6SubProduct6);
            var Menu2Category22SubCategory3SubSubCategory7SubProduct7 = new RestProduct { Price = 550, Category = Menu2Category22SubCategory3, Name = "Московский Мул/Moscow Mule", Description = "(водка,сок лимона,сироп,куантро,имбирный эль)", Activity = Activity.Active };
            db.RestProducts.Add(Menu2Category22SubCategory3SubSubCategory7SubProduct7);
            //на основе игристого
            var Menu2Category22SubCategory4 = new RestCategory { RestPoint = restaurant, Name = "Коктейли на основе игристого/Based cocktails sparkling", ParentCategory = Menu2Category22, Image = "bar_mini.png", Activity = Activity.Active };
            db.RestCategorys.Add(Menu2Category22SubCategory4);
            var Menu2Category22SubCategory4SubSubCategory1SubProduct1 = new RestProduct { Price = 900, Category = Menu2Category22SubCategory4, Name = "Кир Роял/Kir Royal", Description = "(игристое вино,касис)", Activity = Activity.Active };
            db.RestProducts.Add(Menu2Category22SubCategory4SubSubCategory1SubProduct1);
            var Menu2Category22SubCategory4SubSubCategory2SubProduct2 = new RestProduct { Price = 900, Category = Menu2Category22SubCategory4, Name = "Мартини Роял/Martini Royale", Description = "(игристое вино,мартини бьянко,лайм,мята)", Activity = Activity.Active };
            db.RestProducts.Add(Menu2Category22SubCategory4SubSubCategory2SubProduct2);
            var Menu2Category22SubCategory4SubSubCategory3SubProduct3 = new RestProduct { Price = 950, Category = Menu2Category22SubCategory4, Name = "Апероль Спритц/Aperol spritz", Description = "(игристое,апероль)", Activity = Activity.Active };
            db.RestProducts.Add(Menu2Category22SubCategory4SubSubCategory3SubProduct3);
            //горячие коктейли
            var Menu2Category22SubCategory7 = new RestCategory { RestPoint = restaurant, Name = "Горячие Коктейли/Hot Cocktails", ParentCategory = Menu2Category22, Image = "bar_mini.png", Activity = Activity.Active };
            db.RestCategorys.Add(Menu2Category22SubCategory7);
            var Menu2Category22SubCategory7SubSubCategory1SubProduct1 = new RestProduct { Price = 600, Category = Menu2Category22SubCategory7, Name = "Вечерний Нектар/Evening Nectar", Description = "(водка,гранд мариньер,мед,сок лимона,апельсиновый сок,чай)", Activity = Activity.Active };
            db.RestProducts.Add(Menu2Category22SubCategory7SubSubCategory1SubProduct1);
            var Menu2Category22SubCategory7SubSubCategory2SubProduct2 = new RestProduct { Price = 600, Category = Menu2Category22SubCategory7, Name = "Грог/Grog", Description = "(ром,фрукты,сироп,чай,специи)", Activity = Activity.Active };
            db.RestProducts.Add(Menu2Category22SubCategory7SubSubCategory2SubProduct2);
            var Menu2Category22SubCategory7SubSubCategory3SubProduct3 = new RestProduct { Price = 600, Category = Menu2Category22SubCategory7, Name = "Теннесси Хот Тодди/Hot Toddy Tennessee", Description = "(джек дениэлс,сироп,сок лимона,чай,специи)", Activity = Activity.Active };
            db.RestProducts.Add(Menu2Category22SubCategory7SubSubCategory3SubProduct3);
            var Menu2Category22SubCategory7SubSubCategory4SubProduct4 = new RestProduct { Price = 600, Category = Menu2Category22SubCategory7, Name = "Глинтвейн/Mulled Wine", Description = "(вино,фрукты,мёд,специи)", Activity = Activity.Active };
            db.RestProducts.Add(Menu2Category22SubCategory7SubSubCategory4SubProduct4);
            //безалкогольные коктейли
            var Menu2Category22SubCategory8 = new RestCategory { RestPoint = restaurant, Name = "Безалкогольные Коктейли/Non-alcoholic cocktails", ParentCategory = Menu2Category22, Image = "bar_mini.png", Activity = Activity.Active };
            db.RestCategorys.Add(Menu2Category22SubCategory8);
            var Menu2Category22SubCategory8SubSubCategory1SubProduct1 = new RestProduct { Price = 500, Category = Menu2Category22SubCategory8, Name = "Мохито б/а/Mojito b/a", Description = "(мята,лайм,сироп,содовая)", Activity = Activity.Active };
            db.RestProducts.Add(Menu2Category22SubCategory8SubSubCategory1SubProduct1);
            var Menu2Category22SubCategory8SubSubCategory2SubProduct2 = new RestProduct { Price = 600, Category = Menu2Category22SubCategory8, Name = "Клубничный Мохито б/а/Strawberry Mojito b/a", Description = "(клубника,мята,лайм,сироп,содовая)", Activity = Activity.Active };
            db.RestProducts.Add(Menu2Category22SubCategory8SubSubCategory2SubProduct2);
            var Menu2Category22SubCategory8SubSubCategory3SubProduct3 = new RestProduct { Price = 500, Category = Menu2Category22SubCategory8, Name = "Ху Ис Ху б/а/Who is Who b/a", Description = "(пюре маракуя,сок лимона,сироп,имбирный эль)", Activity = Activity.Active };
            db.RestProducts.Add(Menu2Category22SubCategory8SubSubCategory3SubProduct3);
            var Menu2Category22SubCategory8SubSubCategory4SubProduct4 = new RestProduct { Price = 500, Category = Menu2Category22SubCategory8, Name = "Клубничный Лимонад/Strawberry Lemonade", Description = "(сироп,клубника,сок лимона,имбирный эль)", Activity = Activity.Active };
            db.RestProducts.Add(Menu2Category22SubCategory8SubSubCategory4SubProduct4);
            var Menu2Category22SubCategory8SubSubCategory5SubProduct5 = new RestProduct { Price = 500, Category = Menu2Category22SubCategory8, Name = "Лимонный Лимонад/Lemon Lemonade", Description = "(сок лимона,сироп,имбирный эль)", Activity = Activity.Active };
            db.RestProducts.Add(Menu2Category22SubCategory8SubSubCategory5SubProduct5);
            //свежевыжатиые соки
            var Menu2Category23 = new RestCategory { RestPoint = restaurant, Name = "Свежевыжатые соки", ParentCategory = Menu2, Image = "bar_mini.png", Activity = Activity.Active };
            db.RestCategorys.Add(Menu2Category23);
            var Menu2Category23SubCategory1SubProduct1 = new RestProduct { Price = 500, Category = Menu2Category23, Name = "Ананасовый", Weight = "200мл", Activity = Activity.Active };
            db.RestProducts.Add(Menu2Category23SubCategory1SubProduct1);
            var Menu2Category23SubCategory2SubProduct2 = new RestProduct { Price = 300, Category = Menu2Category23, Name = "Морковный", Weight = "200мл", Activity = Activity.Active };
            db.RestProducts.Add(Menu2Category23SubCategory2SubProduct2);
            var Menu2Category23SubCategory3SubProduct3 = new RestProduct { Price = 400, Category = Menu2Category23, Name = "Сельдереевый", Weight = "200мл", Activity = Activity.Active };
            db.RestProducts.Add(Menu2Category23SubCategory3SubProduct3);
            var Menu2Category23SubCategory4SubProduct4 = new RestProduct { Price = 350, Category = Menu2Category23, Name = "Яблочный", Weight = "200мл", Activity = Activity.Active };
            db.RestProducts.Add(Menu2Category23SubCategory4SubProduct4);
            //безалкоголка
            var Menu2Category20 = new RestCategory { RestPoint = restaurant, Name = "Безалкогольные напитки", ParentCategory = Menu2, Image = "bar_mini.png", Activity = Activity.Active };
            db.RestCategorys.Add(Menu2Category20);
            //соки
            var Menu2Category20SubCategory4 = new RestCategory { RestPoint = restaurant, Name = "Соки и нектары Pago", ParentCategory = Menu2Category20, Image = "bar_mini.png", Activity = Activity.Active };
            db.RestCategorys.Add(Menu2Category20SubCategory4);
            var Menu2Category20SubCategory4SubSubCategory1SubProduct1 = new RestProduct { Price = 250, Category = Menu2Category20SubCategory4, Name = "Orange juice (апельсиновый)", Weight = "200мл", Activity = Activity.Active };
            db.RestProducts.Add(Menu2Category20SubCategory4SubSubCategory1SubProduct1);
            var Menu2Category20SubCategory4SubSubCategory2SubProduct2 = new RestProduct { Price = 250, Category = Menu2Category20SubCategory4, Name = "Apple juice (яблочный)", Weight = "200мл", Activity = Activity.Active };
            db.RestProducts.Add(Menu2Category20SubCategory4SubSubCategory2SubProduct2);
            var Menu2Category20SubCategory4SubSubCategory3SubProduct3 = new RestProduct { Price = 250, Category = Menu2Category20SubCategory4, Name = "Tomato juice (томатный)", Weight = "200мл", Activity = Activity.Active };
            db.RestProducts.Add(Menu2Category20SubCategory4SubSubCategory3SubProduct3);
            //газировка
            var Menu2Category20SubCategory2 = new RestCategory { RestPoint = restaurant, Name = "Газированные напитки", ParentCategory = Menu2Category20, Image = "bar_mini.png", Activity = Activity.Active };
            db.RestCategorys.Add(Menu2Category20SubCategory2);
            var Menu2Category20SubCategory2SubSubCategory1SubProduct1 = new RestProduct { Price = 200, Category = Menu2Category20SubCategory2, Name = "Coca-Cola", Weight = "250мл", Activity = Activity.Active };
            db.RestProducts.Add(Menu2Category20SubCategory2SubSubCategory1SubProduct1);
            var Menu2Category20SubCategory2SubSubCategory2SubProduct2 = new RestProduct { Price = 200, Category = Menu2Category20SubCategory2, Name = "Ginger Ale", Weight = "250мл", Activity = Activity.Active };
            db.RestProducts.Add(Menu2Category20SubCategory2SubSubCategory2SubProduct2);
            var Menu2Category20SubCategory2SubSubCategory3SubProduct3 = new RestProduct { Price = 200, Category = Menu2Category20SubCategory2, Name = "Schweppes Bitter-Lemon", Weight = "250мл", Activity = Activity.Active };
            db.RestProducts.Add(Menu2Category20SubCategory2SubSubCategory3SubProduct3);
            var Menu2Category20SubCategory2SubSubCategory4SubProduct4 = new RestProduct { Price = 200, Category = Menu2Category20SubCategory2, Name = "Sprite", Weight = "250мл", Activity = Activity.Active };
            db.RestProducts.Add(Menu2Category20SubCategory2SubSubCategory4SubProduct4);
            //вода
            var Menu2Category20SubCategory1 = new RestCategory { RestPoint = restaurant, Name = "Вода", ParentCategory = Menu2Category20, Image = "bar_mini.png", Activity = Activity.Active };
            db.RestCategorys.Add(Menu2Category20SubCategory1);
            var Menu2Category20SubCategory1SubSubCategory1SubProduct1 = new RestProduct { Price = 320, Category = Menu2Category20SubCategory1, Name = "Goccia di Carnia Mineral Still Water in glass bottle (б/г)", Weight = "250мл/750мл", Activity = Activity.Active };
            db.RestProducts.Add(Menu2Category20SubCategory1SubSubCategory1SubProduct1);
            var Menu2Category20SubCategory1SubSubCategory2SubProduct2 = new RestProduct { Price = 320, Category = Menu2Category20SubCategory1, Name = "Goccia di Carnia Mineral Sparkling Water in glass bottle (с/г)", Weight = "250мл/750мл", Activity = Activity.Active };
            db.RestProducts.Add(Menu2Category20SubCategory1SubSubCategory2SubProduct2);
            var Menu2Category20SubCategory1SubSubCategory3SubProduct3 = new RestProduct { Price = 320, Category = Menu2Category20SubCategory1, Name = "Etrusca Still Water in glass bottle (б/г)", Weight = "250мл/750мл", Activity = Activity.Active };
            db.RestProducts.Add(Menu2Category20SubCategory1SubSubCategory3SubProduct3);
            var Menu2Category20SubCategory1SubSubCategory4SubProduct4 = new RestProduct { Price = 320, Category = Menu2Category20SubCategory1, Name = "Etrusca Still Water in glass bottle (с/г)", Weight = "250мл/750мл", Activity = Activity.Active };
            db.RestProducts.Add(Menu2Category20SubCategory1SubSubCategory4SubProduct4);
            //энергетики
            var Menu2Category20SubCategory3 = new RestCategory { RestPoint = restaurant, Name = "Энергетические напитки", ParentCategory = Menu2Category20, Image = "bar_mini.png", Activity = Activity.Active };
            db.RestCategorys.Add(Menu2Category20SubCategory3);
            var Menu2Category20SubCategory3SubSubCategory1SubProduct1 = new RestProduct { Price = 250, Category = Menu2Category20SubCategory3, Name = "Effect", Weight = "250мл", Activity = Activity.Active };
            db.RestProducts.Add(Menu2Category20SubCategory3SubSubCategory1SubProduct1);
            //горячие напитки
            var Menu2Category21 = new RestCategory { RestPoint = restaurant, Name = "Горячие Напитки", ParentCategory = Menu2, Image = "bar_mini.png", Activity = Activity.Active };
            db.RestCategorys.Add(Menu2Category21);
            //чаи
            var Menu2Category21SubCategory1 = new RestCategory { RestPoint = restaurant, Name = "Чай", ParentCategory = Menu2Category21, Image = "bar_mini.png", Activity = Activity.Active };
            db.RestCategorys.Add(Menu2Category21SubCategory1);
            var Menu2Category21SubCategory1SubSubCategory1SubProduct1 = new RestProduct { Price = 500, Category = Menu2Category21SubCategory1, Name = "Асам", Weight = "500мл", Activity = Activity.Active };
            db.RestProducts.Add(Menu2Category21SubCategory1SubSubCategory1SubProduct1);
            var Menu2Category21SubCategory1SubSubCategory2SubProduct2 = new RestProduct { Price = 500, Category = Menu2Category21SubCategory1, Name = "ЭрлГрей", Weight = "500мл", Activity = Activity.Active };
            db.RestProducts.Add(Menu2Category21SubCategory1SubSubCategory2SubProduct2);
            var Menu2Category21SubCategory1SubSubCategory3SubProduct3 = new RestProduct { Price = 500, Category = Menu2Category21SubCategory1, Name = "Сенча", Weight = "500мл", Activity = Activity.Active };
            db.RestProducts.Add(Menu2Category21SubCategory1SubSubCategory3SubProduct3);
            var Menu2Category21SubCategory1SubSubCategory4SubProduct4 = new RestProduct { Price = 500, Category = Menu2Category21SubCategory1, Name = "Жасминовая Жемчужина", Weight = "500мл", Activity = Activity.Active };
            db.RestProducts.Add(Menu2Category21SubCategory1SubSubCategory4SubProduct4);
            var Menu2Category21SubCategory1SubSubCategory5SubProduct5 = new RestProduct { Price = 500, Category = Menu2Category21SubCategory1, Name = "Молочный улун", Weight = "500мл", Activity = Activity.Active };
            db.RestProducts.Add(Menu2Category21SubCategory1SubSubCategory5SubProduct5);
            //кофе
            var Menu2Category21SubCategory2 = new RestCategory { RestPoint = restaurant, Name = "Кофе", ParentCategory = Menu2Category21, Image = "bar_mini.png", Activity = Activity.Active };
            db.RestCategorys.Add(Menu2Category21SubCategory2);
            var Menu2Category21SubCategory2SubSubCategory1SubProduct1 = new RestProduct { Price = 150, Category = Menu2Category21SubCategory2, Name = "Ристретто", Activity = Activity.Active };
            db.RestProducts.Add(Menu2Category21SubCategory2SubSubCategory1SubProduct1);
            var Menu2Category21SubCategory2SubSubCategory2SubProduct2 = new RestProduct { Price = 150, Category = Menu2Category21SubCategory2, Name = "Эспрессо", Activity = Activity.Active };
            db.RestProducts.Add(Menu2Category21SubCategory2SubSubCategory2SubProduct2);
            //ягодный чай
            var Menu2Category21SubCategory3 = new RestCategory { RestPoint = restaurant, Name = "Ягодный чай", ParentCategory = Menu2Category21, Image = "bar_mini.png", Activity = Activity.Active };
            db.RestCategorys.Add(Menu2Category21SubCategory3);
            var Menu2Category21SubCategory3SubSubCategory8SubProduct8 = new RestProduct { Price = 600, Category = Menu2Category21SubCategory3, Name = "Лесные Ягоды", Description = "(собрание свежих лесных сезонных ягод в соседстве с облепихой)", Weight = "500мл", Activity = Activity.Active };
            db.RestProducts.Add(Menu2Category21SubCategory3SubSubCategory8SubProduct8);
            var Menu2Category21SubCategory3SubSubCategory9SubProduct9 = new RestProduct { Price = 600, Category = Menu2Category21SubCategory3, Name = "Облепиховый", Weight = "500мл", Activity = Activity.Active };
            db.RestProducts.Add(Menu2Category21SubCategory3SubSubCategory9SubProduct9);
            var Menu2Category21SubCategory3SubSubCategory10SubProduct10 = new RestProduct { Price = 600, Category = Menu2Category21SubCategory3, Name = "Ежевичный", Weight = "500мл", Activity = Activity.Active };
            db.RestProducts.Add(Menu2Category21SubCategory3SubSubCategory10SubProduct10);
            //сигареты
            var Menu2Category24 = new RestCategory { RestPoint = restaurant, Name = "Сигаретный набор", ParentCategory = Menu2, Image = "bar_mini.png", Activity = Activity.Active };
            db.RestCategorys.Add(Menu2Category24);
            var Menu2Category24SubCategory1SubProduct1 = new RestProduct { Price = 200, Category = Menu2Category24, Name = "Парламент Найт", Activity = Activity.Active };
            db.RestProducts.Add(Menu2Category24SubCategory1SubProduct1);
            var Menu2Category24SubCategory1SubProduct2 = new RestProduct { Price = 200, Category = Menu2Category24, Name = "Парламент Сильвер", Activity = Activity.Active };
            db.RestProducts.Add(Menu2Category24SubCategory1SubProduct2);
            var Menu2Category24SubCategory1SubProduct3 = new RestProduct { Price = 200, Category = Menu2Category24, Name = "Парламент Аква", Activity = Activity.Active };
            db.RestProducts.Add(Menu2Category24SubCategory1SubProduct3);
            var Menu2Category24SubCategory1SubProduct4 = new RestProduct { Price = 200, Category = Menu2Category24, Name = "Парламент Супер Слимс", Activity = Activity.Active };
            db.RestProducts.Add(Menu2Category24SubCategory1SubProduct4);
            var Menu2Category24SubCategory2SubProduct1 = new RestProduct { Price = 200, Category = Menu2Category24, Name = "Кент 1", Activity = Activity.Active };
            db.RestProducts.Add(Menu2Category24SubCategory2SubProduct1);
            db.SaveChanges();

        }
        public void AddChief()
        {
            var restaurant = db.RestPoints.Find(1);
            var Menu3 = new RestCategory { RestPoint = restaurant, Name = "Меню от Шефа", Image = "chief.png", Activity = Activity.Active };
            db.RestCategorys.Add(Menu3);
            var Menu3Product1 = new RestProduct { Price = 15000, Category = Menu3, Name = "Дорада, запечённая с пряными травами, с гарниром из овощей с сливочным соусом", Description = "К блюдам из дорадо подают белые сухие вина.<br/>Калорийность дорадо составляет 96 ккал на 100 грамм продукта<br/>Рыбка не только красива и вкусна, она полна витаминами, Дорадо – это настоящий победитель среди себе подобных по высокому содержанию йода.", Weight = "750гр.", Activity = Activity.Active };
            db.RestProducts.Add(Menu3Product1);
            var Menu3Product2 = new RestProduct { Price = 17000, Category = Menu3, Name = "Каре ягненка", Description = "Нежное сочное мясо ягненка с пряным гранатовым соусом. На гарнир овощи на гриле.. Прекрасное блюдо для романтического ужина с красным вином.<br/>Каре ягненка в 100 г содержит всего лишь 191 ккал.", Weight = "750гр.", Activity = Activity.Active };
            db.RestProducts.Add(Menu3Product2);
            var Menu3Product3 = new RestProduct { Price = 14000, Category = Menu3, Name = "«Салат Оливье» по версии ресторана Москва середины 20-х годов 20 века", Description = "Ингредиенты: картофель. Репчатый лук, морковь, маринованный огурец, яблоко, варёное мясо птицы, зелёный горошек, варёное перепелиное яйцо, оливковый майонез, соль, перец по вкусу.", Weight = "750гр.", Activity = Activity.Active };
            db.RestProducts.Add(Menu3Product3);
            var Menu3Product4 = new RestProduct { Price = 12500, Category = Menu3, Name = "Стейк лосося", Description = "Лосось очень нежная, мягкая и ароматная рыба, которая после термообработки сохраняет все свои полезные качества. Рыба хорошо сочетается с белым сухим вином, свежими овощами. Подается с соусами. Спаржей, свежими томатами, дольками лайма,<br />Лосось с гриля, конечно, блюдо калорийное относительно других способов приготовления и составляет 283 ккал на 100 грамм продукта, но его вкус превосходит все ожидания.<br />Состав лосося с гриля богат жирами, аминокислотами и витаминами А, D, Е, фосфором, калием, хлором, кальцием и еще многими микро- и макроэлементами.<br />Дополнительное прекрасное свойство лосося – антидепрессивный эффект. Он полезен людям, страдающим от болезней нервной и сердечной систем, беременным, для оптимального развития плода.", Weight = "750гр.", Activity = Activity.Active };
            db.RestProducts.Add(Menu3Product4);
            var Menu3Product5 = new RestProduct { Price = 14000, Category = Menu3, Name = "Тартар из лосося", Description = "Лосось; маринованные каперсы, огурец, лук шалот, шнитт-лук. Соевый соус. Лимонный сок. Черный перец (молотый); Масло оливковое.<br />Французское блюдо тартар представляет собой закуску из нескольких мелко нарезанных ингредиентов. Заправляются все компоненты тартара соусом. Характерной чертой блюда является то, что все продукты для него используются в свежем, маринованном или слабосоленом виде. Именно поэтому лосось для тартара не подвергается никакой тепловой обработке. Тартар из лосося – это не только вкусно, но еще и очень полезно. Такую низкокалорийную закуску можно смело включать в диетическое меню, угощение отлично подойдет и для праздничного стола.", Weight = "750гр.", Activity = Activity.Active };
            db.RestProducts.Add(Menu3Product5);
            var Menu3Product6 = new RestProduct { Price = 12500, Category = Menu3, Name = "Чизкейк", Description = "Чизкейк, конечно, звучит красиво и модно, но по сути это просто пирог или торт, основной компонент начинки которого – мягкий сыр<br />Сведущие в истории кулинарии люди утверждают, что во времена первых Олимпийских игр, проходивших на острове Делос, раздавали вкусное, сытное и питательное лакомство – чизкейк. Такой десерт был для спортсменов не просто вкусной сладостью, а и бесценным источником энергии.", Weight = "750гр.", Activity = Activity.Active };
            db.RestProducts.Add(Menu3Product6);
            db.SaveChanges();

        }
        public JsonResult CatProd(int categoryId, Guid? sessionId)
        {
            var products = db.RestProducts.Count(p => p.Category.Id == categoryId);

            if (products == 0)
            {
                var categorys2 = db.RestCategorys.Where(c => c.Id == categoryId).Select(c => new
                {
                    Id = c.Id,
                    status = "category",
                    Parent = c.Name,
                    Logo = c.Image,
                    Array = c.SubCategories.Select(sc => new
                    {
                        Id = sc.Id,
                        Name = sc.Name,
                        Activity = sc.Activity,
                        Image = (sc.Image != null) ? (sc.Image) : (sc.ParentCategory.Image)
                    })
                }).SingleOrDefault();

                return Json(categorys2, JsonRequestBehavior.AllowGet);
            }
            if (sessionId.HasValue)
            {
                var session = db.RestAppSessions.SingleOrDefault(s => s.Id == sessionId);
                if (session == null) return Json(false, JsonRequestBehavior.AllowGet);

                var product = db.RestCategorys.Where(c => c.Id == categoryId).Select(c => new
                {
                    status = "product",
                    Parent = c.Name,
                    Logo = c.Image,
                    Array = c.Products.Select(sc => new
                    {
                        Name = sc.Name,
                        Price = sc.Price,
                        Id = sc.Id,
                        Image = (sc.Image != null) ? (sc.Image) : (sc.Category.Image),
                        Description = sc.Description,
                        Weight = sc.Weight,
                        Favorite =
                            (sc.RestProductFavorites.Any(f => f.RestAppUser.Id == session.RestAppUser.Id))
                                ? (true)
                                : (false)
                    })
                }).SingleOrDefault();
                return Json(product, JsonRequestBehavior.AllowGet);
            }
            var product2 = db.RestCategorys.Where(c => c.Id == categoryId).Select(c => new
            {
                status = "product",
                Parent = c.Name,
                Logo = c.Image,
                Array = c.Products.Select(sc => new
                {
                    Name = sc.Name,
                    Price = sc.Price,
                    Id = sc.Id,
                    Image = (sc.Image != null) ? (sc.Image) : (sc.Category.Image),
                    Description = sc.Description,
                    Weight = sc.Weight
                })
            }).SingleOrDefault();

            return Json(product2, JsonRequestBehavior.AllowGet);
        }
        public JsonResult OneProd(int productId, Guid? sessionId)
        {
            if (sessionId.HasValue)
            {
                var session = db.RestAppSessions.SingleOrDefault(s => s.Id == sessionId);
                if (session == null) return Json(false, JsonRequestBehavior.AllowGet);
                var products = db.RestProducts.Where(p => p.Id == productId).Select(c => new
                {
                    Activity = c.Activity,
                    Name = c.Name,
                    Id = c.Id,
                    status = "product",
                    Price = c.Price,
                    Description = c.Description,
                    Weight = c.Weight,
                    Image = (c.Image != null) ? (c.Image) : (c.Category.Image),
                    Parent = c.Category.Name,
                    Favorite = (c.RestProductFavorites.Any(f => f.RestAppUser.Id == session.RestAppUser.Id)) ? (true) : (false)
                });
                return Json(products, JsonRequestBehavior.AllowGet);
            }
            var product = db.RestProducts.Where(p => p.Id == productId).Select(c => new
            {
                Activity = c.Activity,
                Name = c.Name,
                Id = c.Id,
                status = "product",
                Price = c.Price,
                Description = c.Description,
                Weight = c.Weight,
                Image = (c.Image != null) ? (c.Image) : (c.Category.Image),
                Parent = c.Category.Name,
            });
            return Json(product, JsonRequestBehavior.AllowGet);
            
        }
        public JsonResult CatMenu(int restId)
        {
            var categorys = db.RestCategorys.Where(c => c.ParentCategory == null && c.RestPoint.Point.Id == restId).Select(c => new
            {
                Id = c.Id,
                Name = c.Name,
                Activity = c.Activity,
                status = "category",
                Parent = c.Name,
                Image = c.Image
            });
            return Json(categorys, JsonRequestBehavior.AllowGet);
        }
        //public JsonResult SetRating(int prodId, int rating, string comment, Guid sessionId)
        //{
        //    var session = db.RestAppSessions.SingleOrDefault(u => u.Id == sessionId);
        //    if (session == null) return Json(false, JsonRequestBehavior.AllowGet);
        //    var user = session.RestAppUser;
        //    var productReview = db.RestProdReviews.SingleOrDefault(pr => pr.RestAppUser.Id == user.Id && pr.RestProduct.Id == prodId);
        //    if (productReview == null)
        //    {
        //        productReview = new RestProdReview { Date = DateTime.UtcNow, RestProduct = db.RestProducts.Find(prodId), Rating = rating, Comment = comment, RestAppUser = db.RestAppUsers.Find(user.Id) };
        //        db.RestProdReviews.Add(productReview);
        //    }
        //    else
        //    {
        //        productReview.Rating = rating;
        //        productReview.Comment = comment;
        //        productReview.Date = DateTime.UtcNow;
        //        db.Entry(productReview).State = EntityState.Modified;
        //    }
        //    db.SaveChanges();
        //    return null;
        //}
        public JsonResult AddProdToFavorite(int prodId, Guid sessionId)
        {
            var session = db.RestAppSessions.SingleOrDefault(s => s.Id == sessionId);
            if (session == null) return Json(false, JsonRequestBehavior.AllowGet);
            var user = session.RestAppUser;

            var favorite = db.RestProductFavorites.SingleOrDefault(p => p.RestAppUser.Id == user.Id);
            if (favorite == null)
            {
                favorite = new RestProductFavorite { RestAppUser = db.RestAppUsers.Find(user.Id), RestProducts = new List<RestProduct> { db.RestProducts.Find(prodId) } };
                db.RestProductFavorites.Add(favorite);
            }
            else
            {
                favorite.RestProducts.Add(db.RestProducts.Find(prodId));
                db.Entry(favorite).State = EntityState.Modified;
            }
            db.SaveChanges();
            return Json(true, JsonRequestBehavior.AllowGet);
        }
        public JsonResult RemoveProdFromFavorite(int prodId, Guid sessionId)
        {
            var session = db.RestAppSessions.SingleOrDefault(s => s.Id == sessionId);
            if (session == null) return Json(false, JsonRequestBehavior.AllowGet);
            var user = session.RestAppUser;

            var favorite = db.RestProductFavorites.FirstOrDefault(p => p.RestAppUser.Id == user.Id);
            if (favorite == null)
            {
                return Json(false, JsonRequestBehavior.AllowGet);
            }
            favorite.RestProducts.Remove(db.RestProducts.Find(prodId));
            db.Entry(favorite).State = EntityState.Modified;
            db.SaveChanges();
            return Json(true, JsonRequestBehavior.AllowGet);
        }
        public JsonResult GetFavorite(Guid sessionId)
        {
            var session = db.RestAppSessions.SingleOrDefault(s => s.Id == sessionId);
            if (session == null) return Json(false, JsonRequestBehavior.AllowGet);
            var user = session.RestAppUser;

            var favor = db.RestProductFavorites.SingleOrDefault(f => f.RestAppUser.Id == user.Id);
            if (favor == null) return Json(false, JsonRequestBehavior.AllowGet);

            var products = db.RestProducts.Where(p => p.RestProductFavorites.Any(f => f.Id == favor.Id)).Select(sl => new
            {
                Id = sl.Id,
                Name = sl.Name,
                Description = sl.Description,
                Weight = sl.Weight,
                Price = sl.Price,
                Image = (sl.Image != null) ? (sl.Image) : sl.Category.Image
            });
            return Json(products, JsonRequestBehavior.AllowGet);
        }
        //public JsonResult CatProd(int? categoryId, Guid? sessionId, int? pointId)
        //{
        //    if (!categoryId.HasValue)
        //    {
        //        var categorys = db.RestCategorys.Where(c => c.ParentCategory == null && c.RestPoint.Point.Id == pointId).Select(c => new
        //        {
        //            Activity = c.Activity,
        //            status = "category",
        //            Parent = c.Name,
        //            Image = c.Image
        //        });
        //        return Json(categorys, JsonRequestBehavior.AllowGet);
        //    }

        //    var products = db.RestProducts.Count(p => p.Category.Id == categoryId);

        //    if (products == 0)
        //    {
        //        var categorys2 = db.RestCategorys.Where(c => c.Id == categoryId).Select(c => new
        //        {
        //            Activity = c.Activity,
        //            status = "category",
        //            Parent = c.Name,
        //            Image = (c.Image != null) ? (c.Image) : (c.ParentCategory.Image),
        //            Array = c.SubCategories.Select(sc => new
        //            {
        //                Activity = sc.Activity,
        //                Name = sc.Name,
        //                Image = sc.Image,
        //                Id = sc.Id
        //            })
        //        }).SingleOrDefault();

        //        return Json(categorys2, JsonRequestBehavior.AllowGet);
        //    }
        //    if (sessionId.HasValue)
        //    {
        //        var session = db.RestAppSessions.SingleOrDefault(s => s.Id == sessionId);
        //        if (session == null) return Json(false, JsonRequestBehavior.AllowGet);
        //        var product2 = db.RestCategorys.Where(c => c.Id == categoryId).Select(c => new
        //        {
        //            Activity = c.Activity,
        //            status = "product",
        //            Parent = c.Name,
        //            Image = (c.Image != null) ? (c.Image) : (c.ParentCategory.Image),
        //            Array = c.Products.Select(sc => new
        //            {
        //                Activity = sc.Activity,
        //                Name = sc.Name,
        //                Price = sc.Price,
        //                Id = sc.Id,
        //                Image = (sc.Image != null) ? (sc.Image) : (sc.Category.Image),
        //                Description = sc.Description,
        //                Weight = sc.Weight,
        //                Favorite = (sc.RestProductFavorites.Any(f => f.RestAppUser.Id == session.RestAppUser.Id)) ? (true) : (false)
        //            })
        //        }).SingleOrDefault();

        //        return Json(product2, JsonRequestBehavior.AllowGet);
        //    }
        //    var product = db.RestCategorys.Where(c => c.Id == categoryId).Select(c => new
        //    {
        //        Activity = c.Activity,
        //        status = "product",
        //        Parent = c.Name,
        //        Image = (c.Image != null) ? (c.Image) : (c.ParentCategory.Image),
        //        Array = c.Products.Select(sc => new
        //        {
        //            Activity = sc.Activity,
        //            Name = sc.Name,
        //            Price = sc.Price,
        //            Id = sc.Id,
        //            Image = (sc.Image != null) ? (sc.Image) : (sc.Category.Image),
        //            Description = sc.Description,
        //            Weight = sc.Weight
        //        })
        //    }).SingleOrDefault();

        //    return Json(product, JsonRequestBehavior.AllowGet);
        //}
        //public JsonResult CatMenu(int restId, int? catId)
        //{
        //    if (!catId.HasValue)
        //    {
        //        var menuItems = db.RestMenus.Where(m => m.RestPoint.Point.Id == restId).Select(m => new
        //        {
        //            Activity = m.Activity,
        //            Name = m.Name,
        //            Id = m.Id,
        //            status = "category",
        //            Image = m.Image
        //        });
        //        return Json(menuItems, JsonRequestBehavior.AllowGet);
        //    }
        //    var menus = db.RestMenus.Where(rm => rm.Id == catId).Select(rm => new
        //    {
        //        Activity = rm.Activity,
        //        Image = rm.Image,
        //        status = "category",
        //        Parent = rm.Name,
        //        Array = rm.Categorys.Where(c => c.ParentCategory == null).Select(m => new
        //        {
        //            Activity = m.Activity,
        //            Name = m.Name,
        //            Id = m.Id,
        //            Image = m.Image
        //        })
        //    }).SingleOrDefault();
        //    return Json(menus, JsonRequestBehavior.AllowGet);
        //}
    }
}