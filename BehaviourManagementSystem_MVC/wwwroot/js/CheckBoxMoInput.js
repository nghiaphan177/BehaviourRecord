$(function () {
    $('#input-nhe').hide();

    //show it when the checkbox is clicked
    $('input[id="check-nhe"]').on('click', function () {
        if ($(this).prop('checked')) {
            $('#input-nhe').fadeIn();
        } else {
            $('#input-nhe').hide();
        }
    });


    $('#input-vua').hide();

    //show it when the checkbox is clicked
    $('input[id="check-vua"]').on('click', function () {
        if ($(this).prop('checked')) {
            $('#input-vua').fadeIn();
        } else {
            $('#input-vua').hide();
        }
    });

    $('#input-kn').hide();

    //show it when the checkbox is clicked
    $('input[id="check-kn"]').on('click', function () {
        if ($(this).prop('checked')) {
            $('#input-kn').fadeIn();
        } else {
            $('#input-kn').hide();
        }
    });



    //Edit


    $('#input-nhe-sua').hide();
    $('#input-quanly-nhe').hide();
    //show it when the checkbox is clicked
    $('input[id="check-nhe-sua"]').on('click', function () {
        if ($(this).prop('checked')) {
            $('#input-nhe-sua').fadeIn();
            $('#input-quanly-nhe').fadeIn();
        } else {
            $('#input-nhe-sua').hide();
            $('#nhe-sua').val('');
            $('.checkbox-nhe').prop('checked', false);
            $('#input-quanly-nhe').hide();
        }
    });

    if ($('#check-nhe-sua').prop('checked')) {
        $('#input-nhe-sua').fadeIn();
        $('#input-quanly-nhe').fadeIn();
    } else {
        $('#input-nhe-sua').hide();
        $('#nhe-sua').val('');
        $('#input-quanly-nhe').hide();
    }
    

    $('#input-vua-sua').hide();
    $('#input-quanly-vua').hide();
    //show it when the checkbox is clicked
    $('input[id="check-vua-sua"]').on('click', function () {
        if ($(this).prop('checked')) {
            $('#input-vua-sua').fadeIn();
            $('#input-quanly-vua-sua').fadeIn();
        } else {
            $('#input-vua-sua').hide();
            $('#trungbinh-sua').val('');
            $('.checkbox-vua').prop('checked', false);
            $('#input-quanly-vua').hide();

        }
    });

    if ($('#check-vua-sua').prop('checked')) {
        $('#input-vua-sua').fadeIn();
        $('#input-quanly-vua').fadeIn();
    } else {
        $('#input-vua-sua').hide();
        $('#trungbinh-sua').val('');
        $('#input-quanly-vua').hide();
    }

    $('#input-kn-sua').hide();
    $('#input-quanly-kn').hide();
    //show it when the checkbox is clicked
    $('input[id="check-kn-sua"]').on('click', function () {
        if ($(this).prop('checked')) {
            $('#input-kn-sua').fadeIn();
            $('#input-quanly-kn').fadeIn();
        } else {
            $('#input-kn-sua').hide();
            $('#khacnghiet-sua').val('');
            $('.checkbox-khac-nghiet').prop('checked', false);
            $('#input-quanly-kn').hide();
        }
    });

    if ($('#check-kn-sua').prop('checked')) {
        $('#input-kn-sua').fadeIn();
        $('#input-quanly-kn').fadeIn();
    } else {
        $('#input-kn-sua').hide();
        $('#khacnghiet-sua').val('');
        $('#input-quanly-kn').hide();
    }
});