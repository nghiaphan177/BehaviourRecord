var minDate, maxDate;

// Custom filtering function which will search data in column four between two values
$.fn.dataTable.ext.search.push(
    function (settings, data, dataIndex) {
        let min = moment($('#min').val(), 'DD/MM/YYYY', true).isValid() ?
            moment($('#min').val(), 'DD/MM/YYYY', true).unix() :
            null;

        let max = moment($('#max').val(), 'DD/MM/YYYY').isValid() ?
            moment($('#max').val(), 'DD/MM/YYYY', true).unix() :
            null;
        var date = moment(data[0], 'DD/MM/YYYY', true).unix();

       

        if (
            (min === null && max === null) ||
            (min === null && date <= max) ||
            (min <= date && max === null) ||
            (min <= date && date <= max)
        ) {
            return true;
        }
        return false;
    }
);

$(document).ready(function () {
    // Create date inputs
    minDate = new DateTime($('#min'), {
        format: 'DD/MM/YYYY'
    });
    maxDate = new DateTime($('#max'), {
        format: 'DD/MM/YYYY'
    });

    // DataTables initialisation
    var table = $('#example').DataTable();

    // Refilter the table
    $('#min, #max').on('change', function () {
        table.draw();
    });
});