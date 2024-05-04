function ReloadTable() {
    // Получаем новые значения параметров
    var errorRate = $('#errorRateInput').val();
    var seed = $('#seedInput').val();
    var region = $('#regionSelect').val();

    // Отправляем запрос на сервер для загрузки новых данных
    $.post('/User/GenerateUsers', { count: 20, errorRate: errorRate, seed: seed, region: region }, function(data) {
        // Обновляем содержимое таблицы новыми данными
        $('#userTable tbody').html(data);
    });
}

// Обработчик клика на кнопку экспорта в CSV
$('#exportButton').click(function() {
    // Получаем текущие параметры
    var errorRate = $('#errorRateInput').val();
    var seed = $('#seedInput').val();
    var region = $('#regionSelect').val();
    var currentPage = $('#currentPage').val();

    // Формируем URL для запроса экспорта
    var exportUrl = '/User/ExportToCsv?errorRate=' + errorRate + '&seed=' + seed + '&region=' + region + '&page=' + currentPage;

    // Отправляем GET-запрос на сервер для экспорта в CSV
    window.location = exportUrl;
});
