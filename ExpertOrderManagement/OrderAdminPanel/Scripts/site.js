
//$(document).bind('ajaxStart', function () {
//    $('#loader-wrapper').fadeIn();
//});

$(document).ajaxSend(function (evt, request, settings) {
    if (!(settings.url.indexOf('/CheckEmailId') || settings.url.indexOf('/CheckUserName')))
    {
        $('#loader-wrapper').fadeIn();
    }
});


$(document).bind('ajaxStop', function () {
    $('#loader-wrapper').fadeOut();
});

$(document).ajaxError(function (event, jqxhr, settings, thrownError) {
    if (jqxhr.responseText) {
        $.Notify({ keepOpen: true, type: 'alert', caption: 'Error', content: jqxhr.responseText });
    }
});