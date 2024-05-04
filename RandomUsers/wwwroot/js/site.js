function ReloadTable() {
    
    var errorRate = $('#errorRateInput').val();
    var seed = $('#seedInput').val();
    var region = $('#regionSelect').val();
    
    $.post('/User/GenerateUsers', { count: 20, errorRate: errorRate, seed: seed, region: region }, function(data) {
        $('#userTable tbody').html(data);
    });
}


$('#exportButton').click(function() {
    var errorRate = $('#errorRateInput').val();
    var seed = $('#seedInput').val();
    var region = $('#regionSelect').val();
    var currentPage = $('#currentPage').val();
    
    var exportUrl = '/User/ExportToCsv?errorRate=' + errorRate + '&seed=' + seed + '&region=' + region + '&page=' + currentPage;
    
    window.location = exportUrl;
});
