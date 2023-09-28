Выполненное тестовое задание на разработку сайта компании (WebAPI/Angular/Database).<br>
Фронтенд часть - https://github.com/dmitriytomarov/company-app-frontend

Стек:<br>
Бэкенд: C#, ASP.Net Core 6.0<br>
Фронтенд: Angular<br>
База данных: MS SQL(*.mdf)<br>
Верстка: Bootstrap<br>

Задание:<br>
Разработать сайт с горизонтальным навигационным меню, состоящим из двух вкладок:<br>
 - “О компании” (стартовая страница)<br>
 - “Сотрудники”<br>

На первой вкладке использовать произвольную верстку. <br>
При переходе на вкладку «Сотрудники» должна появляться таблица, состоящая из колонок:<br>
 - “Отдел”<br>
 - “Ф.И.О”<br>
 - “Дата рождения”<br>
 - “Дата устройства на работу”<br>
 - “Заработная плата”<br>
  - “Редактирование/удаление сотрудника” (также предусмотреть функционал добавления нового сотрудника)<br>
  
Все что в таблице должно браться из базы данных MSSQL.<br>
Предусмотреть возможность фильтрации и сортировки данных в таблице.<br>
В колонке “Редактирование/удаление сотрудника” хедер должен быть пустой, в теле - две кнопки.<br> 
При нажатии на кнопку “Редактировать” - открывается окно с полями сотрудника, при нажатии на кнопку "Удалить" - показывается окно с подтверждением.<br>

Все действия должны выполняться без перезагрузки страницы (Single Page Application).<br>

Написать и приложить SQL запросы для: <br>
 - выборки всех сотрудников<br>
 - выборки сотрудников у кого зп выше 10000 <br>
 - удаления сотрудников старше 70 лет <br>
 - обновления зп до 15000 тем сотрудникам, у которых она меньше.<br>
  
*Запросы находятся в каталоге решения, папка "Additional (SQL)".
