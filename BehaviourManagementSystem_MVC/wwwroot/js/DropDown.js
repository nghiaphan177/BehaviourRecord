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

$('#multiselect-drop input').change(function () {
    var s = $('#multiselect-drop input:checked').map(function () {
        return this.value;
    }).get().join(',');
    $('#nhe').val((s.length > 0 ? s : ""));
    $('#nhe-sua').val((s.length > 0 ? s : ""));
});

$('#multiselect-drop-nphuchoi input').change(function () {
    var s = $('#multiselect-drop-nphuchoi input:checked').map(function () {
        return this.value;
    }).get().join(',');
    $('#nphuchoi').val((s.length > 0 ? s : ""));
});

$('#multiselect-drop-tb input').change(function () {
    var s = $('#multiselect-drop-tb input:checked').map(function () {
        return this.value;
    }).get().join(',');
    $('#trungbinh').val((s.length > 0 ? s : ""));
    $('#trungbinh-sua').val((s.length > 0 ? s : ""));

});

$('#multiselect-drop-tbphuchoi input').change(function () {
    var s = $('#multiselect-drop-tbphuchoi input:checked').map(function () {
        return this.value;
    }).get().join(',');
    $('#tbphuchoi').val((s.length > 0 ? s : ""));
});

$('#multiselect-drop-kn input').change(function () {
    var s = $('#multiselect-drop-kn input:checked').map(function () {
        return this.value;
    }).get().join(',');
    $('#khacnghiet').val((s.length > 0 ? s : ""));
    $('#khacnghiet-sua').val((s.length > 0 ? s : ""));

});

$('#multiselect-knphuchoi input').change(function () {
    var s = $('#multiselect-knphuchoi input:checked').map(function () {
        return this.value;
    }).get().join(',');
    $('#knphuchoi').val((s.length > 0 ? s : ""));
});

/// Edit

