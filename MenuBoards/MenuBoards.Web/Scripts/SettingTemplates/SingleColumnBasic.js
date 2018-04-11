if (!window.settingsTemplates) {
    window.settingsTemplates = {
    }
}

window.settingsTemplates.SingleColumnBasic = {
    collectSettings: function () {
        var $section = $(".SingleColumnBasicTmpl");
        var settings = {
            BackgroundColor: $("#BackgroundColor").val(),
            HeadingBkgdColor: $("#HeadingBkgdColor").val()
        };

        return settings;
    },

    getRenderedTemplate: function(settings) {
        var source = document.getElementById("SingleColumnBasicTmpl").innerHTML;
        var template = Handlebars.compile(source);
        
        var html = template(settings);
        return html;
    }
}