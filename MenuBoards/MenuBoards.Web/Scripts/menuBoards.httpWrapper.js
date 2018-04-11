window.menuBoards.httpWrapper = {

    get: function (options) {
        $.ajax({
            type: "GET",
            url: options.url,
            data: options.data,
            dataType: "json",
            contentType: "application/json; charset=utf-8",
            success: options.success,
            error: options.error
        });
    },

    post: function(options) {
        $.ajax({
            type: "POST",
            url: options.url,
            data: JSON.stringify(options.data),
            dataType: "json",
            contentType: "application/json; charset=utf-8",
            success: options.success,
            error: options.error
        });
    },

    delete: function(options) {
        $.ajax({
            type: "GET",
            url: options.url,
            data: options.data,
            dataType: "json",
            contentType: "application/json; charset=utf-8",
            success: options.success,
            error: options.error
        });
    }
}