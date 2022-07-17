$('#multiselect-drop-camxuc input').change(function () {
    var s = $('#multiselect-drop-camxuc input:checked').map(function () {
        return this.value;
    }).get().join(',');
    $('#camxuc').val((s.length > 0 ? s : ""));
});

$('#multiselect-drop-moitruong input').change(function () {
    var s = $('#multiselect-drop-moitruong input:checked').map(function () {
        return this.value;
    }).get().join(',');
    $('#moitruong').val((s.length > 0 ? s : ""));
});

$('#multiselect-drop-hanhdong input').change(function () {
    var s = $('#multiselect-drop-hanhdong input:checked').map(function () {
        return this.value;
    }).get().join(',');
    $('#hanhdong').val((s.length > 0 ? s : ""));
});