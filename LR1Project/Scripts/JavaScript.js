@section Scripts {
    <script>
// получить Uri к api/Dishes var uri="@Url.Action("GetFlower","Flower")";
        $(document).ready(function () {
            // Получить список объектов
            $.getJSON(uri, function (data) {
                // Для каждого элемента списка
                $.each(data, function (index, value) {
                    // создать строку таблицы и добавить ее в таблицу
                    $("#list").append(createRow(index + 1, value));
                });
            })
        });
// Функция формирования одной строки таблицы
// index - порядковый номер
// data - объект данных
function createRow(index, data) {
// создать строку таблицы
var row = $("<tr>");
        // добавть колонку с порядковым номером
row.append("<td>" + index + "</td>");
// добавить колонку с названием
row.append("<td>" + data.flowerName + "</td>");
// создать кнопку
var button = $("<button>")
            .addClass("btn btn-default") // стиль кнопки
            .click(data.flowerId, showDetails) // подписка на событие click
            .text("Подробно"); // надпись
            // создать колонку с кнопкой
var td = $("<td>").append(button);
                // добавить в строку таблицы
                row.append(td);
                return row;
                }
                //Функция выода информации о выбранном объекте
function showDetails(e) {
                        // Послать запрос
                        $.getJSON(uri + "/" + e.data, function (data) {
                            // строка информации об объекте
                            var details = data.flowerName + ": "
                                + data.flowerdescription + " - "
                                + data.flowerprice + " руб.";
                            $("#details") // Найти блок для информации
                                .empty() // очистить содержимое
                                .text(details); // записать новый текст
                        })
                    }
</script> }