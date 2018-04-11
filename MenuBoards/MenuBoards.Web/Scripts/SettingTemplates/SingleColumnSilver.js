if (!window.settingsTemplates) {
    window.settingsTemplates = {
    }
}

window.settingsTemplates.SingleColumnSilver = {
    collectSettings: function () {
        var $section = $(".SingleColumnSilverTmpl");
        var settings = {
            BackgroundColor: $("#BackgroundColor").val(),
            HeadingBkgdColor: $("#HeadingBkgdColor").val()
        };

        return settings;
    },

    getRenderedTemplate: function (settings) {
        var source = document.getElementById("SingleColumnSilverTmpl").innerHTML;
        var template = Handlebars.compile(source);

        var html = template(settings);
        return html;
    }
}