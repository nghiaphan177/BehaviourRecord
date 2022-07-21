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
});