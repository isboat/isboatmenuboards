window.menuBoards.subTemplateSettings = {
    settings: null,
    currentTemplate: null,
    listOfDefaultSettings : null,

    loadDefaultSubTemplateSettings: function () {
        var $this = this;

        window.menuBoards.httpWrapper.get({
            url: '/Slide/GetDefaultSubTemplateSettings',
            success: function (response) {

                if (response && response !== "error") {
                    self.templateTypes = response.TemplateTypeOptions;
                    
                    $this.listOfDefaultSettings = response;
                }
            },
            error: function () { }
        });
    },

    showDefaultTemplate: function () {

        if (this.settings) {
            this.appendTemplate(this.settings.HtmlTemplateId);
        }
    },

    showSelectedTemplate: function () {
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