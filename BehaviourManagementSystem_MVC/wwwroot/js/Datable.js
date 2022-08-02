$(document).ready(function () {
    $('#example').DataTable({
        dom: 'lrtip',
        order: [[0, 'asc']],
        "info": false,
        "lengthChange": false,
        "language": {
            "search": "Tìm Kiếm:"
        }
    });
});
