if (!window.settingsTemplates) {
    window.settingsTemplates = {
    }
}

window.settingsTemplates.SingleColumnBronze = {
    collectSettings: function () {
        var $section = $(".SingleColumnBronzeTmpl");
        var settings = {
            BackgroundColor: $("#BackgroundColor").val(),
            HeadingBkgdColor: $("#HeadingBkgdColor").val()
        };

        return settings;
    },

    getRenderedTemplate: function (settings) {
        var source = document.getElementById("SingleColumnBronzeTmpl").innerHTML;
        var template = Handlebars.compile(source);

        var html = template(settings);
        return html;
    }
}