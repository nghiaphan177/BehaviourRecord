$(document).ready(function () {
    $('#example').DataTable({
        dom: 'lrtip',
        "order": [[0, "desc"]], //or asc 
        "columnDefs": [{ "targets": 0, "type": "date-eu" }],
        "info": false,
        "lengthChange": false,
        "language": {
            "search": "Tìm Kiếm:"
        }
    });
});
