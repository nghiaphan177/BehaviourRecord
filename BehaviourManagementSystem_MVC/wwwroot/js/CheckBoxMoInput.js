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

    //show it when the checkbox is clicked
    $('input[id="check-nhe-sua"]').on('click', function () {
        if ($(this).prop('checked')) {
            $('#input-nhe-sua').fadeIn();
        } else {
            $('#input-nhe-sua').hide();
        }
    });

    if ($('#check-nhe-sua').prop('checked')) {
        $('#input-nhe-sua').fadeIn();
    } else {
        $('#input-nhe-sua').hide();
    }
    

    $('#input-vua-sua').hide();

    //show it when the checkbox is clicked
    $('input[id="check-vua-sua"]').on('click', function () {
        if ($(this).prop('checked')) {
            $('#input-vua-sua').fadeIn();
        } else {
            $('#input-vua').hide();
        }
    });

    if ($('#check-vua-sua').prop('checked')) {
        $('#input-vua-sua').fadeIn();
    } else {
        $('#input-vua-sua').hide();
    }

    $('#input-kn-sua').hide();

    //show it when the checkbox is clicked
    $('input[id="check-kn-sua"]').on('click', function () {
        if ($(this).prop('checked')) {
            $('#input-kn-sua').fadeIn();
        } else {
            $('#input-kn-sua').hide();
            $('#khacnghiet').val('');

        }
    });

    if ($('#check-kn-sua').prop('checked')) {
        $('#input-kn-sua').fadeIn();
    } else {
        $('#input-kn-sua').hide();
        $('#khacnghiet-sua').val('');
    }
});