window.menuBoards.subTemplateSettings = {
    settings: null,
    currentTemplate: null,

    showDefaultTemplate: function () {

        if (this.settings) {
            this.appendTemplate(this.settings.HtmlTemplateId);
        }
    },

    appendTemplate: function (tmpId) {
        var self = this;

        //var $template = $("#" + tmpId + "Tmpl");
        //var $controls = $($template.html());

        var $subTplSection = $(".designSettingsForm .subTplSettings");
        $subTplSection.empty();
        //$subTplSection.append($controls);

        self.currentTemplate = tmpId;
        var html = window.settingsTemplates[self.currentTemplate].getRenderedTemplate(self.settings);
        $subTplSection.append(html);

    },

    setSettings: function() {
        this.settings = window.settingsTemplates[this.currentTemplate].collectSettings();
    }
}