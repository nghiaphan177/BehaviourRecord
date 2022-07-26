$(function () {
    $('#input-nhe').hide();
    $('#input-ql-nhe').hide();
    //show it when the checkbox is clicked
    $('input[id="check-nhe"]').on('click', function () {
        if ($(this).prop('checked')) {
            $('#input-nhe').fadeIn();
            $('#input-ql-nhe').fadeIn();
        } else {
            $('#input-nhe').hide();
            $('#input-ql-nhe').hide();
        }
    });

    if ($('#check-nhe').prop('checked')) {
        $('#input-nhe').fadeIn();
        $('#nhe-sua').prop('required', true);
        $('#input-ql-nhe').fadeIn();
    } else {
        $('#input-nhe').hide();
        $('#nhe').val('');
        $('#nhe').prop('required', null);
        $('#ql-nhe').val('');
        $('#input-ql-nhe').hide();
    }

    $('#input-vua').hide();
    $('#input-ql-vua').hide();
    //show it when the checkbox is clicked
    $('input[id="check-vua"]').on('click', function () {
        if ($(this).prop('checked')) {
            $('#input-vua').fadeIn();
            $('#input-ql-vua').fadeIn();
        } else {
            $('#input-vua').hide();
            $('#input-ql-vua').hide();
        }
    });

    if ($('#check-vua').prop('checked')) {
        $('#input-vua').fadeIn();
        $('#vua').prop('required', true);
        $('#input-ql-vua').fadeIn();
    } else {
        $('#input-vua').hide();
        $('#vua').val('');
        $('#vua').prop('required', null);
        $('#ql-vua').val('');
        $('#input-ql-vua').hide();
    }

    $('#input-kn').hide();
    $('#input-ql-kn').hide();
    //show it when the checkbox is clicked
    $('input[id="check-kn"]').on('click', function () {
        if ($(this).prop('checked')) {
            $('#input-kn').fadeIn();
            $('#input-ql-kn').fadeIn();
        } else {
            $('#input-kn').hide();
            $('#input-ql-kn').hide();
        }
    });



    //Edit


    $('#input-nhe-sua').hide();
    $('#input-ql-nhe-sua').hide();
    //show it when the checkbox is clicked
    $('input[id="check-nhe-sua"]').on('click', function () {
        if ($(this).prop('checked')) {
            $('#input-nhe-sua').fadeIn();
            $('#nhe-sua').prop('required', true);
            $('#input-ql-nhe-sua').fadeIn();
            $('#ql-nhe-sua').prop('required', true);
        } else {
            $('#input-nhe-sua').hide();
            $('#nhe-sua').val('');
            $('#nhe-sua').prop('required', null);
            $('#ql-nhe-sua').val('');
            $('.checkbox-nhe').prop('checked', false);
            $('#input-ql-nhe-sua').hide();
            $('#ql-nhe-sua').prop('required', null);
        }
    });

    if ($('#check-nhe-sua').prop('checked')) {
        $('#input-nhe-sua').fadeIn();
        $('#nhe-sua').prop('required', true);
        $('#input-ql-nhe-sua').fadeIn();
        $('#ql-nhe-sua').prop('required', true);
    } else {
        $('#input-nhe-sua').hide();
        $('#nhe-sua').val('');
        $('#nhe-sua').prop('required', null);
        $('#ql-nhe-sua').val('');
        $('#input-ql-nhe-sua').hide();
        $('#ql-nhe-sua').prop('required', null);
    }
    

    $('#input-vua-sua').hide();
    $('#input-ql-vua-sua').hide();
    //show it when the checkbox is clicked
    $('input[id="check-vua-sua"]').on('click', function () {
        if ($(this).prop('checked')) {
            $('#input-vua-sua').fadeIn();
            $('#trungbinh-sua').prop('required', true);
            $('#input-ql-vua-sua').fadeIn();
            $('#ql-vua-sua').prop('required', true);
        } else {
            $('#input-vua-sua').hide();
            $('#trungbinh-sua').val('');
            $('#trungbinh-sua').prop('required', null);
            $('#ql-vua-sua').val('');
            $('.checkbox-vua').prop('checked', false);
            $('#input-ql-vua-sua').hide();
            $('#ql-vua-sua').prop('required', null);

        }
    });

    if ($('#check-vua-sua').prop('checked')) {
        $('#input-vua-sua').fadeIn();
        $('#trungbinh-sua').prop('required', true);
        $('#input-ql-vua-sua').fadeIn();
        $('#ql-vua-sua').prop('required', true);
    } else {
        $('#input-vua-sua').hide();
        $('#trungbinh-sua').val('');
        $('#trungbinh-sua').prop('required', null);
        $('#ql-vua-sua').val('');
        $('#input-ql-vua-sua').hide();
        $('#ql-vua-sua').prop('required', null);
    }

    $('#input-kn-sua').hide();
    $('#input-quanly-kn').hide();
    $('#input-ql-kn-sua').hide();
    //show it when the checkbox is clicked
    $('input[id="check-kn-sua"]').on('click', function () {
        if ($(this).prop('checked')) {
            $('#input-kn-sua').fadeIn();
            $('#khacnghiet-sua').prop('required', true);
            $('#input-quanly-kn').fadeIn();
            $('#input-ql-kn-sua').fadeIn();
        } else {
            $('#input-kn-sua').hide();
            $('#khacnghiet-sua').val('');
            $('#khacnghiet-sua').prop('required', null);
            $('#ql-khacnghiet-sua').val('');
            $('.checkbox-khac-nghiet').prop('checked', false);
            $('#input-quanly-kn').hide();
            $('#input-ql-kn-sua').hide();
        }
    });

    if ($('#check-kn-sua').prop('checked')) {
        $('#input-kn-sua').fadeIn();
        $('#khacnghiet-sua').prop('required', true);
        $('#input-quanly-kn').fadeIn();
        $('#input-ql-kn-sua').fadeIn();

    } else {
        $('#input-kn-sua').hide();
        $('#khacnghiet-sua').val('');
        $('#khacnghiet-sua').prop('required', null);
        $('#ql-khacnghiet-sua').val('');
        $('#input-quanly-kn').hide();
        $('#input-ql-kn-sua').hide();
    }
});